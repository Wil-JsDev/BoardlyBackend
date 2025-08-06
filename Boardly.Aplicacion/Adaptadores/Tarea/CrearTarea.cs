using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Aplicacion.DTOs.Usuario;
using Boardly.Dominio.Enum;
using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.CasosDeUso.SignaIR;
using Boardly.Dominio.Puertos.CasosDeUso.Tarea;
using Boardly.Dominio.Puertos.Cloudinary;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Tarea;

public class CrearTarea(
    ILogger<CrearTarea> logger,
    ITareaRepositorio tareaRepositorio,
    IActividadRepositorio actividadRepositorio,
    INotificadorTareas<TareaDto> notificadorTareas,
    IUsuarioRepositorio usuarioRepositorio,
    ITareaEmpleadoRepositorio tareaEmpleadoRepositorio,
    ICloudinaryServicio cloudinaryServicio
    ) : ICrearTarea<CrearTareaDto, TareaDto>
{
    public async Task<ResultadoT<TareaDto>> CrearTareaAsync(CrearTareaDto solicitud,
        CancellationToken cancellationToken)
    {
        if (await tareaRepositorio.ExisteNombreTareaAsync(solicitud.Titulo, solicitud.ProyectoId, cancellationToken))
        {
            logger.LogWarning("Ya existe una tarea con el nombre {Titulo} en el proyecto {ProyectoId}",
                solicitud.Titulo, solicitud.ProyectoId);

            return ResultadoT<TareaDto>.Fallo(Error.Conflicto("409",
                "Ya existe una tarea con el nombre especificado."));
        }

        var usuario = await usuarioRepositorio.ObtenerByIdAsync(solicitud.UsuarioId, cancellationToken);
        if (usuario is null)
        {
            logger.LogWarning("El usuario {UsuarioId} no fue encontrado", solicitud.UsuarioId);

            return ResultadoT<TareaDto>.Fallo(Error.NoEncontrado("404", "El usuario especificado no fue encontrado."));
        }

        var actividad = await actividadRepositorio.ObtenerByIdAsync(solicitud.ActividadId, cancellationToken);
        if (actividad is null)
        {
            logger.LogWarning("La actividad {ActividadId} no fue encontrada", solicitud.ActividadId);

            return ResultadoT<TareaDto>.Fallo(Error.NoEncontrado("404",
                "La actividad especificada no fue encontrada."));
        }

        if (!await ValidacionFecha.ValidarRangoDeFechasAsync(solicitud.FechaInicio, solicitud.FechaVencimiento,
                cancellationToken))
        {
            logger.LogWarning(
                "El rango de fechas proporcionado no es válido. FechaInicio: {FechaInicio}, FechaFin: {FechaVencimiento}",
                solicitud.FechaInicio, solicitud.FechaVencimiento);

            return ResultadoT<TareaDto>.Fallo(
                Error.Fallo("400",
                    "El rango de fechas no es válido. La fecha de inicio debe ser menor que la fecha de fin y la fecha de inicio debe ser futuras.")
            );
        }

        if (solicitud.EmpleadoIds.Count == 0)
        {
            logger.LogWarning("No se proporcionaron empleados para asignar la tarea. Solicitud inválida.");

            return ResultadoT<TareaDto>.Fallo(Error.Fallo("400",
                "Debe proporcionar al menos un empleado para asignar la tarea."));
        }

        string url = "";
        if (solicitud.Archivo != null)
        {
            using var stream = solicitud.Archivo.OpenReadStream();
            url = await cloudinaryServicio.SubirArchivoAsync(
                stream,
                solicitud.Archivo.FileName,
                cancellationToken);
        }
        

        Dominio.Modelos.Tarea tareaEntidad = new()
        {
            TareaId = Guid.NewGuid(),
            ProyectoId = solicitud.ProyectoId,
            Titulo = solicitud.Titulo,
            Descripcion = solicitud.Descripcion,
            Estado = EstadoTarea.Pendiente.ToString(),
            Archivo = url,
            FechaInicio = solicitud.FechaInicio,
            FechaVencimiento = solicitud.FechaVencimiento,
            FechaActualizacion = DateTime.UtcNow,
            FechaCompletada = null,
            ActividadId = solicitud.ActividadId
        };
        
        var tareaEmpleado = solicitud.EmpleadoIds.Select(id => new TareaEmpleado
        {
            TareaId = tareaEntidad.TareaId,
            EmpleadoId = id
        }).ToList();
        
        await tareaRepositorio.CrearAsync(tareaEntidad, cancellationToken);

        await tareaEmpleadoRepositorio.CrearTareasEmpleadosAsync(tareaEmpleado, cancellationToken);

        var empleadosAsignados = await tareaEmpleadoRepositorio.ObtenerEmpleadosPorIdsAsync(solicitud.EmpleadoIds, cancellationToken);

        var listaUsuariosFotoPerfil = empleadosAsignados.Select(te => new UsuarioFotoPerfilDto(
            UsuarioId: te.Empleado!.UsuarioId,
            FotoPerfil: te.Empleado!.Usuario.FotoPerfil
        )).ToList();
        
        
        TareaDto tareaCreadaDto = new
        (
            TareaId: tareaEntidad.TareaId,
            ProyectoId: tareaEntidad.ProyectoId,
            Titulo: tareaEntidad.Titulo,
            EstadoTarea: tareaEntidad.Estado,
            Descripcion: tareaEntidad.Descripcion,
            FechaInicio: tareaEntidad.FechaInicio,
            FechaVencimiento: tareaEntidad.FechaVencimiento,
            FechaActualizacion: tareaEntidad.FechaActualizacion,
            FechaCreado: tareaEntidad.FechaCreado,
            ActividadId: solicitud.ActividadId,
            UsuarioFotoPerfil: listaUsuariosFotoPerfil,
            Archivo: tareaEntidad.Archivo,
            EnRevision: tareaEntidad.EnRevision
        );

        
        await notificadorTareas.NotificarNuevaTarea(solicitud.UsuarioId, tareaCreadaDto);
        
        return ResultadoT<TareaDto>.Exito(tareaCreadaDto);
    }
}