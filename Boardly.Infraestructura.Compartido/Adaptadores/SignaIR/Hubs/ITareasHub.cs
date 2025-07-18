using Boardly.Aplicacion.DTOs.Tarea;

namespace Boardly.Infraestructura.Compartido.Adaptadores.SignaIR.Hubs;

public interface ITareasHub
{
    Task RecibirNuevaTarea(TareaDto tarea);
    Task ActualizarTarea(TareaDto tarea);
}