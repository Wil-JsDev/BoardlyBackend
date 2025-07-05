using Boardly.Dominio.Enum;
using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios;

public class TareaRepositorio(BoardlyContexto boardlyContexto) : GenericoRepositorio<Tarea>(boardlyContexto), ITareaRepositorio
{
    public async Task<bool> ExisteNombreTareaAsync(string nombreTarea, Guid proyectoId,
        CancellationToken cancellationToken)
    {
        return await ValidarAsync(x => x.Titulo == nombreTarea 
                                    && x.ProyectoId == proyectoId, 
                                     cancellationToken);
    }
        
    public async Task<bool> NombreTareaEnUso(string nombreTarea, Guid proyectoId, Guid tareaId, CancellationToken cancellationToken)
    {
        return await ValidarAsync(x => x.Titulo == nombreTarea 
                                    && x.ProyectoId == proyectoId
                                    && x.TareaId != tareaId, 
                                     cancellationToken);
    }
    
    public async Task<bool> ExisteTareaPorIdAsync(Guid tareaId, CancellationToken cancellationToken)
    {
        return await ValidarAsync(x => x.TareaId == tareaId, cancellationToken);
    }

    public async Task<List<Tarea>> ObtenerTareaPorIdDetalles(Guid tareaId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Tarea>()
            .AsNoTracking()
            .Where(x => x.TareaId == tareaId)
            .Include(t => t.TareasUsuario)
            .ThenInclude(us => us.Usuario)
                .ThenInclude(us => us.Empleado)
                    .ThenInclude(em => em!.EmpleadosProyectoRol)
                        .ThenInclude(emp => emp.RolProyecto.Nombre)
            .Include(dep => dep.Dependencias)
                .ThenInclude(depend => depend.Tarea)
            .Include(dep => dep.Dependientes)
                .ThenInclude(dep => dep.Tarea)
            .Include(t => t.Comentarios)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<List<Tarea>> ObtenerTareasPorUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Tarea>()
            .AsNoTracking()
            .Where(x => x.TareasUsuario.Any(t => t.UsuarioId == usuarioId))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Tarea>> ObtenerTareasPorProyectoIdAsync(Guid proyectoId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Tarea>()
            .AsNoTracking()
            .Where(x => x.ProyectoId == proyectoId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Tarea>> ObtenerTareasPorProyectoIdYEstadoAsync(Guid proyectoId, EstadoTarea estado, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Tarea>()
            .AsNoTracking()
            .Where(x => x.ProyectoId == proyectoId && x.Estado == estado.ToString())
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Tarea>> ObtenerPorFechaCreacionAsync(Guid proyectoId,
        CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Tarea>()
            .AsNoTracking()
            .Where(x => x.ProyectoId == proyectoId)
            .OrderBy(x => x.FechaCreado)
            .Include(t => t.TareasUsuario)
                .ThenInclude(us => us.Usuario)
            .ThenInclude(us => us.NombreUsuario)
            .Include(dep => dep.Dependencias)
            .ThenInclude(depend => depend.Tarea)
            .Include(dep => dep.Dependientes)
            .ThenInclude(dep => dep.Tarea)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }

    // Tareas con fecha de vencimiento con tres opciones: Hoy, esta semana y 24H
    public async Task<List<Tarea>> ObtenerPorFechaVencimientoAsync(Guid proyectoId, DateTime desde, DateTime hasta,
        CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Tarea>()
            .AsNoTracking()
            .Where(x => x.ProyectoId == proyectoId && x.FechaVencimiento >= desde && x.FechaVencimiento <= hasta)
            .Include(t => t.TareasUsuario)
            .ThenInclude(us => us.Usuario)
            .ThenInclude(us => us.NombreUsuario)
            .Include(dep => dep.Dependencias)
            .ThenInclude(depend => depend.Tarea)
            .Include(dep => dep.Dependientes)
            .ThenInclude(dep => dep.Tarea)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExisteDependenciaCircularAsync(Guid tareaId, Guid dependeDeId, CancellationToken cancellationToken)
    {
        if (tareaId == dependeDeId)
            return false;
        
        var visitados = new HashSet<Guid>();
        return await BuscarCicloAsync(tareaId, dependeDeId, visitados, cancellationToken);
    }

    #region Metodos privados
        private async Task<bool> BuscarCicloAsync(
            Guid actual,
            Guid objetivo,
            HashSet<Guid> visitados,
            CancellationToken cancellationToken)
        {
            if (visitados.Contains(actual))
                return false;

            visitados.Add(actual);

            // Buscamos todas las tareas de las que depende "actual"
            var dependencias = await _boardlyContexto.Set<TareaDependencia>()
                .AsNoTracking()
                .Where(td => td.TareaId == actual)
                .Select(td => td.DependeDeId)
                .ToListAsync(cancellationToken);

            foreach (var dependeDe in dependencias)
            {
                if (dependeDe == objetivo)
                    return true; // Se encontró un ciclo

                if (await BuscarCicloAsync(dependeDe, objetivo, visitados, cancellationToken))
                    return true;
            }

            return false;
        }
        
    #endregion
}