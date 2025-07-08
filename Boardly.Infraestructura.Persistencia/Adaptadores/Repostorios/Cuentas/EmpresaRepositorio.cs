using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios.Cuentas;

public class EmpresaRepositorio(BoardlyContexto contexto): GenericoRepositorio<Empresa>(contexto), IEmpresaRepositorio
{
    public async Task<bool> ExisteNombreEmpresaAsync(string nombreEmpresa, CancellationToken cancellationToken) => 
        await ValidarAsync(us => us.Nombre == nombreEmpresa, cancellationToken);
    
    public async Task<bool> NombreEmpresaEnUso(string nombreEmpresa, Guid empresaId, CancellationToken cancellationToken) => 
        await ValidarAsync(empresa => empresa.Nombre == nombreEmpresa && empresa.EmpresaId != empresaId, cancellationToken);

    public async Task<ResultadoPaginado<Empresa>> ObtenerPaginasEmpresaAsync(Guid ceoId,int numeroPagina, int tamanoPagina,
        CancellationToken cancellationToken)
    {
        var consulta = _boardlyContexto.Set<Empresa>()
            .AsNoTracking()
            .Where(x => x.CeoId == ceoId)
            .OrderByDescending(x => x.FechaCreacion);
        
        var total = await consulta.CountAsync(cancellationToken);
        
        var empresa = await consulta
            .Skip((numeroPagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync(cancellationToken);
        
        return new ResultadoPaginado<Empresa>(empresa, total, numeroPagina, tamanoPagina);   
    }
    
}