using Boardly.Aplicacion.DTOs.RolProyecto;
using Boardly.Dominio.Puertos.CasosDeUso.RolProyecto;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.RolProyecto;

public class ActualizarRolProyecto(
    ILogger<ActualizarRolProyecto> logger,
    IRolProyectoRepositorio repositorioRolProyecto
) : IActualizarRolProyecto<CrearRolProyectoDto, string>
{
    public async Task<ResultadoT<string>> ActualizarRolProyectoAsync(Guid id, CrearRolProyectoDto solicitud,
        CancellationToken cancellationToken)
    {
        var rolProyecto = await repositorioRolProyecto.ObtenerByIdAsync(id, cancellationToken);
        if (rolProyecto is null)
        {
            logger.LogWarning("No se encontró un rol de proyecto con el ID: {RolProyectoId}", id);
            
            return ResultadoT<string>.Fallo(Error.Fallo("400", "Este id del rol de proyecto no existe."));
        }

        var proyectoId = rolProyecto.EmpleadosProyectoRol.FirstOrDefault()?.ProyectoId;

        if (proyectoId == null)
        {
            logger.LogWarning("No se encontró un proyecto asociado al rol de proyecto. Id: {RolId}", id);
            
            return ResultadoT<string>.Fallo(Error.Fallo("400", "No se puede actualizar un rol sin proyecto asociado."));
        }
        
        var existeNombreDuplicado = await repositorioRolProyecto.ExisteNombreRolProyectoEnActualizacionAsync(id, (Guid)proyectoId, solicitud.Nombre, cancellationToken);;
        if (existeNombreDuplicado)
        {
            logger.LogWarning("Ya existe un rol de proyecto con el nombre: {Nombre}", solicitud.Nombre);
            
            return ResultadoT<string>.Fallo(Error.Conflicto("409", "Ya existe un rol de proyecto con ese nombre."));
        }

        if (existeNombreDuplicado)
        {
            logger.LogWarning("Ya existe un rol con el nombre '{Nombre}' en el mismo proyecto. RolId: {RolId}", solicitud.Nombre, id);
            
            return ResultadoT<string>.Fallo(Error.Fallo("400", $"Ya existe un rol con el nombre '{solicitud.Nombre}' en este proyecto."));
        }
        
        rolProyecto.Nombre = solicitud.Nombre;
        rolProyecto.Descripcion = solicitud.Descripcion;

        await repositorioRolProyecto.ActualizarAsync(rolProyecto, cancellationToken);

        return ResultadoT<string>.Exito("Rol de proyecto actualizado correctamente.");
    
    }
}