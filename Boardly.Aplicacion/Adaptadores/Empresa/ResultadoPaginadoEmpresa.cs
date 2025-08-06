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
    ICeoRepositorio ceoRepositorio,
    IDistributedCache cache
) : IResultadoPaginaEmpresa<PaginacionParametro, EmpresaProyectosDto>
{
    public async Task<ResultadoT<ResultadoPaginado<EmpresaProyectosDto>>> ObtenerPaginacionEmpresaAsync(Guid ceoId,PaginacionParametro solicitud, CancellationToken cancellationToken)
    {
        if (solicitud.TamanoPagina <= 0 || solicitud.NumeroPagina <= 0)
        {
            logger.LogWarning("Parametros invalidos de paginacion. TamanoPagina: {TamanoPagina}, NumeroPagina: {NumeroPagina}",
                solicitud.TamanoPagina, solicitud.NumeroPagina);

            return ResultadoT<ResultadoPaginado<EmpresaProyectosDto>>.Fallo(
                Error.Fallo("400", "Los parametros de paginacion deben ser mayores a cero.")
            );
        }

        var ceo = await ceoRepositorio.ObtenerByIdAsync(ceoId, cancellationToken);
        if (ceo is null)
        {
            logger.LogWarning("El CEO con id {CeoId} no existe.", ceoId);
            
            return ResultadoT<ResultadoPaginado<EmpresaProyectosDto>>.Fallo(
                Error.Fallo("400", "El CEO con id especificado no existe.")
            );
        }
        
        var resultadoPagina = await empresaRepositorio.ObtenerPaginasEmpresaAsync(ceoId, solicitud.NumeroPagina,
            solicitud.TamanoPagina, cancellationToken);
        
        if (resultadoPagina.Elementos is null || !resultadoPagina.Elementos.Any())
        {
            logger.LogWarning("No se encontraron empresas en la pagina {NumeroPagina} con tamano {TamanoPagina}.",
                solicitud.NumeroPagina, solicitud.TamanoPagina);

            return ResultadoT<ResultadoPaginado<EmpresaProyectosDto>>.Fallo(
                Error.Fallo("404", "No se encontraron empresas para los parametros de paginacion especificados.")
            );
        }

        var empresaDtoList = await cache.ObtenerOCrearAsync(
            $"obtener-paginacion-empresa-{solicitud.TamanoPagina}-{solicitud.NumeroPagina}",
            async () =>
            {
                var empresas = resultadoPagina.Elementos.ToList();
                var dtoList = new List<EmpresaProyectosDto>();

                foreach (var empresa in empresas)
                {
                    var totalEmpleados = await empresaRepositorio.ObtenerConteoDeEmpleadosPorEmpresaIdAsync(empresa.EmpresaId, cancellationToken);
                    var totalProyectos = await empresaRepositorio.ObtenerConteoDeProyectosPorEmpresaAsync(empresa.EmpresaId, cancellationToken);

                    var dto = new EmpresaProyectosDto(
                        EmpresaId: empresa.EmpresaId,
                        CeoId: empresa.CeoId,
                        Nombre: empresa.Nombre,
                        Descripcion: empresa.Descripcion,
                        FechaCreacion: empresa.FechaCreacion,
                        Estado: empresa.Estado,
                        EmpresaConteo: new EmpresaConteoDto(
                            TotalEmpleados: totalEmpleados,
                            TotalProyectos: totalProyectos
                        )
                    );

                    dtoList.Add(dto);
                }

                return new ResultadoPaginado<EmpresaProyectosDto>(
                    elementos: dtoList,
                    totalElementos: resultadoPagina.TotalElementos,
                    paginaActual: solicitud.NumeroPagina,
                    tamanioPagina: solicitud.TamanoPagina
                );
            },
            cancellationToken: cancellationToken
        );


        logger.LogInformation("Se obtuvo la pagina {NumeroPagina} de empresas con exito. Cantidad de empresas en esta pagina: {CantidadEmpresas}",
            solicitud.NumeroPagina, empresaDtoList.Elementos!.Count());

        return ResultadoT<ResultadoPaginado<EmpresaProyectosDto>>.Exito(empresaDtoList);
    }
}
