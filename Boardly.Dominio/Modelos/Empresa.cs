using Boardly.Dominio.Enum;

namespace Boardly.Dominio.Modelos;

public class Empresa
{
    public Guid EmpresaId { get; set; }
    public Guid EmpleadoId { get; set; }
    public Guid? CeoId { get; set; }
    public string Nombre { get; set; } 
    public string? Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public EstadoEmpresa Estado { get; set; } 

    public Empleado Empleado { get; set; }
    public Ceo Ceo { get; set; }
    public ICollection<Proyecto> Proyectos { get; set; }
}