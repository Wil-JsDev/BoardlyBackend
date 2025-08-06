using Boardly.Aplicacion.DTOs.Tarea;

namespace Boardly.Infraestructura.Compartido.Adaptadores.SignaIR.Hubs;

public interface ITareasHub
{
    Task RecibirNuevaTarea(TareaDto tarea);
    Task ActualizarTarea(TareaDto tarea);
    
    Task RecibirTareasPaginadas(IEnumerable<TareaDto> tareas);
    Task RecibirTareaEnPendiente(TareaDto tarea);
    
    Task RecibirTareaEnProceso(TareaDto tarea);

    Task RecibirTareaEnRevision(TareaDto tarea);
    
    Task RecibirTareaFinalizada(TareaDto tarea);
    
}