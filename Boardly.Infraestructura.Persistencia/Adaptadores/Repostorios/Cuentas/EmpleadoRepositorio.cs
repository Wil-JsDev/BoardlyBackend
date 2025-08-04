using Boardly.Dominio.Enum;
using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios.Cuentas;

public class EmpleadoRepositorio(BoardlyContexto boardlyContexto) : GenericoRepositorio<Empleado>(boardlyContexto), IEmpleadoRepositorio
{
    public async Task<IEnumerable<Empleado>> ObtenerPorEmpresaIdAsync(Guid empresaId,
        CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Empleado>()
            .Where(e => e.EmpresaId == empresaId)
            .Include(e => e.Usuario)
            .Include(e => e.EmpleadosProyectoRol)
            .ThenInclude(epr => epr.RolProyecto)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Empleado?> ObtenerEmpleadoByIdAsync(Guid empleadoId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Empleado>()
            .Include(e => e.Usuario)
            .Include(e => e.EmpleadosProyectoRol)
            .FirstOrDefaultAsync(e => e.EmpleadoId == empleadoId, cancellationToken);
    }

    public async Task<int> BorrarEmpleadoDeUnProyectoAsync(Guid empleadoId, Guid proyectoId, CancellationToken cancellationToken)
    {
        var empleado = await _boardlyContexto.Set<Empleado>()
            .Include(e => e.EmpleadosProyectoRol)
            .ThenInclude(epr => epr.RolProyecto)
            .FirstOrDefaultAsync(e => e.EmpleadoId == empleadoId, cancellationToken);

        var relacionesAEliminar = empleado.EmpleadosProyectoRol
            .Where(epr => epr.RolProyecto.ProyectoId == proyectoId)
            .ToList();

        _boardlyContexto.EmpleadoProyectoRol.RemoveRange(relacionesAEliminar);
        return await _boardlyContexto.SaveChangesAsync(cancellationToken);
    }


    public async Task<int> ObtenerConteoDeEmpresasQuePerteneceAsync(Guid empleadoId,
        CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Empresa>()
            .AsNoTracking()
            .Where(e => e.Empleados.Any(empleado => empleado.EmpleadoId == empleadoId))
            .CountAsync(cancellationToken);
    }

    public async Task<int> ObtenerConteoDeProyectosQuePerteneceAsync(Guid empleadoId,
        CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<EmpleadoProyectoRol>()
            .AsNoTracking()
            .Where(x => x.EmpleadoId == empleadoId)
            .CountAsync(cancellationToken);
    }

    public async Task<int> ObtenerConteoDeActividadesQuePerteneceAsync(Guid empleadoId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Actividad>()
            .AsNoTracking()
            .Where(x => x.Tareas
                .Any(t => t.TareasEmpleado!
                    .Any(te => te.EmpleadoId == empleadoId)))
            .CountAsync(cancellationToken);
    }

    public async Task<int> ObtenerConteoDeTareasQuePerteneceAsync(Guid empleadoId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Tarea>()
            .AsNoTracking()
            .Where(x => x.TareasEmpleado!
                .Any(te => te.EmpleadoId == empleadoId))
            .CountAsync(cancellationToken);
    }

    public async Task<int> ObtenerConteoDeTareasEnProcesoQuePertenceAsync(Guid empleadoId,
        CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Tarea>()
            .AsNoTracking()
            .Where(x => x.Estado == EstadoTarea.EnProceso.ToString())
            .Where(x => x.TareasEmpleado!
                .Any(tareaEmpleado => tareaEmpleado.EmpleadoId == empleadoId))
            .CountAsync(cancellationToken);
    }

    public async Task<int> ObtenerConteoDeTareasAVencerAsync(Guid empleadoId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Tarea>()
            .AsNoTracking()
            .Where(x => x.FechaVencimiento <= DateTime.UtcNow)
            .Where(x => x.Estado == EstadoTarea.EnProceso.ToString())
            .Where(x => x.TareasEmpleado!
                .Any(tareaEmpleado => tareaEmpleado.EmpleadoId == empleadoId))
            .CountAsync(cancellationToken);
    }
    public async Task<ResultadoPaginado<Empleado>> ObtenerPaginasEmpleadoProyectoIdAsync(Guid proyectoId,
        int numeroPagina,
        int tamanoPagina,
        CancellationToken cancellationToken)
    {
        var consulta = await _boardlyContexto.Set<Empleado>()
            .AsNoTracking()
            .Where(e => e.EmpleadosProyectoRol.Any(empleadoProyectoRol => empleadoProyectoRol.ProyectoId == proyectoId))
            .Include(em => em.Usuario)
            .Include(em => em.EmpleadosProyectoRol)
                .ThenInclude(epr => epr.RolProyecto)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
        
        var totalCount = consulta.Count;
        
        var empleados = consulta
            .Skip((numeroPagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToList();
        
        return new ResultadoPaginado<Empleado>(empleados, totalCount, numeroPagina, tamanoPagina);   
    }

    public async Task<ResultadoPaginado<Empleado>> ObtenerPaginasEmpleadoEmpresaId(Guid empresaId, int numeroPagina, int tamanoPagina,
        CancellationToken cancellationToken)
    {
        var consulta = _boardlyContexto.Set<Empleado>()
            .AsNoTracking()
            .Where(e => e.EmpresaId == empresaId)
            .Include(e => e.Usuario);

        var totalCount = await consulta.CountAsync(cancellationToken);

        var empleados = await consulta
            .Skip((numeroPagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync(cancellationToken);

        return new ResultadoPaginado<Empleado>(empleados, totalCount, numeroPagina, tamanoPagina);
    }
}