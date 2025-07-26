using Boardly.Dominio.Enum;
using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios.Cuentas;

public class EmpleadoRepositorio(BoardlyContexto boardlyContexto) : GenericoRepositorio<Empleado>(boardlyContexto), IEmpleadoRepositorio
{
    public async Task<IEnumerable<Empleado>> ObtenerPorEmpresaIdAsync(Guid empresaId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Empleado>()
            .AsNoTracking()
            .Include(e => e.Usuario)
            .Where(e => e.EmpresaId == empresaId)
            .ToListAsync(cancellationToken);
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
}