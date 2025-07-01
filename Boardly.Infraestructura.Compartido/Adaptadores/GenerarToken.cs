using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Boardly.Dominio.Configuraciones;
using Boardly.Dominio.Puertos.CasosDeUso.Autenticacion;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Boardly.Infraestructura.Compartido.Adaptadores;

public class GenerarToken(IOptions<JwtConfiguraciones> jwtConfiguraciones) : IGenerarToken<Dominio.Modelos.Usuario>
{
    
    private readonly JwtConfiguraciones _jwtConfiguraciones = jwtConfiguraciones.Value;

    public string GenerarTokenJwt(Dominio.Modelos.Usuario usuario)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.UsuarioId.ToString()!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Correo!),
            new Claim("nombreUsuario", usuario.NombreUsuario!)
        };
        
        
        
        var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguraciones.Clave!));
        var credenciales = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: _jwtConfiguraciones.Emisor,
            audience: _jwtConfiguraciones.Audiencia,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtConfiguraciones.DuracionEnMinutos),
            signingCredentials: credenciales
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
        
    }
}