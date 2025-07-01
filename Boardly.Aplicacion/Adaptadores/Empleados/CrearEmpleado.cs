using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empleados;

public class CrearEmpleado(
    ILogger<CrearEmpleado> logger,
    IEmpleadoRepositorio empleadoRepositorio,
    IUsuarioRepositorio usuarioRepositorio
    ) : ICrearEmpleado<CrearEmpleadoDto, EmpleadoDto>
{
    public async Task<ResultadoT<EmpleadoDto>> CrearEmpleadoAsync(CrearEmpleadoDto? solicitud, CancellationToken cancellationToken)
    {
        if (solicitud is null)
        {
            logger.LogWarning("La solicitud para crear un empleado es nula.");
            
            return ResultadoT<EmpleadoDto>.Fallo(Error.Fallo("400", "La solicitud es inválida o nula."));
        }
    
        var usuario = await usuarioRepositorio.ObtenerByIdAsync(solicitud.UsuarioId, cancellationToken);
        if (usuario is null)
        {
            logger.LogWarning("No se encontró un usuario con el ID proporcionado: {UsuarioId}", solicitud.UsuarioId);
            
            return ResultadoT<EmpleadoDto>.Fallo(Error.NoEncontrado("404", "Usuario no encontrado."));
        }

        Empleado empleadoEntidad = new()
        {
            EmpleadoId = Guid.NewGuid(),
            UsuarioId = solicitud.UsuarioId
        };

        await empleadoRepositorio.CrearAsync(empleadoEntidad, cancellationToken);
    
        logger.LogInformation("Empleado creado exitosamente con ID: {EmpleadoId} para el Usuario ID: {UsuarioId}", empleadoEntidad.EmpleadoId, empleadoEntidad.UsuarioId);

        EmpleadoDto empleadoDto = new
        (
            EmpleadoId: empleadoEntidad.EmpleadoId,
            UsuarioId: empleadoEntidad.UsuarioId
        );
    
        return ResultadoT<EmpleadoDto>.Exito(empleadoDto);
    }

}