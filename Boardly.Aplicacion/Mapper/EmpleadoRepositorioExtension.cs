using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Mapper;

public static class EmpleadoRepositorioExtensions
{
    public static async Task<ResultadoT<ConteoEmpleadoDto>> MapearConteoEmpleadoAsync(
        this IEmpleadoRepositorio repositorio,
        Guid empleadoId,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        var empresas = await repositorio.ObtenerConteoDeEmpresasQuePerteneceAsync(empleadoId, cancellationToken);
        if (empresas == 0)
        {
            logger.LogWarning("El empleado con ID {EmpleadoId} no está asociado a ninguna empresa.", empleadoId);
            
            return ResultadoT<ConteoEmpleadoDto>.Fallo(Error.NoEncontrado("404", "El empleado no pertenece a ninguna empresa."));
        }

        var proyectos = await repositorio.ObtenerConteoDeProyectosQuePerteneceAsync(empleadoId, cancellationToken);
        if (proyectos == 0)
        {
            logger.LogWarning("El empleado con ID {EmpleadoId} no pertenece a ningún proyecto.", empleadoId);
            
            return ResultadoT<ConteoEmpleadoDto>.Fallo(Error.NoEncontrado("404", "El empleado no pertenece a ningún proyecto."));
        }

        var actividades = await repositorio.ObtenerConteoDeActividadesQuePerteneceAsync(empleadoId, cancellationToken);
        if (actividades == 0)
        {
            logger.LogWarning("El empleado con ID {EmpleadoId} no tiene actividades asignadas.", empleadoId);
            
            return ResultadoT<ConteoEmpleadoDto>.Fallo(Error.NoEncontrado("404", "El empleado no tiene actividades asignadas."));
        }

        var tareasTotales = await repositorio.ObtenerConteoDeTareasQuePerteneceAsync(empleadoId, cancellationToken);
        if (tareasTotales == 0)
        {
            logger.LogWarning("El empleado con ID {EmpleadoId} no tiene tareas asignadas.", empleadoId);
            
            return ResultadoT<ConteoEmpleadoDto>.Fallo(Error.NoEncontrado("404", "El empleado no tiene tareas asignadas."));
        }

        var tareasEnProceso = await repositorio.ObtenerConteoDeTareasEnProcesoQuePertenceAsync(empleadoId, cancellationToken);
        if (tareasEnProceso == 0)
        {
            logger.LogWarning("El empleado con ID {EmpleadoId} no tiene tareas en proceso.", empleadoId);
            
            return ResultadoT<ConteoEmpleadoDto>.Fallo(Error.NoEncontrado("404", "El empleado no tiene tareas en proceso."));
        }

        var tareasAVencer = await repositorio.ObtenerConteoDeTareasAVencerAsync(empleadoId, cancellationToken);
        if (tareasAVencer == 0)
        {
            logger.LogWarning("El empleado con ID {EmpleadoId} no tiene tareas próximas a vencer.", empleadoId);
            
            return ResultadoT<ConteoEmpleadoDto>.Fallo(Error.NoEncontrado("404", "El empleado no tiene tareas próximas a vencer."));
        }

        var dto = new ConteoEmpleadoDto(
            Empresas: empresas,
            Proyectos: proyectos,
            Actividades: actividades,
            TareasTotales: tareasTotales,
            TareasEnProceso: tareasEnProceso,
            TareasApuntoDeVencer: tareasAVencer
        );

        return ResultadoT<ConteoEmpleadoDto>.Exito(dto);
    }
}
