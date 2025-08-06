using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empleados;

public class ResultadoPaginadoEmpleadoPorProyectoId(
    ILogger<ResultadoPaginadoEmpleadoPorProyectoId> logger,
    IEmpleadoRepositorio empleadoRepositorio,
    IProyectoRepositorio proyectoRepositorio,
    IDistributedCache cache
    ) : IResultadoPaginadoEmpleadoPorProyectoId<PaginacionParametro, EmpleadoRolProyectoDto>
{
public async Task<ResultadoT<ResultadoPaginado<EmpleadoRolProyectoDto>>> ObtenerPaginacionEmpleadoPorProyectoIdAsync(Guid proyectoId, PaginacionParametro solicitud,
    CancellationToken cancellationToken)
{
    var proyecto = await proyectoRepositorio.ObtenerByIdAsync(proyectoId, cancellationToken);
    if (proyecto is null)
    {
        logger.LogWarning("Este {ProyectoId} id no existe", proyectoId);
        return ResultadoT<ResultadoPaginado<EmpleadoRolProyectoDto>>.Fallo(Error.Fallo("400", "Este id del proyecto no existe."));
    }

    if (solicitud.TamanoPagina <= 0 || solicitud.NumeroPagina <= 0)
    {
        logger.LogWarning("Los parámetros de paginación son inválidos. NúmeroPagina: {NumeroPagina}, TamanoPagina: {TamanoPagina}",
            solicitud.NumeroPagina, solicitud.TamanoPagina);
        return ResultadoT<ResultadoPaginado<EmpleadoRolProyectoDto>>.Fallo(
            Error.Fallo("400", "Los parámetros de paginación deben ser mayores a cero.")
        );
    }

    var obtenerPaginas = await empleadoRepositorio.ObtenerPaginasEmpleadoProyectoIdAsync(
        proyectoId, solicitud.NumeroPagina, solicitud.TamanoPagina, cancellationToken);

    var resultadoPaginaDto = obtenerPaginas.Elementos!.Select(x => new EmpleadoRolProyectoDto
    (
        EmpleadoId: x.EmpleadoId,
        NombreCompleto: $"{x.Usuario.Nombre} {x.Usuario.Apellido}",
        Posicion: x.EmpleadosProyectoRol
            .FirstOrDefault(y => y.ProyectoId == proyectoId)?.RolProyecto?.Nombre ?? "Sin rol",
        FotoPerfil: x.Usuario.FotoPerfil
    )).ToList();

    var totalCount = obtenerPaginas.TotalElementos;

    ResultadoPaginado<EmpleadoRolProyectoDto> resultado = new(resultadoPaginaDto, totalCount, solicitud.NumeroPagina, solicitud.TamanoPagina);

    if (!resultado.Elementos!.Any())
    {
        logger.LogWarning("No se encontraron empleados asociados al proyecto con ID: {ProyectoId}", proyectoId);
        return ResultadoT<ResultadoPaginado<EmpleadoRolProyectoDto>>.Fallo(Error.Fallo("404", "No hay empleados asignados al proyecto"));
    }

    logger.LogInformation("Se obtuvieron {Cantidad} empleados del proyecto con ID: {ProyectoId}", resultado.Elementos!.Count(), proyectoId);

    return ResultadoT<ResultadoPaginado<EmpleadoRolProyectoDto>>.Exito(resultado);
}
}