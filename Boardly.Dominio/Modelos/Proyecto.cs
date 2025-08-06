using Boardly.Dominio.Enum;

namespace Boardly.Dominio.Modelos;

public sealed class Proyecto
{
    public Guid ProyectoId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public string Estado { get; set; } 
    public Guid EmpresaId { get; set; }
    public DateTime FechaCreado { get; set; } = DateTime.UtcNow;
    public DateTime FechaActualizacion { get; set; }

    public Empresa Empresa { get; set; }
    public ICollection<Tarea> Tareas { get; set; }
    
    public ICollection<RolProyecto> RolesProyecto { get; set; }
    public ICollection<Actividad>? Actividades { get; set; } 
    public ICollection<EmpleadoProyectoRol> EmpleadosProyectoRol { get; set; }


}