namespace Boardly.Dominio.Modelos;

public sealed class Empleado
{
    public Guid EmpleadoId { get; set; } 
    public Guid UsuarioId { get; set; }
    public Guid EmpresaId { get; set; }
    
    public Empresa Empresa { get; set; }
    public Usuario Usuario { get; set; }
    public ICollection<EmpleadoProyectoRol> EmpleadosProyectoRol { get; set; }

}