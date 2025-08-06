using Boardly.Aplicacion.DTOs.Proyecto;
using Boardly.Dominio.Puertos.CasosDeUso.Proyecto;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Proyecto;

public class ObtenerIdProyecto(
    ILogger<ObtenerIdProyecto> logger,
    IProyectoRepositorio repositorioProyecto,
    IDistributedCache cache
    ) : IObtenerIdProyecto<ProyectoDto>
{
    public async Task<ResultadoT<ProyectoDto>> ObtenerIdProyectoAsync(Guid id, CancellationToken cancellationToken)
    {
        var proyectoId = await cache.ObtenerOCrearAsync(
            $"obtener-proyecto-{id}",
            async () => await repositorioProyecto.ObtenerByIdAsync(id, cancellationToken),
            cancellationToken: cancellationToken
        );

        if (proyectoId is null)
        {
            logger.LogWarning("No se encontró el proyecto con ID: {ProyectoId} al intentar obtenerlo.", id);

            return ResultadoT<ProyectoDto>.Fallo(
                Error.NoEncontrado("404", "El proyecto especificado no fue encontrado."));
        }

        ProyectoDto proyectoDto = new
        (
            proyectoId.ProyectoId,
            proyectoId.EmpresaId,
            proyectoId.Nombre,
            proyectoId.Descripcion,
            proyectoId.FechaInicio,
            proyectoId.FechaFin,
            proyectoId.Estado,
            proyectoId.FechaCreado
        );

        logger.LogInformation("Se obtuvo correctamente el proyecto con ID: {ProyectoId}", proyectoId.ProyectoId);

        return ResultadoT<ProyectoDto>.Exito(proyectoDto);
    }

}
