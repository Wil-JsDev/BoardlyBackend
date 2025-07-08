using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Servicios;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Servicios;

public class ObtenerEmpleadoId(BoardlyContexto boardlyContexto) : IObtenerEmpleadoId
{
    public async Task<Guid?> ObtenerEmpleadoIdAsync(Guid usuarioId, CancellationToken cancellationToken)
    {
        var empleado = await boardlyContexto.Set<Empleado>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.UsuarioId == usuarioId, cancellationToken);

        return empleado?.EmpleadoId;
    }
}