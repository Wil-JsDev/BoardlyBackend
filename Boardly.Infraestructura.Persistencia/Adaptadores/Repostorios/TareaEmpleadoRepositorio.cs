using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios;

public class TareaEmpleadoRepositorio(BoardlyContexto boardlyContexto) : GenericoRepositorio<TareaEmpleado>(boardlyContexto), ITareaEmpleadoRepositorio
{
    public async Task CrearTareasEmpleadosAsync(List<TareaEmpleado> tareasEmpleados, CancellationToken cancellationToken)
    {
        await _boardlyContexto.AddRangeAsync(tareasEmpleados, cancellationToken);
        await GuardarAsync(cancellationToken);   
    }

    public async Task<TareaEmpleado?> ObtenerEmpleadoIdsAsync(IEnumerable<Guid> empleadosIds, CancellationToken cancellationToken)
    {
        var consulta = await _boardlyContexto.Set<TareaEmpleado>()
            .AsNoTracking()
            .Where(t => empleadosIds.Contains(t.EmpleadoId))
            .FirstOrDefaultAsync(cancellationToken);
        
        return consulta;   
    }
    
    public async Task ActualizarTareasEmpleadosAsync(Guid tareaId, List<Guid> nuevosEmpleadoIds, CancellationToken cancellationToken)
    {
        // 1. Eliminar las relaciones actuales
        var relacionesActuales = await _boardlyContexto.Set<TareaEmpleado>()
            .Where(te => te.TareaId == tareaId)
            .ToListAsync(cancellationToken);

        _boardlyContexto.Set<TareaEmpleado>().RemoveRange(relacionesActuales);

        // 2. Crear las nuevas relaciones
        var nuevasRelaciones = nuevosEmpleadoIds.Select(empleadoId => new TareaEmpleado
        {
            TareaId = tareaId,
            EmpleadoId = empleadoId
        }).ToList();

        await _boardlyContexto.Set<TareaEmpleado>().AddRangeAsync(nuevasRelaciones, cancellationToken);

        // 3. Guardar cambios
        await GuardarAsync(cancellationToken);
    }

    
}