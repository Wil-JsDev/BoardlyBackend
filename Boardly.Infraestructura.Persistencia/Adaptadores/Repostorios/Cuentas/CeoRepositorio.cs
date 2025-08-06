using Boardly.Dominio.Enum;
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

    public async Task<int> ObtenerConteoDeEmpleadosDentroDeLaEmpresaAsync(Guid ceoId,
        CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Empresa>()
            .AsNoTracking()
            .Where(x => x.CeoId == ceoId )
            .SelectMany(x => x.Empleados)
            .CountAsync(cancellationToken);
    }

    public async Task<int> ObtenerConteoDeEmpleadosActivosDeLaEmpresaAsync(Guid ceoId,
        CancellationToken cancellationToken)
    {
        
        return await _boardlyContexto.Set<Empresa>()
            .AsNoTracking()
            .Where(x => x.CeoId == ceoId)
            .SelectMany(x => x.Empleados)
            .Where(x => x.Usuario.Estado == EstadoUsuario.Activo.ToString())
            .CountAsync(cancellationToken);
    }
    
    public async Task<int> ObtenerConteoDeEmpleadosInactivosDeLaEmpresaAsync(Guid ceoId,
        CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Empresa>()
            .AsNoTracking()
            .Where(x => x.CeoId == ceoId)
            .SelectMany(x => x.Empleados)
            .Where(x => x.Usuario.Estado == EstadoUsuario.Inactivo.ToString())
            .CountAsync(cancellationToken);
    }

    public async Task<int> ObtenerConteoDeEmpresasCeoAsync(Guid ceoId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Empresa>()
            .AsNoTracking()
            .Where(x => x.CeoId == ceoId)
            .CountAsync(cancellationToken);
    }

    public async Task<int> ObtenerConteoDeProyectosAsync(Guid ceoId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Proyecto>()
            .AsNoTracking()
            .Where(p => _boardlyContexto.Set<Empresa>()
                .Where(e => e.CeoId == ceoId)
                .Select(e => e.EmpresaId)
                .Contains(p.EmpresaId)
            )
            .CountAsync(cancellationToken);
    }
    
}