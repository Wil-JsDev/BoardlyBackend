using Boardly.Aplicacion.DTOs.Codigo;
using Boardly.Dominio.Puertos.CasosDeUso.Codigo;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Codigo;

public class BuscarCodigo(
    ICodigoRepositorio codigoRepositorio,
    ILogger<BuscarCodigo> logger
    ) : IBuscarCodigo<CodigoDto>
{
    public async Task<ResultadoT<CodigoDto>> BuscarCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        if (codigo is null)
        {
            logger.LogWarning("La solicitud de búsqueda de código es nula.");
            return ResultadoT<CodigoDto>.Fallo(
                Error.Fallo("400", "El valor del código no puede ser nulo.")
            );
        }

        var codigoValor = await codigoRepositorio.BuscarCodigoAsync(codigo, cancellationToken);
        if (codigoValor is null)
        {
            logger.LogWarning("No se encontró ningún código con el valor '{Codigo}'.", codigo);
            return ResultadoT<CodigoDto>.Fallo(
                Error.NoEncontrado("404", $"No se encontró ningún código con el valor '{codigo}'.")
            );
        }

        CodigoDto codigoDto = new(
            CodigoId: codigoValor.CodigoId,
            UsuarioId: codigoValor.UsuarioId,
            Codigo: codigoValor.Valor,
            Usado: codigoValor.Usado!.Value,
            Expiracion: codigoValor.Expiracion
        );

        logger.LogInformation("Se encontró correctamente el código con valor '{Codigo}' para el usuario '{UsuarioId}'.",
            codigo, codigoValor.UsuarioId);

        return ResultadoT<CodigoDto>.Exito(codigoDto);
    }

}