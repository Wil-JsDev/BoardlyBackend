namespace Boardly.Dominio.Modelos;

public sealed class TareaUsuario
{
    public Guid TareaId { get; set; }
    public Guid UsuarioId { get; set; }

    public Tarea Tarea { get; set; }
    public Usuario Usuario { get; set; }
}