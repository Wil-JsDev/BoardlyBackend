using Boardly.Aplicacion.DTOs.Ceo;
using Boardly.Dominio.Puertos.CasosDeUso.Ceo;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Ceo;

public class CrearCeo(
    ILogger<CrearCeo> logger,
    ICeoRepositorio ceoRepositorio,
    IUsuarioRepositorio usuarioRepositorio
) : ICrearCeo<CrearCeoDto, CeoDto>
{
    public async Task<ResultadoT<CeoDto>> CrearCeoAsync(CrearCeoDto solicitud, CancellationToken cancellationToken)
    {
        if (solicitud is null)
        {
            logger.LogWarning("Solicitud de creación de Ceo es nula.");
            return ResultadoT<CeoDto>.Fallo(Error.Fallo("400", "La solicitud de creación de Ceo no puede ser nula."));
        }
        
        var usuario = await usuarioRepositorio.ObtenerByIdAsync(solicitud.UsuarioId, cancellationToken);
        if (usuario is null)
        {
            logger.LogWarning("No existe un usuario con el ID: {UsuarioId}", solicitud.UsuarioId);
            return ResultadoT<CeoDto>.Fallo(Error.NoEncontrado("404", "El usuario no existe."));
        }
        
        Dominio.Modelos.Ceo ceoEntidad = new()
        {
            CeoId = Guid.NewGuid(),
            UsuarioId = solicitud.UsuarioId,
        };
        
        await ceoRepositorio.CrearAsync(ceoEntidad, cancellationToken);

        logger.LogInformation("Ceo creado exitosamente en la base de datos. Ceo: {CeoId}", ceoEntidad.CeoId);

        CeoDto ceo = new(
            CeoId: ceoEntidad.CeoId,
            UsuarioId: ceoEntidad.UsuarioId
        );
        
        return ResultadoT<CeoDto>.Exito(ceo);
    }
}