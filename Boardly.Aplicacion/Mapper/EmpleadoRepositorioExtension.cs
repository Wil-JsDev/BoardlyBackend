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
        
        var proyectos = await repositorio.ObtenerConteoDeProyectosQuePerteneceAsync(empleadoId, cancellationToken);

        var actividades = await repositorio.ObtenerConteoDeActividadesQuePerteneceAsync(empleadoId, cancellationToken);

        var tareasTotales = await repositorio.ObtenerConteoDeTareasQuePerteneceAsync(empleadoId, cancellationToken);
       
        var tareasEnProceso = await repositorio.ObtenerConteoDeTareasEnProcesoQuePertenceAsync(empleadoId, cancellationToken);

        var tareasAVencer = await repositorio.ObtenerConteoDeTareasAVencerAsync(empleadoId, cancellationToken);
      
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
