namespace Boardly.Dominio.Modelos;

public sealed class ActividadDependencia
{
    public Guid ActividadId { get; set; }
    public Guid DependeDeActividadId { get; set; }

    public Actividad Actividad { get; set; }
    public Actividad DependeDeActividad { get; set; }
}