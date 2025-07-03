using Asp.Versioning;
using Boardly.Aplicacion.DTOs.Empresa;
using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Dominio.Puertos.CasosDeUso.Empresa;
using Microsoft.AspNetCore.Mvc;

namespace Boardly.Infraestructura.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/companies")]
public class EmpresaControlador(
    ICrearEmpresa<CrearEmpresaDto, EmpresaDto> crearEmpresa,
    IActualizarEmpresa<ActualizarEmpresaDto, ActualizarEmpresaDto> actualizarEmpresa,
    IBorrarEmpresa borrarEmpresa,
    IResultadoPaginaEmpresa<PaginacionParametro, EmpresaDto> resultadoPaginaEmpresa
    ) : ControllerBase
{
   
    [HttpPost]
    public async Task<IActionResult> CrearEmpresa([FromBody] CrearEmpresaDto empresa, CancellationToken cancellationToken)
    {
        var resultado = await crearEmpresa.CrearEmpresaAsync(empresa, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }

    [HttpPut("{companyId}")]
    public async Task<IActionResult> ActualizarEmpresa([FromRoute] Guid companyId,
        [FromBody] ActualizarEmpresaDto empresa, CancellationToken cancellationToken)
    {
        var resultado = await actualizarEmpresa.ActualizarEmpresaAsync(companyId, empresa, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }
    
    [HttpDelete("{companyId}")]
    public async Task<IActionResult> BorrarEmpresa([FromRoute] Guid companyId, CancellationToken cancellationToken)
    {
        var resultado = await borrarEmpresa.BorrarEmpresaAsync(companyId, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return NotFound(resultado.Error);
    }

    [HttpGet("pagination")]
    public async Task<IActionResult> ResultadoPaginaEmpresa(
        [FromQuery] int numeroPagina,
        [FromQuery] int tamanoPagina,
        CancellationToken cancellationToken)
    {
        PaginacionParametro parametros = new(numeroPagina, tamanoPagina);
        var resultado = await resultadoPaginaEmpresa.ObtenerPaginacionEmpresaAsync(parametros, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }
    
}