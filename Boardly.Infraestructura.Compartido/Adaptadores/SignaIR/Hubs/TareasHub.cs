using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Dominio.Enum;
using Boardly.Dominio.Puertos.CasosDeUso.SignaIR;
using Boardly.Dominio.Puertos.CasosDeUso.Tarea;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Boardly.Infraestructura.Compartido.Adaptadores.SignaIR.Hubs;

[Authorize]
public class TareasHub(
    ILogger<TareasHub> logger,
    IActualizarEstadoTarea actualizarEstadoTarea,
    INotificadorTareas<TareaDto> notificador
        ) : Hub<ITareasHub>
{
    private readonly ILogger<TareasHub> _logger = logger;

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;

        if (!Guid.TryParse(userId, out var usuarioId))
        {
            logger.LogError(" El UserIdentifier no es un GUID válido.");
            return;
        }

        var (actividadId, numeroPagina, tamanoPagina) = ObtenerParametrosDesdeQuery();

        if (actividadId is null)
        {
            logger.LogWarning(" No se pudo obtener un 'actividadId' válido para el usuario {UsuarioId}", usuarioId);
            return;
        }

        await notificador.EnviarTareasInicialesAsync(usuarioId, actividadId.Value, numeroPagina, tamanoPagina);

        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("Usuario desconectado de TareasHub: {UserId}", Context.UserIdentifier);
        
        return base.OnDisconnectedAsync(exception);
    }

    public async Task MarcarEnPendiente(Guid tareaId)
        => await actualizarEstadoTarea.CambiarEstadoAsync(tareaId, EstadoTarea.Pendiente, CancellationToken.None);
    public async Task MarcarEnProceso(Guid tareaId)
        => await actualizarEstadoTarea.CambiarEstadoAsync(tareaId, EstadoTarea.EnProceso, CancellationToken.None);

    public async Task MarcarEnRevision(Guid tareaId)
        => await actualizarEstadoTarea.CambiarEstadoAsync(tareaId, EstadoTarea.EnRevision,
            CancellationToken.None);

    public async Task MarcarFinalizada(Guid tareaId)
        => await actualizarEstadoTarea.CambiarEstadoAsync(tareaId, EstadoTarea.Finalizada, CancellationToken.None);


    #region Metodos Privados

        private (Guid? ActividadId, int NumeroPagina, int TamanoPagina) ObtenerParametrosDesdeQuery()
        {
            var httpContext = Context.GetHttpContext();
            var query = httpContext?.Request.Query;

            var actividadIdString = query?["actividadId"];
            var numeroPaginaString = query?["numeroPagina"];
            var tamanoPaginaString = query?["tamanoPagina"];

            /* Conexion desde el client
             const connection = new signalR.HubConnectionBuilder()
               .withUrl(`/tareasHub?actividadId=${actividadId}&numeroPagina=${numeroPagina}&tamanoPagina=${tamanoPagina}`)
               .build();
             */
            
            if (!Guid.TryParse(actividadIdString, out var actividadId))
            {
                logger.LogWarning("⚠️ No se proporcionó un 'actividadId' válido.");
                return (null, 1, 5);
            }

            var numeroPagina = int.TryParse(numeroPaginaString, out var np) ? np : 1;
            var tamanoPagina = int.TryParse(tamanoPaginaString, out var tp) ? tp : 5;

            return (actividadId, numeroPagina, tamanoPagina);
        }

    #endregion
    
}