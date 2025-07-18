using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Boardly.Infraestructura.Compartido.Adaptadores.SignaIR;

public class CustomUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {

        var sub = connection.User?.FindFirst("sub")?.Value;
        if (string.IsNullOrWhiteSpace(sub))
            sub = connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        Console.WriteLine($"🧠 UserId (desde token): {sub}");
        return sub;
    }
}