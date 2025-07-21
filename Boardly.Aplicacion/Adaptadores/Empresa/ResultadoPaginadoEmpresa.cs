using Boardly.Aplicacion.DTOs.Empresa;
using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Dominio.Helper;
using Boardly.Dominio.Puertos.CasosDeUso.Empresa;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empresa;

public class ResultadoPaginadoEmpresa(
    ILogger<ResultadoPaginadoEmpresa> logger,
    IEmpresaRepositorio empresaRepositorio,
    IDistributedCache cache
) : IResultadoPaginaEmpresa<PaginacionParametro, EmpresaDto>
{
    public async Task<ResultadoT<ResultadoPaginado<EmpresaDto>>> ObtenerPaginacionEmpresaAsync(Guid ceoId,PaginacionParametro solicitud, CancellationToken cancellationToken)
    {
        if (solicitud.TamanoPagina <= 0 || solicitud.NumeroPagina <= 0)
        {
            logger.LogWarning("Parametros invalidos de paginacion. TamanoPagina: {TamanoPagina}, NumeroPagina: {NumeroPagina}",
                solicitud.TamanoPagina, solicitud.NumeroPagina);

            return ResultadoT<ResultadoPaginado<EmpresaDto>>.Fallo(
                Error.Fallo("400", "Los parametros de paginacion deben ser mayores a cero.")
            );
        }

        var resultadoPagina = await empresaRepositorio.ObtenerPaginasEmpresaAsync(ceoId, solicitud.NumeroPagina,
            solicitud.TamanoPagina, cancellationToken);
        
        if (resultadoPagina.Elementos is null || !resultadoPagina.Elementos.Any())
        {
            logger.LogWarning("No se encontraron empresas en la pagina {NumeroPagina} con tamano {TamanoPagina}.",
                solicitud.NumeroPagina, solicitud.TamanoPagina);

            return ResultadoT<ResultadoPaginado<EmpresaDto>>.Fallo(
                Error.Fallo("404", "No se encontraron empresas para los parametros de paginacion especificados.")
            );
        }

        var empresaDtoList = await cache.ObtenerOCrearAsync(
            $"obtener-paginacion-empresa-{solicitud.TamanoPagina}-{solicitud.NumeroPagina}",
            async () =>
            {
                var dtoList = resultadoPagina.Elementos.Select(empresaEntidad => new EmpresaDto
                (
                    EmpresaId: empresaEntidad.EmpresaId, 
                    CeoId: empresaEntidad.CeoId,
                    Nombre: empresaEntidad.Nombre,
                    Descripcion: empresaEntidad.Descripcion,
                    FechaCreacion: empresaEntidad.FechaCreacion,
                    Estado: empresaEntidad.Estado
                )).ToList();
                
                var totalElementos = dtoList.Count;
                
                var elementosPaginados = dtoList
                    .Paginar(solicitud.NumeroPagina, solicitud.TamanoPagina)
                    .ToList();
                
                return new ResultadoPaginado<EmpresaDto>(
                    elementos: elementosPaginados,
                    totalElementos: totalElementos,
                    paginaActual: solicitud.NumeroPagina,
                    tamanioPagina: solicitud.TamanoPagina
                );
            },
            cancellationToken: cancellationToken
        );

        logger.LogInformation("Se obtuvo la pagina {NumeroPagina} de empresas con exito. Cantidad de empresas en esta pagina: {CantidadEmpresas}",
            solicitud.NumeroPagina, empresaDtoList.Elementos!.Count());

        return ResultadoT<ResultadoPaginado<EmpresaDto>>.Exito(empresaDtoList);
    }
}
