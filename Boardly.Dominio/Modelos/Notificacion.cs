namespace Boardly.Dominio.Modelos;

public sealed class Notificacion
{
    public Guid NotificacionId { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid? TareaId { get; set; }
    public string Mensaje { get; set; }
    public bool Leida { get; set; }
    public DateTime FechaCreado { get; set; } = DateTime.UtcNow;

    public Usuario Usuario { get; set; }
    public Tarea? Tarea { get; set; }
}