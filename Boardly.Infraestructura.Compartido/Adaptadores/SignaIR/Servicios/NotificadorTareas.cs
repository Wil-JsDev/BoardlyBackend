using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Dominio.Puertos.CasosDeUso.SignaIR;
using Boardly.Dominio.Puertos.CasosDeUso.Tarea;
using Boardly.Infraestructura.Compartido.Adaptadores.SignaIR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Boardly.Infraestructura.Compartido.Adaptadores.SignaIR.Servicios;

public class NotificadorTareas(
    IHubContext<TareasHub, ITareasHub> hubContext,
    ILogger<NotificadorTareas> logger,
    IResultadoPaginadoTarea<PaginacionParametro, TareaDto> tareaPaginado) : INotificadorTareas<TareaDto>
{
    public async Task NotificarNuevaTarea(Guid usuarioId, TareaDto tarea)
        => await hubContext.Clients.User(usuarioId.ToString()).RecibirNuevaTarea(tarea);

    public async Task NotificarTareaActualizada(Guid usuarioId, TareaDto tareaActualizada)
        => await hubContext.Clients.User(usuarioId.ToString()).ActualizarTarea(tareaActualizada);

    public async Task NotificarTareasPaginadas(Guid usuarioId, IEnumerable<TareaDto> tareas)
        => await hubContext.Clients.User(usuarioId.ToString()).RecibirTareasPaginadas(tareas);

    public async Task NotificarTareaEnPendiente(Guid usuarioId, TareaDto tarea)
        => await hubContext.Clients.User(usuarioId.ToString()).RecibirTareaEnPendiente(tarea);

    public async Task NotificarTareaEnProceso(Guid usuarioId, TareaDto tarea)
        => await hubContext.Clients.User(usuarioId.ToString()).RecibirTareaEnProceso(tarea);

    public async Task NotificarTareaEnRevision(Guid usuarioId, TareaDto tarea)
        => await hubContext.Clients.User(usuarioId.ToString()).RecibirTareaEnRevision(tarea);

    public async Task NotificarTareaFinalizada(Guid usuarioId, TareaDto tarea)
        => await hubContext.Clients.User(usuarioId.ToString()).RecibirTareaFinalizada(tarea);
    
    public async Task EnviarTareasInicialesAsync(Guid usuarioId, Guid actividadId, int numeroPagina, int tamanoPagina)
    {

        PaginacionParametro paginacion = new PaginacionParametro(numeroPagina, tamanoPagina);
        var resultado = await tareaPaginado.ObtenerPaginacionTareaAsync(
            actividadId,
            paginacion,
            CancellationToken.None);

        if (resultado is { EsExitoso: true, Valor: not null })
        {
            await hubContext.Clients.User(usuarioId.ToString())
                .RecibirTareasPaginadas(resultado.Valor.Elementos!);
        }
        
        logger.LogWarning("⚠️ No se encontraron tareas para el usuario {UsuarioId} en la actividad {ActividadId}", usuarioId, actividadId);
        
    }
}