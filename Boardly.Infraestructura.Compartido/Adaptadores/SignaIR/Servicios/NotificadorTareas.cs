using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Dominio.Enum;
using Boardly.Dominio.Puertos.CasosDeUso.SignaIR;
using Boardly.Infraestructura.Compartido.Adaptadores.SignaIR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Boardly.Infraestructura.Compartido.Adaptadores.SignaIR.Servicios;

public class NotificadorTareas(IHubContext<TareasHub, ITareasHub> hubContext) : INotificadorTareas<TareaDto>
{
    public async Task NotificarNuevaTarea(Guid usuarioId, TareaDto tarea)
    {
        await hubContext.Clients.User(usuarioId.ToString())
            .RecibirNuevaTarea(tarea);
    }
    public async Task NotificarTareaActualizada(Guid usuarioId, TareaDto tareaActualizada)
    {
        await hubContext.Clients.User(usuarioId.ToString())
            .ActualizarTarea(tareaActualizada);
    }
}