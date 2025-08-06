namespace Boardly.Dominio.Puertos.CasosDeUso.SignaIR;

public interface INotificadorTareas<TSolicitud>
{
    Task NotificarNuevaTarea(Guid usuarioId, TSolicitud tarea);
    Task NotificarTareaActualizada(Guid usuarioId, TSolicitud tarea);

    Task NotificarTareasPaginadas(Guid usuarioId, IEnumerable<TSolicitud> tareaDtos);
    Task NotificarTareaEnPendiente(Guid usuarioId, TSolicitud tarea);

    Task NotificarTareaEnProceso(Guid usuarioId, TSolicitud tarea);
    
    Task NotificarTareaEnRevision(Guid usuarioId, TSolicitud tarea);
    
    Task NotificarTareaFinalizada(Guid usuarioId, TSolicitud tarea);

    Task EnviarTareasInicialesAsync(Guid usuarioId, Guid actividadId, int numeroPagina, int tamanoPagina);
}
