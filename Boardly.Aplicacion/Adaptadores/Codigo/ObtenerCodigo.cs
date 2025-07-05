using Boardly.Aplicacion.DTOs.Codigo;
using Boardly.Dominio.Puertos.CasosDeUso.Codigo;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;

namespace Boardly.Aplicacion.Adaptadores.Codigo;

public class ObtenerCodigo(ICodigoRepositorio codigoRepositorio) : IObtenerCodigo<CodigoDto>
{
    public async Task<ResultadoT<CodigoDto>> ObtenerCodigoAsync(Guid codigoId, CancellationToken cancellationToken)
    {
        var codigo = await codigoRepositorio.ObtenerCodigoPorIdAsync(codigoId, cancellationToken);
        if (codigo == null)
        {
    
            return ResultadoT<CodigoDto>.Fallo(Error.NoEncontrado("404", "Código no encontrado"));
        }

        CodigoDto codigoDto = new
        (
            CodigoId: Guid.NewGuid(), 
            UsuarioId: codigo.UsuarioId,
            Codigo: codigo.Valor!,
            Usado: codigo.Usado!,
            Expiracion: codigo.Expiracion
        );


        return ResultadoT<CodigoDto>.Exito(codigoDto);

    }
}