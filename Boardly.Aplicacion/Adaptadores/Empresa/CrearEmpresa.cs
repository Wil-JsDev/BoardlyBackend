using Boardly.Aplicacion.DTOs.Empresa;
using Boardly.Dominio.Enum;
using Boardly.Dominio.Puertos.CasosDeUso.Empresa;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empresa;

public sealed class CrearEmpresa(
    ILogger<CrearEmpresa> logger,
    IEmpresaRepositorio empresaRepositorio,
    IEmpleadoRepositorio empleadoRepositorio
) : ICrearEmpresa<CrearEmpresaDto, EmpresaDto>
{
    public async Task<ResultadoT<EmpresaDto>> CrearEmpresaAsync(CrearEmpresaDto solicitud, CancellationToken cancellationToken)
    {
        if (solicitud is null)
        {
            logger.LogError("La solicitud para crear una empresa es nula.");
            
            return ResultadoT<EmpresaDto>.Fallo(Error.Fallo("400", "La solicitud no puede ser nula."));
        }

        if (await empresaRepositorio.ExisteNombreEmpresaAsync(solicitud.Nombre!, cancellationToken))
        {
            logger.LogWarning("El nombre de empresa '{Nombre}' ya está en uso.", solicitud.Nombre);
            
            return ResultadoT<EmpresaDto>.Fallo(Error.Conflicto("409", $"El nombre '{solicitud.Nombre}' ya está en uso."));
        }
        
        var empleado = await empleadoRepositorio.ObtenerByIdAsync(solicitud.EmpleadoId, cancellationToken);
        if (empleado is null)
        {
            logger.LogWarning("No se encontró el empleado con ID {EmpleadoId} al intentar asociarlo a la empresa.", solicitud.EmpleadoId);
            
            return ResultadoT<EmpresaDto>.Fallo(Error.NoEncontrado("404", $"El empleado con ID {solicitud.EmpleadoId} no existe."));
        }

        Dominio.Modelos.Empresa empresaEntidad = new()
        {
            EmpresaId = Guid.NewGuid(),
            CeoId = solicitud.CeoId,
            EmpleadoId = solicitud.EmpleadoId,
            Nombre = solicitud.Nombre,
            Descripcion = solicitud.Descripcion,
            Estado = nameof(EstadoEmpresa.Activo)
        };

        await empresaRepositorio.CrearAsync(empresaEntidad, cancellationToken);

        logger.LogInformation("Empresa creada con ID {EmpresaId} y nombre '{Nombre}'.", empresaEntidad.EmpresaId, empresaEntidad.Nombre);

        EmpresaDto empresaDto = new(
            EmpresaId: empresaEntidad.EmpresaId,
            CeoId: empresaEntidad.CeoId,
            EmpleadoId: empresaEntidad.EmpleadoId,
            Nombre: empresaEntidad.Nombre,
            Descripcion: empresaEntidad.Descripcion,
            FechaCreacion: empresaEntidad.FechaCreacion,
            Estado: empresaEntidad.Estado
        );

        return ResultadoT<EmpresaDto>.Exito(empresaDto);
    }
}
