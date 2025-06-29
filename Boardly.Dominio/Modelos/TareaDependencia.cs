namespace Boardly.Dominio.Modelos;

public sealed class TareaDependencia
{
    public Guid TareaId { get; set; }
    public Guid DependeDeId { get; set; }

    public Tarea Tarea { get; set; }
    public Tarea DependeDe { get; set; }
}