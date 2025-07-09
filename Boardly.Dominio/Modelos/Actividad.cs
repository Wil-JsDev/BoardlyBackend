using Boardly.Dominio.Enum;

namespace Boardly.Dominio.Modelos;

public sealed class Actividad
{
    public Guid ActividadId { get; set; }
    
    public Guid ProyectoId { get; set; }
    public string? Nombre { get; set; }
    public string? Prioridad { get; set; } 
    public string? Descripcion { get; set; }
    public string? Estado { get; set; } 
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFinalizacion { get; set; }
    public int Orden { get; set; }

    public Proyecto? Proyecto { get; set; }
    public ICollection<ActividadDependencia> Dependencias { get; set; }
    public ICollection<ActividadDependencia> Dependientes { get; set; }
    public ICollection<Tarea> Tareas { get; set; } 
}