using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Infraestructura.Persistencia.Contexto;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios;

public class ActividadRepositorio(BoardlyContexto boardlyContexto) : GenericoRepositorio<Actividad>(boardlyContexto), IActividadRepositorio
{
    public async Task<bool> ExisteNombreActividadAsync(string nombreEmpresa, CancellationToken cancellationToken)
    {
        return await ValidarAsync(us => us.Nombre == nombreEmpresa, cancellationToken);
    }

    public async Task<bool> NombreActividadEnUso(string nombreUsuario, Guid actividadId, CancellationToken cancellationToken)
    {
        return await ValidarAsync(actividad => actividad.Nombre == nombreUsuario && actividad.ActividadId != actividadId, cancellationToken);
    }
}