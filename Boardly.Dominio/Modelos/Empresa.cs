using Boardly.Dominio.Enum;

namespace Boardly.Dominio.Modelos;

public sealed class Empresa
{
    public Guid EmpresaId { get; set; }
    public Guid? CeoId { get; set; }
    public string Nombre { get; set; } 
    public string? Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public string Estado { get; set; } 

    public Ceo Ceo { get; set; }
    public ICollection<Proyecto> Proyectos { get; set; }
    public ICollection<Empleado> Empleados { get; set; } 
}