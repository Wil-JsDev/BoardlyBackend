using System.Linq.Expressions;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios;

public class GenericoRepositorio<TEntidad>(BoardlyContexto boardlyContexto) : IGenericoRepositorio<TEntidad>
    where TEntidad : class
{
    protected readonly BoardlyContexto _boardlyContexto = boardlyContexto;

    public async Task<TEntidad> ObtenerByIdAsync(Guid id, CancellationToken cancellationToken) =>
    (await _boardlyContexto.Set<TEntidad>().FindAsync(id, cancellationToken))!;

    public async Task<ResultadoPaginado<TEntidad>> ObtenerPaginadoAsync(int numeroPagina, int tamanioPagina, CancellationToken cancellationToken)
    {
        var total = await _boardlyContexto.Set<TEntidad>().AsNoTracking().CountAsync(cancellationToken);
        
        var entidad = await _boardlyContexto.Set<TEntidad>().AsNoTracking()
            .Skip((numeroPagina - 1) * tamanioPagina)
            .Take(tamanioPagina)
            .ToListAsync(cancellationToken);
        
        return new ResultadoPaginado<TEntidad>(entidad, total, numeroPagina, tamanioPagina);
    }

    public async Task CrearAsync(TEntidad entidad, CancellationToken cancellationToken)
    {
        await _boardlyContexto.Set<TEntidad>().AddAsync(entidad, cancellationToken);
        await GuardarAsync(cancellationToken);
    }

    public async Task ActualizarAsync(TEntidad entidad, CancellationToken cancellationToken)
    {
        _boardlyContexto.Set<TEntidad>().Attach(entidad);
        _boardlyContexto.Entry(entidad).State = EntityState.Modified;
        await GuardarAsync(cancellationToken);
    }

    public async Task EliminarAsync(TEntidad entidad, CancellationToken cancellationToken)
    {
        _boardlyContexto.Remove(entidad);
        await GuardarAsync(cancellationToken);
    }

    public async Task<bool> ValidarAsync(Expression<Func<TEntidad, bool>> predicate, CancellationToken cancellationToken) => 
        await _boardlyContexto.Set<TEntidad>()
            .AsNoTracking()
            .AnyAsync(predicate, cancellationToken);
    
    public async Task GuardarAsync(CancellationToken cancellationToken) =>
    await _boardlyContexto.SaveChangesAsync(cancellationToken);
}