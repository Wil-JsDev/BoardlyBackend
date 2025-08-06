using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Dominio.Helper;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empleados;

public class ResultadoPaginadoEmpleado(
    ILogger<ResultadoPaginadoEmpleado> logger,
    IEmpleadoRepositorio empleadoRepositorio,
    IDistributedCache cache
    ) : IResultadoPaginadoEmpleado<PaginacionParametro,  EmpleadoDto>
{
    public async Task<ResultadoT<ResultadoT<ResultadoPaginado<EmpleadoDto>>>> ObtenerPaginacionEmpleadoAsync(PaginacionParametro solicitud, CancellationToken cancellationToken)
    {
        if (solicitud.TamanoPagina <= 0 || solicitud.NumeroPagina <= 0)
        {
            logger.LogWarning("Parametros invalidos de paginacion. TamanoPagina: {TamanoPagina}, NumeroPagina: {NumeroPagina}",
                solicitud.TamanoPagina, solicitud.NumeroPagina);

            return ResultadoT<ResultadoPaginado<EmpleadoDto>>.Fallo(
                Error.Fallo("400", "Los parametros de paginacion deben ser mayores a cero.")
            );
        }

        var resultadoPagina = await empleadoRepositorio.ObtenerPaginadoAsync(solicitud.NumeroPagina,
            solicitud.TamanoPagina,
            cancellationToken);

        var resultadoPaginaDto = await cache.ObtenerOCrearAsync(
            $"obtener-paginado-empleado-{solicitud.NumeroPagina}-{solicitud.TamanoPagina}",
            async () =>
            {
                var dtoList = resultadoPagina.Elementos!.Select(x => new EmpleadoDto
                (
                    EmpleadoId: x.EmpleadoId,
                    UsuarioId: x.UsuarioId,
                    EmpresaId: x.EmpresaId
                )).ToList();
              
                var totalElementos = dtoList.Count();
                
                var elementosPaginados = dtoList
                    .Paginar(solicitud.NumeroPagina, solicitud.TamanoPagina)
                    .ToList();
                
                return  new ResultadoPaginado<EmpleadoDto>(
                    elementos: elementosPaginados,
                    totalElementos: totalElementos,
                    paginaActual: solicitud.NumeroPagina,
                    tamanioPagina: solicitud.TamanoPagina
                );
            }
        );
        
        logger.LogInformation("Se obtuvo la pagina {NumeroPagina} de empleados con exito. Cantidad de empleados en esta pagina: {CantidadEmpleado}",
            solicitud.NumeroPagina, resultadoPaginaDto.Elementos!.Count());

        return ResultadoT<ResultadoPaginado<EmpleadoDto>>.Exito(resultadoPaginaDto);
    }
}