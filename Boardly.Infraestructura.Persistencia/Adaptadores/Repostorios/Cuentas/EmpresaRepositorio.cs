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

    public async Task<IEnumerable<Empresa>> ObtenerEmpresaDetallesPorEmpleadoIdAsync(
        Guid empleadoId,
        CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Empresa>()
            .AsNoTracking()
            .Where(x => x.Empleados.Any(e => e.EmpleadoId == empleadoId))
            .Select(empresa => new Empresa
            {
                EmpresaId = empresa.EmpresaId,
                Nombre = empresa.Nombre,
                Descripcion = empresa.Descripcion,
                Estado = empresa.Estado,
                FechaCreacion = empresa.FechaCreacion,
                CeoId = empresa.CeoId,
                Ceo = empresa.Ceo,
                Empleados = empresa.Empleados
                    .Where(e => e.EmpleadoId == empleadoId)
                    .Select(e => new Empleado
                    {
                        EmpleadoId = e.EmpleadoId,
                        EmpleadosProyectoRol = e.EmpleadosProyectoRol
                            .Where(epr => epr.EmpleadoId == empleadoId)
                            .ToList()
                    })
                    .ToList(),
                Proyectos = empresa.Proyectos
                    .Where(p => p.EmpleadosProyectoRol
                        .Any(epr => epr.EmpleadoId == empleadoId))
                    .ToList()
            })
            .ToListAsync(cancellationToken);
    }


    
    public async Task<int> ObtenerConteoDeEmpleadosPorEmpresaIdAsync(Guid empresaId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Empleado>()
            .AsNoTracking()
            .Where(x => x.EmpresaId == empresaId)
            .CountAsync(cancellationToken);
    }

    public async Task<int> ObtenerConteoDeProyectosPorEmpresaAsync(Guid empresaId, CancellationToken cancellationToken)
    {
        return await _boardlyContexto.Set<Proyecto>()
            .AsNoTracking()
            .Where(x => x.EmpresaId == empresaId)
            .CountAsync(cancellationToken);
    }
}