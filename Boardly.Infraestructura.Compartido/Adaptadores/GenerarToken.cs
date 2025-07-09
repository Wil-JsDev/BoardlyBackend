using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Boardly.Dominio.Configuraciones;
using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.CasosDeUso.Autenticacion;
using Boardly.Dominio.Puertos.Servicios;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Boardly.Infraestructura.Compartido.Adaptadores;

public class GenerarToken(
    IOptions<JwtConfiguraciones> jwtConfiguraciones, 
    IObtenerRoles obtenerRoles,
    IObtenerEmpleadoId obtenerEmpleadoId,
    IObtenerCeoId obtenerCeoId
    ) : IGenerarToken<Dominio.Modelos.Usuario>
{
    
    private readonly JwtConfiguraciones _jwtConfiguraciones = jwtConfiguraciones.Value;

    public async Task<string> GenerarTokenJwt(Dominio.Modelos.Usuario usuario, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.UsuarioId.ToString()!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("nombreUsuario", usuario.NombreUsuario!)
        };
        
        //Agregar roles
        var roles = await obtenerRoles.ObtenerRolesAsync(usuario.UsuarioId, cancellationToken);
        claims.AddRange(roles.Select(rol => new Claim("roles", rol)));
        
        //Obtener ceo id si aplica
        var ceoId = await obtenerCeoId.ObtenerCeoIdAsync(usuario.UsuarioId, cancellationToken);
        if (ceoId.HasValue)
        {
            claims.Add(new Claim("ceoId", ceoId.Value.ToString()));
        }

        // Obtener empleado id si aplica
        var empleadoId = await obtenerEmpleadoId.ObtenerEmpleadoIdAsync(usuario.UsuarioId, cancellationToken);
        if (empleadoId.HasValue)
        {
            claims.Add(new Claim("empleadoId", empleadoId.Value.ToString()));
        }
        
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