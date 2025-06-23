using Boardly.Dominio.Enum;

namespace Boardly.Dominio.Modelos;

public class Proyecto
{
    public Guid ProyectoId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public EstadoProyecto Estado { get; set; } 
    public Guid EmpresaId { get; set; }
    public DateTime FechaCreado { get; set; }
    public DateTime FechaActualizacion { get; set; }

    public Empresa Empresa { get; set; }
    public ICollection<Tarea> Tareas { get; set; }
    public ICollection<EmpleadoProyectoRol> EmpleadosProyectoRol { get; set; }


}