using Boardly.Aplicacion.DTOs.Proyecto;
using Boardly.Dominio.Puertos.CasosDeUso.Proyecto;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Proyecto;

public class ActualizarProyecto(
    ILogger<ActualizarProyecto> logger,
    IProyectoRepositorio repositorioProyecto
    ) : IActualizarProyecto<ActualizarProyectoDto, ActualizarProyectoDto>
{
    public async Task<ResultadoT<ActualizarProyectoDto>> ActualizarProyectoAsync(Guid id, ActualizarProyectoDto solicitud, CancellationToken cancellationToken)
    {
        var proyecto = await repositorioProyecto.ObtenerByIdAsync(id, cancellationToken);
        if (proyecto is null)
        {
            logger.LogWarning("No se encontró el proyecto con ID: {ProyectoId} al intentar actualizar.", id);

            return ResultadoT<ActualizarProyectoDto>.Fallo(
                Error.NoEncontrado("404", "El proyecto especificado no fue encontrado."));
        }

        if (await repositorioProyecto.NombreProyectoEnUsoAsync(proyecto.ProyectoId, solicitud.Nombre, cancellationToken))
        {
            logger.LogWarning("El nombre '{Nombre}' ya está en uso por otro proyecto diferente al ID: {ProyectoId}.", solicitud.Nombre, proyecto.ProyectoId);

            return ResultadoT<ActualizarProyectoDto>.Fallo(
                Error.Conflicto("409", "Ya existe otro proyecto con ese nombre."));
        }

        proyecto.Nombre = solicitud.Nombre;
        proyecto.Descripcion = solicitud.Descripcion;
        proyecto.FechaActualizacion = DateTime.UtcNow;

        await repositorioProyecto.ActualizarAsync(proyecto, cancellationToken);

        logger.LogInformation("Proyecto actualizado exitosamente. ID: {ProyectoId}", proyecto.ProyectoId);

        ActualizarProyectoDto proyectoDto = new
        (
            ProyectoId: proyecto.ProyectoId,
            Nombre: proyecto.Nombre,
            Descripcion: proyecto.Descripcion
        );

        return ResultadoT<ActualizarProyectoDto>.Exito(proyectoDto);
    }

}