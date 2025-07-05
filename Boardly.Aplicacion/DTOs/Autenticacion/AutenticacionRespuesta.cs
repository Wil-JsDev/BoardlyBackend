using System.Text.Json.Serialization;

namespace Boardly.Aplicacion.DTOs.Autenticacion;

public class AutenticacionRespuesta
{
    public string? JwtToken { get; set; }
    
    [JsonIgnore] 
    public string? RefreshToken { get; set; }
}