using Boardly.Aplicacion.DTOs.Empresa;
using Boardly.Aplicacion.DTOs.Proyecto;
using Boardly.Dominio.Modelos;

namespace Boardly.Aplicacion.Mapper;

public static class EmpresaMapper
{
    public static List<EmpresaDetallesProyectosDto> MapearDetallesProyectos(
        IEnumerable<Empresa> detallesProyectos,
        Guid empleadoId,
        int countActividad,
        int countTareas,
        int countTareasEstado)
    {
        return detallesProyectos.Select(x =>
        {
            var empleado = x.Empleados.FirstOrDefault(e => e.EmpleadoId == empleadoId);
            var rol = empleado?.EmpleadosProyectoRol?.FirstOrDefault()?.RolProyecto?.Nombre ?? string.Empty;

            var proyecto = x.Proyectos.FirstOrDefault();

            return new EmpresaDetallesProyectosDto
            (
                Nombre: x.Nombre,
                Descripcion: x.Descripcion,
                Rol: rol,
                ProyectoEmpresa: new ProyectoEmpresaDto
                (
                    ProyectoId: proyecto?.ProyectoId ?? Guid.Empty,
                    Nombre: proyecto?.Nombre ?? string.Empty
                ),
                ActividadesCount: countActividad,
                TareasCount: countTareas,
                EstadoTareaCount: countTareasEstado,
                FechaFinalizacion: proyecto?.FechaFin ?? DateTime.MinValue,
                Estado: proyecto?.Estado ?? string.Empty
            );
        }).ToList();
    }
}