namespace Boardly.Dominio.Puertos.CasosDeUso.SignaIR;

public interface INotificadorTareas<TSolicitud>
{
    Task NotificarNuevaTarea(Guid usuarioId, TSolicitud tarea);
    Task NotificarTareaActualizada(Guid usuarioId, TSolicitud tarea);
}