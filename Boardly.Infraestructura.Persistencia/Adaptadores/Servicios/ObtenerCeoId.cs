using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Servicios;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Servicios;

public class ObtenerCeoId(BoardlyContexto boardlyContexto) : IObtenerCeoId
{
    public async Task<Guid?> ObtenerCeoIdAsync(Guid usuarioId, CancellationToken cancellationToken)
    {
        var ceo = await boardlyContexto.Set<Ceo>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.UsuarioId == usuarioId, cancellationToken);
        
        return ceo?.CeoId;
    }
}