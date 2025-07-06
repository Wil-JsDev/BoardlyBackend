using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios.Cuentas;

public class CeoRepositorio(BoardlyContexto boardlyContexto) : GenericoRepositorio<Ceo>(boardlyContexto), ICeoRepositorio
{
    public async Task<Guid> ObtenerIdCeoAsync(Guid usuarioId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Ceo>()
            .AsNoTracking()
            .Where(u => u.UsuarioId == usuarioId).Select(c => c.CeoId).FirstOrDefaultAsync(cancellationToken);
    }
}