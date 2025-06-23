using Boardly.Dominio.Enum;

namespace Boardly.Dominio.Modelos;

public class Empleado
{
    public Guid EmpleadoId { get; set; } 
    public Guid UsuarioId { get; set; }
    public string Roles { get; set; } 
    
    public Usuario Usuario { get; set; }
    public ICollection<EmpleadoProyectoRol> EmpleadosProyectoRol { get; set; }

}