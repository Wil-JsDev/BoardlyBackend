using Asp.Versioning;
using Boardly.Aplicacion.DTOs.Ceo;
using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Dominio.Puertos.CasosDeUso.Ceo;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Microsoft.AspNetCore.Mvc;

namespace Boardly.Infraestructura.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/employee")]
public class EmpleadoControlador(
    ICrearCeo<CrearCeoDto, CeoDto> crearCeo,
    ICrearEmpleado<CrearEmpleadoDto, EmpleadoDto> crearEmpleado
    ) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CrearEmpleado([FromBody] CrearEmpleadoDto employee, CancellationToken cancellationToken)
    {
        var empleado = await crearEmpleado.CrearEmpleadoAsync(employee, cancellationToken);
        if (empleado.EsExitoso)
            return Ok(empleado.Valor);
             
        return BadRequest(empleado.Error);
    }
}