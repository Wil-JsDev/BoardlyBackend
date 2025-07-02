using System.Text.Json.Serialization;

namespace Boardly.Aplicacion.DTOs.Autenticacion;

public class AutenticacionRespuesta
{
    public Guid? Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Correo { get; set; }

    public string? NombreUsuario { get; set; }

    public List<string>? Roles { get; set; }

    public bool? EsVerificado { get; set; }
    
    public string? JWTToken { get; set; }
    
    [JsonIgnore] 
    public string? RefreshToken { get; set; }
}