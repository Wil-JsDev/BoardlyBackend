using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Tarea;

public interface IBorrarTarea
{
    Task<ResultadoT<Guid>> BorrarTareaAsync(Guid id, CancellationToken cancellationToken);
}