using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Boardly.Infraestructura.Compartido.Adaptadores.SignaIR.Hubs;

[Authorize]
public class TareasHub(ILogger<TareasHub> logger) : Hub<ITareasHub>
{
    private readonly ILogger<TareasHub> _logger = logger;

    public override Task OnConnectedAsync()
    {
        var userIdentifier = Context.UserIdentifier;
        var isAuth = Context.User?.Identity?.IsAuthenticated ?? false;
        var sub = Context.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        logger.LogInformation("🔌 Usuario conectado:");
        logger.LogInformation("- UserIdentifier (SignalR): {UserIdentifier}", userIdentifier);
        logger.LogInformation("- sub desde Claims: {Sub}", sub);
        logger.LogInformation("- ¿Autenticado?: {IsAuth}", isAuth);
        
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("Usuario desconectado de TareasHub: {UserId}", Context.UserIdentifier);
        
        return base.OnDisconnectedAsync(exception);
    }

}