using Boardly.Dominio.Enum;
using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios;

public class ProyectoRepositorio(BoardlyContexto boardlyContexto) : GenericoRepositorio<Proyecto>(boardlyContexto), IProyectoRepositorio
{

    public async Task<ResultadoPaginado<Proyecto>> ProyectosFinalizados(Guid empresaId,
        int numeroPagina,
        int tamanoPagina,
        CancellationToken cancellationToken)
    {
        var consulta = _boardlyContexto.Set<Proyecto>()
            .Where(p => p.EmpresaId == empresaId)
            .Where(p => p.Estado == EstadoProyecto.Finalizado.ToString())
            .OrderByDescending(p => p.FechaCreado) 
            .AsNoTracking();
        
        var total = await consulta.CountAsync(cancellationToken);
        
        var proyecto = await consulta
            .Skip((numeroPagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync(cancellationToken);

        return new ResultadoPaginado<Proyecto>(proyecto, total, numeroPagina, tamanoPagina);
    }
    
    public async Task<ResultadoPaginado<Proyecto>> ObtenerPaginasProyectoAsync(Guid empresaId, int numeroPagina, int tamanoPagina,
        CancellationToken cancellationToken)
    {
        var consulta = _boardlyContexto.Set<Proyecto>()
            .Where(p => p.EmpresaId == empresaId)
            .OrderByDescending(p => p.FechaCreado) 
            .AsNoTracking();
        
        var total = await consulta.CountAsync(cancellationToken);
        
        var proyecto = await consulta
            .Skip((numeroPagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync(cancellationToken);
        
        return new ResultadoPaginado<Proyecto>(proyecto, total, numeroPagina, tamanoPagina);   
    }

    public async Task<Proyecto> ObtenerProyectoEmpleadosPorIdAsync(Guid proyectoId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Proyecto>()
            .Include(p => p.EmpleadosProyectoRol)
            .FirstOrDefaultAsync(p => p.ProyectoId == proyectoId, cancellationToken);
    }

    public async Task<bool> ExisteProyectoAsync(string nombre, CancellationToken cancellationToken)
    {
        return await ValidarAsync(p => p.Nombre == nombre, cancellationToken);
    }

    public async Task<bool> NombreProyectoEnUsoAsync(Guid proyectoId, string nombre, CancellationToken cancellationToken)
    {
        return await ValidarAsync(p => p.ProyectoId != proyectoId && p.Nombre == nombre, cancellationToken);
    }
    
    public async Task<int> ObtenerConteoDeActividadesProyectosAsync(Guid proyectoId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Actividad>()
            .AsNoTracking()
            .Where(a => a.ProyectoId == proyectoId)
            .CountAsync(cancellationToken);
    }

    public async Task<int> ObtenerConteoDeTareasProyectosAsync(Guid proyectoId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Tarea>()
            .AsNoTracking()
            .Where(t => t.ProyectoId == proyectoId)
            .CountAsync(cancellationToken);
    }

    public async Task<int> ObtenerConteoDeTareasCompletadasProyectosAsync(Guid proyectoId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Tarea>()
            .Where(u => u.Estado == EstadoTarea.Finalizada.ToString())
            .Where(t => t.ProyectoId == proyectoId)
            .CountAsync(cancellationToken);
    }
    
    public async Task<int> ObtenerConteoDeTareasPendienteProyectosAsync(Guid proyectoId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Tarea>()
            .Where(u => u.Estado == EstadoTarea.Pendiente.ToString())
            .Where(t => t.ProyectoId == proyectoId)
            .CountAsync(cancellationToken);
    }

   
    
}