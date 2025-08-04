using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empleados;

public class ResultadoPaginadoEmpleadoEmpresaId(
    ILogger<ResultadoPaginadoEmpleadoEmpresaId> logger,
    IEmpleadoRepositorio empleadoRepositorio,
    IEmpresaRepositorio empresaRepositorio
    ) : IResultadoPaginadoEmpleadoEmpresaId<PaginacionParametro, EmpleadoRolProyectoDto>
{
    public async Task<ResultadoT<ResultadoPaginado<EmpleadoRolProyectoDto>>> ResultadoPaginadoEmpleadoEmpresaIdAsync(Guid empresaId, PaginacionParametro solicitud,
        CancellationToken cancellationToken)
    {
        var empresa = await empresaRepositorio.ObtenerByIdAsync(empresaId, cancellationToken);
        if (empresa is null)
        {
            logger.LogWarning("No se encontro la empresa con ID: {EmpresaId}", empresaId);
            return ResultadoT<ResultadoPaginado<EmpleadoRolProyectoDto>>.Fallo(Error.NoEncontrado("404", $"No se encontro la empresa con ID: {empresaId}"));
        }

        if (solicitud.TamanoPagina <= 0 || solicitud.NumeroPagina <= 0)
        {
            logger.LogWarning("Parametros de paginacion invalidos: NumeroPagina={NumeroPagina}, TamanoPagina={TamanoPagina}", solicitud.NumeroPagina, solicitud.TamanoPagina);
            return ResultadoT<ResultadoPaginado<EmpleadoRolProyectoDto>>.Fallo(Error.Fallo("400", "Parametros de paginacion invalidos. El numero y tamaño de pagina deben ser mayores a 0."));
        }

        var obtenerPaginas = await empleadoRepositorio.ObtenerPaginasEmpleadoEmpresaId(empresaId, solicitud.NumeroPagina, solicitud.TamanoPagina, cancellationToken);

        var resultadoPaginado = obtenerPaginas.Elementos!.Select(x => new EmpleadoRolProyectoDto(
            EmpleadoId: x.EmpleadoId,
            NombreCompleto: $"{x.Usuario.Nombre} {x.Usuario.Apellido}",
            Posicion: string.Empty,
            FotoPerfil: x.Usuario.FotoPerfil
        )).ToList();

        var totalCount = obtenerPaginas.TotalElementos;
        
        ResultadoPaginado<EmpleadoRolProyectoDto> resultado = new (resultadoPaginado, totalCount, solicitud.NumeroPagina, solicitud.TamanoPagina);

        if (!resultado.Elementos!.Any())
        {
            logger.LogWarning("No se encontraron empleados asociados a la empresa con ID: {EmpresaId}", empresaId);
            return ResultadoT<ResultadoPaginado<EmpleadoRolProyectoDto>>.Fallo(Error.Fallo("404", "No hay empleados asociados a la empresa"));
        }

        logger.LogInformation("Se obtuvieron {Cantidad} empleados de la empresa con ID: {EmpresaId}", resultado.Elementos?.Count(), empresaId);
        return ResultadoT<ResultadoPaginado<EmpleadoRolProyectoDto>>.Exito(resultado);
    }
}
