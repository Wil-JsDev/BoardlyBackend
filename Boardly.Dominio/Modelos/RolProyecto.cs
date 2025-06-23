namespace Boardly.Dominio.Modelos;

public class RolProyecto
{
    
    public Guid RolProyectoId { get; set; }
    public string Nombre { get; set; } 
    public string? Descripcion { get; set; }
    
    public ICollection<EmpleadoProyectoRol> EmpleadosProyectoRol { get; set; }
}