using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios;

public class CodigoRepositorio(BoardlyContexto contexto): GenericoRepositorio<Codigo>(contexto), ICodigoRepositorio
{
    public async Task CrearCodigoAsync(Codigo codigo, CancellationToken cancellationToken)
    {
        await _boardlyContexto.Set<Codigo>().AddAsync(codigo, cancellationToken);
        await _boardlyContexto.SaveChangesAsync(cancellationToken);
    }

    public async Task<Codigo> ObtenerCodigoPorIdAsync(Guid id, CancellationToken cancellationToken) =>
        (await _boardlyContexto.Set<Codigo>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CodigoId == id,cancellationToken))!;
    

    public async Task<Codigo> BuscarCodigoAsync(string codigo, CancellationToken cancellationToken) =>
        (await _boardlyContexto.Set<Codigo>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Valor == codigo, cancellationToken))!;

    public async Task EliminarCodigoAsync(Codigo codigo, CancellationToken cancellationToken)
    {
        _boardlyContexto.Set<Codigo>().Remove(codigo);
        await GuardarAsync(cancellationToken);
    }

    public async Task<bool> ExisteElCodigoAsync(string codigo, CancellationToken cancellationToken) =>
        await ValidarAsync(c => c.Valor == codigo, cancellationToken);
    
    public async Task<bool> ElCodigoEsValidoAsync(string codigo, CancellationToken cancellationToken) =>
        await ValidarAsync(c => c.Valor == codigo &&
                                c.Expiracion > DateTime.UtcNow &&
                                !c.Usado!.Value, cancellationToken);

    public async Task MarcarCodigoComoUsado(string codigo, CancellationToken cancellationToken)
    {
        var usuarioCodigo = await _boardlyContexto.Set<Codigo>()
            .FirstOrDefaultAsync(c => c.Valor == codigo, cancellationToken);

        if (usuarioCodigo != null)
        {
            usuarioCodigo.Usado = true;
            await GuardarAsync(cancellationToken);
        }    
    }

    public async Task<bool> CodigoNoUsadoAsync(string codigo, CancellationToken cancellationToken) =>
        await ValidarAsync(c => c.Valor == codigo && c.Usado!.Value, cancellationToken);

}