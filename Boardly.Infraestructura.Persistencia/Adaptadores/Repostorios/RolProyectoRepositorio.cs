using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios;

public class RolProyectoRepositorio(BoardlyContexto boardlyContexto) : GenericoRepositorio<RolProyecto>(boardlyContexto), IRolProyectoRepositorio
{
    public async Task<bool> ExisteNombreRolProyectoAsync(Guid proyectoId, string nombreEmpresa, CancellationToken cancellationToken)
    {
        return await ValidarAsync(rp => 
            rp.Nombre == nombreEmpresa &&
            rp.EmpleadosProyectoRol.Any(e => e.ProyectoId == proyectoId),
            cancellationToken
            );
    }
    
    public async Task<bool> ExisteNombreRolProyectoEnActualizacionAsync(Guid rolProyectoId, Guid proyectoId, string nombreRol, CancellationToken cancellationToken)
    {
        return await ValidarAsync(rp =>
                    rp.RolProyectoId != rolProyectoId &&
                    rp.Nombre == nombreRol &&
                    rp.EmpleadosProyectoRol.Any(epr => epr.ProyectoId == proyectoId),
                cancellationToken);
    }

    public async Task<Guid> ObtenerIdPorNombreAsync(string nombre, CancellationToken cancellationToken)
    {
        var rol = await _boardlyContexto.Set<RolProyecto>()
            .AsNoTracking()
            .Where(r => r.Nombre.Equals(nombre, StringComparison.CurrentCultureIgnoreCase))
            .FirstOrDefaultAsync(cancellationToken);

        return rol?.RolProyectoId ?? Guid.Empty;
    }
    
    public async Task<ResultadoPaginado<RolProyecto>> ObtenerPaginasRolProyectoAsync(Guid proyectoId, int numeroPagina, int tamanoPagina,
        CancellationToken cancellationToken)
    {
        var consulta = _boardlyContexto.Set<RolProyecto>()
            .AsNoTracking()
            .Where(x => x.EmpleadosProyectoRol.Any(empleadoProyectoRol => empleadoProyectoRol.ProyectoId == proyectoId));
        
        var total = await consulta.CountAsync(cancellationToken);
        
        var rolProyecto = await consulta
            .Skip((numeroPagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync(cancellationToken);
        
        return new ResultadoPaginado<RolProyecto>(rolProyecto, total, numeroPagina, tamanoPagina);   
    }
}