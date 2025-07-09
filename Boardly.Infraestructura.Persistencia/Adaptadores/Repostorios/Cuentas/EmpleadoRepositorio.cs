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
}