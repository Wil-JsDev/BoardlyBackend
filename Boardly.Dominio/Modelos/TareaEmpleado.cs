namespace Boardly.Dominio.Modelos;

public class TareaEmpleado
{
    public Guid TareaId { get; set; }
 
    public Guid EmpleadoId { get; set; }

    public Empleado? Empleado { get; set; }
    
    public Tarea? Tarea { get; set; }
}