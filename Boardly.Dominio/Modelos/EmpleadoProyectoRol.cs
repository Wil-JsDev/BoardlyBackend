namespace Boardly.Dominio.Modelos;

public sealed class EmpleadoProyectoRol
{
    public Guid EmpleadoId { get; set; }
    public Guid ProyectoId { get; set; }
    public Guid RolProyectoId { get; set; }

    public Empleado Empleado { get; set; }
    public Proyecto Proyecto { get; set; } 
    public RolProyecto RolProyecto { get; set; } 
}