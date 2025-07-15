using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios;

public class ActividadRepositorio(BoardlyContexto boardlyContexto) : GenericoRepositorio<Actividad>(boardlyContexto), IActividadRepositorio
{
    public async Task<ResultadoPaginado<Actividad>> ObtenerPaginasActividadByIdProyectoAsync(Guid proyectoId, int numeroPagina, int tamanoPagina,
        CancellationToken cancellationToken)
    {
        var consulta = _boardlyContexto.Set<Actividad>()
            .Where(p => p.ProyectoId == proyectoId)
            .AsNoTracking();
        
        var total = await consulta.CountAsync(cancellationToken);
        
        var actividad = await consulta
            .Skip((numeroPagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync(cancellationToken);
        
        return new ResultadoPaginado<Actividad>(actividad, total, numeroPagina, tamanoPagina);   
    }

    public async Task<bool> ExisteNombreActividadAsync(string nombreEmpresa, CancellationToken cancellationToken)
    {
        return await ValidarAsync(us => us.Nombre == nombreEmpresa, cancellationToken);
    }

    public async Task<bool> NombreActividadEnUso(string nombreUsuario, Guid actividadId, CancellationToken cancellationToken)
    {
        return await ValidarAsync(actividad => actividad.Nombre == nombreUsuario && actividad.ActividadId != actividadId, cancellationToken);
    }
}