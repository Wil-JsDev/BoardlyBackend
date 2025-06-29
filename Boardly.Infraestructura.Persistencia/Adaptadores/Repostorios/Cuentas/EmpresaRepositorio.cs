using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Infraestructura.Persistencia.Contexto;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios.Cuentas;

public class EmpresaRepositorio(BoardlyContexto contexto): GenericoRepositorio<Empresa>(contexto), IEmpresaRepositorio
{
    public async Task<bool> ExisteNombreEmpresaAsync(string nombreEmpresa, CancellationToken cancellationToken) => 
        await ValidarAsync(us => us.Nombre == nombreEmpresa, cancellationToken);
    
    public async Task<bool> NombreEmpresaEnUso(string nombreEmpresa, Guid empresaId, CancellationToken cancellationToken) => 
        await ValidarAsync(empresa => empresa.Nombre == nombreEmpresa && empresa.EmpresaId != empresaId, cancellationToken);
}