using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Servicios;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Servicios;

public class ObtenerRoles(BoardlyContexto contexto ) : IObtenerRoles
{
    public async Task<IList<string>> ObtenerRolesAsync(Guid usuarioId, CancellationToken cancellationToken)
    {
        var roles = new List<string>();

        var esEmpleado = await contexto.Set<Empleado>()
            .AsNoTracking()
            .AnyAsync(e => e.UsuarioId == usuarioId, cancellationToken);
        
        if (esEmpleado)
            roles.Add("Empleado");

        var esCeo = await contexto.Set<Ceo>()
            .AsNoTracking()
            .AnyAsync(c => c.UsuarioId == usuarioId, cancellationToken);
        
        if (esCeo)
            roles.Add("Ceo");

        // Roles asignados en proyectos
        var rolesProyecto = await contexto.Set<EmpleadoProyectoRol>()
            .AsNoTracking()
            .Where(epr => epr.Empleado.UsuarioId == usuarioId)
            .Select(epr => epr.RolProyecto.Nombre)
            .Distinct()
            .ToListAsync(cancellationToken);

        roles.AddRange(rolesProyecto);

        return roles.Distinct().ToList();
    }
}