namespace Boardly.Dominio.Configuraciones;

public class JwtConfiguraciones
{
    public string? Clave { get; set; }
    
    public string? Emisor { get; set; }  
    
    public string? Audiencia { get; set; }   
    
    public int DuracionEnMinutos { get; set; }
}