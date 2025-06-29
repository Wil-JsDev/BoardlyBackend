using Boardly.Dominio.Enum;

namespace Boardly.Dominio.Modelos;

public sealed class Tarea
{
    public Guid TareaId { get; set; }
    public Guid ProyectoId { get; set; }
    public string Titulo { get; set; } = null!;
    public string? Descripcion { get; set; }
    public string Estado { get; set; } 
    public DateTime FechaCreado { get; set; } = DateTime.UtcNow;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public DateTime? FechaCompletada { get; set; }
    public Guid ActividadId { get; set; }

    public Proyecto Proyecto { get; set; }
    public Actividad Actividad { get; set; }
    public ICollection<TareaUsuario> TareasUsuario { get; set; }
    public ICollection<Comentario> Comentarios { get; set; }
    public ICollection<TareaDependencia> Dependencias { get; set; }
    public ICollection<TareaDependencia> Dependientes { get; set; }
    public ICollection<Notificacion> Notificaciones { get; set; }
    
}