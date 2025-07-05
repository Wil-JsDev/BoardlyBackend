using System.Security.Cryptography;
using Boardly.Aplicacion.DTOs.JWT;
using Boardly.Dominio.Puertos.CasosDeUso.Autenticacion;

namespace Boardly.Infraestructura.Compartido.Adaptadores;

public class TokenRefrescado : ITokenRefrescado<TokenRefrescadoDto>
{
    public TokenRefrescadoDto GenerarTokenRefrescado()
    {
        return new ()
        {
            Token = RandomTokenString(),
            Expira = DateTime.UtcNow.AddDays(7),
            Creado = DateTime.UtcNow
        };
    }
    private string RandomTokenString()
    {
        using var rng = RandomNumberGenerator.Create();
        var randomBytes = new Byte[40];
        rng.GetBytes(randomBytes);
        return BitConverter.ToString(randomBytes);
    }
}