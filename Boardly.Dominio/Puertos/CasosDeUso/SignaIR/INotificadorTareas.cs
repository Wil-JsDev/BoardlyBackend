namespace Boardly.Dominio.Puertos.CasosDeUso.SignaIR;

public interface INotificadorTareas<TSolicitud>
{
    Task NotificarNuevaTarea(Guid usuarioId, TSolicitud tarea);
    
    Task NotificarTareaEliminada(Guid usuarioId, Guid tareaId);
}