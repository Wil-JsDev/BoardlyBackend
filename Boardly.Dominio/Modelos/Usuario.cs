using Boardly.Dominio.Enum;

namespace Boardly.Dominio.Modelos;

public sealed class Usuario
{
    public Guid UsuarioId { get; set; }
    public string Nombre { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public string? NombreUsuario { get; set; }
    public string Contrasena { get; set; } = null!;
    public DateTime FechaCreacion { get; set; }
    public bool CuentaConfirmada { get; set; } = false;
    public EstadoUsuario Estado { get; set; } 
    public string? FotoPerfil { get; set; }
    public DateTime? FechaRegistro { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public ICollection<Comentario> Comentarios { get; set; }
    public ICollection<Codigo> Codigos { get; set; }
    public ICollection<TareaUsuario> TareasUsuario { get; set; }
    public ICollection<Notificacion> Notificaciones { get; set; }
    public Empleado? Empleado { get; set; }
    public Ceo? Ceo { get; set; }
}