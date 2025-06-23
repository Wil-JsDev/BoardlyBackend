namespace Boardly.Dominio.Modelos;

public class Comentario
{
    public Guid ComentarioId { get; set; }
    public Guid TareaId { get; set; }
    public string Contenido { get; set; } 
    public string? Adjunto { get; set; }
    public Guid UsuarioId { get; set; }
    public DateTime FechaCreado { get; set; }
    public Tarea Tarea { get; set; }
    public Usuario Usuario { get; set; }
    

}