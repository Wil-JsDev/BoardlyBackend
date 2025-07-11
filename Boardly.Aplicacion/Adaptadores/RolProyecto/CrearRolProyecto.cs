using Boardly.Aplicacion.DTOs.RolProyecto;
using Boardly.Dominio.Puertos.CasosDeUso.RolProyecto;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.RolProyecto;

public class CrearRolProyecto(
    ILogger<CrearRolProyecto> logger,
    IRolProyectoRepositorio repositorioRolProyecto
    ) : ICrearRolProyecto<CrearRolProyectoDto, RolProyectoDto>
{
    public async Task<ResultadoT<RolProyectoDto>> CrearRolProyectoAsync(CrearRolProyectoDto solicitud, CancellationToken cancellationToken)
    {
        if (solicitud is null)
        {
            logger.LogWarning("Solicitud nula recibida al intentar crear un rol de proyecto.");
            
            return ResultadoT<RolProyectoDto>.Fallo(Error.Fallo("400", "La solicitud no puede ser nula."));
        }

        if (await repositorioRolProyecto.ExisteNombreRolProyectoAsync(solicitud.ProyectoId, solicitud.Nombre, cancellationToken))
        {
            logger.LogWarning("Ya existe un rol de proyecto con el nombre: {Nombre}", solicitud.Nombre);
            
            return ResultadoT<RolProyectoDto>.Fallo(Error.Conflicto("409", "Ya existe un rol de proyecto con ese nombre."));
        }

        Dominio.Modelos.RolProyecto rolProyectoEntidad = new()
        {
            RolProyectoId = Guid.NewGuid(),
            Nombre = solicitud.Nombre,
            Descripcion = solicitud.Descripcion
        };

        await repositorioRolProyecto.CrearAsync(rolProyectoEntidad, cancellationToken);
        
        logger.LogInformation("Rol de proyecto creado exitosamente en la base de datos. RolProyectoId: {RolProyectoId}", rolProyectoEntidad.RolProyectoId);

        RolProyectoDto rolProyectoDto = new
        (
            RolProyectoId: rolProyectoEntidad.RolProyectoId,
            Nombre: rolProyectoEntidad.Nombre,
            Descripcion: rolProyectoEntidad.Descripcion
        );

        return ResultadoT<RolProyectoDto>.Exito(rolProyectoDto);
    }
}