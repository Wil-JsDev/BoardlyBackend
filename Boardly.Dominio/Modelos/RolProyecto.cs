namespace Boardly.Dominio.Modelos;

public sealed class RolProyecto
{
    
    public Guid RolProyectoId { get; set; }
    public string Nombre { get; set; } 
    public string? Descripcion { get; set; }
    
    public ICollection<EmpleadoProyectoRol> EmpleadosProyectoRol { get; set; }
}