using Boardly.Aplicacion.DTOs.Proyecto;
using Boardly.Dominio.Puertos.CasosDeUso.Proyecto;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Proyecto;

public class CrearProyecto(
    ILogger<CrearProyecto> logger,
    IProyectoRepositorio repositorioProyecto,
    IEmpresaRepositorio empresaRepositorio
    ) : ICrearProyecto<CrearProyectoDto, ProyectoDto>
{
    public async Task<ResultadoT<ProyectoDto>> CrearProyectoAsync(CrearProyectoDto solicitud, CancellationToken cancellationToken)
    {
        if (solicitud is null)
        {
            logger.LogWarning("Solicitud nula recibida al intentar crear un proyecto.");
            
            return ResultadoT<ProyectoDto>.Fallo(Error.Fallo("400", "La solicitud no puede ser nula."));
        }

        var empresa = await empresaRepositorio.ObtenerByIdAsync(solicitud.EmpresaId, cancellationToken);
        if (empresa is null)
        {
            logger.LogWarning("No se encontró la empresa con ID: {EmpresaId}", solicitud.EmpresaId);
            
            return ResultadoT<ProyectoDto>.Fallo(Error.NoEncontrado("404", "La empresa asociada no fue encontrada."));
        }

        if (await repositorioProyecto.ExisteProyectoAsync(solicitud.Nombre, cancellationToken))
        {
            logger.LogWarning("Ya existe un proyecto con el nombre: {Nombre}", solicitud.Nombre);
            
            return ResultadoT<ProyectoDto>.Fallo(Error.Conflicto("409", "Ya existe un proyecto con ese nombre."));
        }

        Dominio.Modelos.Proyecto proyectoEntidad = new()
        {
            ProyectoId = Guid.NewGuid(),
            Nombre = solicitud.Nombre,
            Descripcion = solicitud.Descripcion,
            FechaInicio = solicitud.FechaInicio,
            FechaFin = solicitud.FechaFin,
            Estado = solicitud.Estado,
            EmpresaId = solicitud.EmpresaId
        };

        await repositorioProyecto.CrearAsync(proyectoEntidad, cancellationToken);

        logger.LogInformation("Proyecto creado exitosamente en la base de datos. ProyectoId: {ProyectoId}", proyectoEntidad.ProyectoId);

        ProyectoDto proyectoDto = new(
            ProyectoId: proyectoEntidad.ProyectoId,
            EmpresaId: proyectoEntidad.EmpresaId,
            Nombre: proyectoEntidad.Nombre,
            Descripcion: proyectoEntidad.Descripcion,
            FechaInicio: proyectoEntidad.FechaInicio,
            FechaFin: proyectoEntidad.FechaFin,
            Estado: proyectoEntidad.Estado,
            FechaCreado: proyectoEntidad.FechaCreado
        );

        return ResultadoT<ProyectoDto>.Exito(proyectoDto);
    }

}