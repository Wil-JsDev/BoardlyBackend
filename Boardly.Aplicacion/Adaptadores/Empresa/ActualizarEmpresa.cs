using Boardly.Aplicacion.DTOs.Empresa;
using Boardly.Dominio.Puertos.CasosDeUso.Empresa;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empresa;

public sealed class ActualizarEmpresa(
    ILogger<ActualizarEmpresa> logger,
    IEmpresaRepositorio empresaRepositorio
) : IActualizarEmpresa<ActualizarEmpresaDto, ActualizarEmpresaDto>
{
    public async Task<ResultadoT<ActualizarEmpresaDto>> ActualizarEmpresaAsync(Guid id, ActualizarEmpresaDto solicitud, CancellationToken cancellationToken)
    {
        if (solicitud is null)
        {
            logger.LogWarning("La solicitud para actualizar la empresa con ID {EmpresaId} es nula.", id);
            return ResultadoT<ActualizarEmpresaDto>.Fallo(
                Error.Fallo("400", "La solicitud es inválida.")
            );
        }

        var empresa = await empresaRepositorio.ObtenerByIdAsync(id, cancellationToken);
        if (empresa is null)
        {
            logger.LogWarning("No se encontró ninguna empresa con el ID {EmpresaId}.", id);
            return ResultadoT<ActualizarEmpresaDto>.Fallo(
                Error.NoEncontrado("404", $"No se encontró ninguna empresa con el ID {id}.")
            );
        }

        if (await empresaRepositorio.NombreEmpresaEnUso(solicitud.Nombre, empresa.EmpresaId, cancellationToken))
        {
            logger.LogWarning("El nombre de la empresa '{Nombre}' ya está en uso por otra entidad.", solicitud.Nombre);
            return ResultadoT<ActualizarEmpresaDto>.Fallo(
                Error.Conflicto("409", $"El nombre '{solicitud.Nombre}' ya está en uso.")
            );
        }

        logger.LogInformation("Actualizando empresa con ID {EmpresaId}.", empresa.EmpresaId);

        empresa.Nombre = solicitud.Nombre;
        empresa.Descripcion = solicitud.Descripcion;
        empresa.Estado = solicitud.Estado;

        await empresaRepositorio.ActualizarAsync(empresa, cancellationToken);

        logger.LogInformation("Empresa con ID {EmpresaId} actualizada correctamente.", empresa.EmpresaId);

        ActualizarEmpresaDto empresaDto = new
        (
            Nombre: empresa.Nombre,
            Descripcion: empresa.Descripcion,
            Estado: empresa.Estado
        );

        return ResultadoT<ActualizarEmpresaDto>.Exito(empresaDto);
    }
}
