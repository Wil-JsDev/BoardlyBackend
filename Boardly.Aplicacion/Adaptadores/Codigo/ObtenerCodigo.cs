using Boardly.Aplicacion.DTOs.Codigo;
using Boardly.Dominio.Puertos.CasosDeUso;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;

namespace Boardly.Aplicacion.Adaptadores.Codigo;

public class ObtenerCodigo: IObtenerCodigo<CodigoDto>
{
    
    private readonly ICodigoRepositorio _codigoRepositorio;
    

    public ObtenerCodigo(ICodigoRepositorio codigoRepositorio)
    {
        _codigoRepositorio = codigoRepositorio;
    }
    
    public async Task<ResultadoT<CodigoDto>> ObtenerCodigoAsync(Guid codigoId, CancellationToken cancellationToken)
    {
        var codigo = await _codigoRepositorio.ObtenerCodigoPorIdAsync(codigoId, cancellationToken);
        if (codigo == null)
        {
    
            return ResultadoT<CodigoDto>.Fallo(Error.NoEncontrado("404", "Código no encontrado"));
        }

        CodigoDto codigoDto = new
        (
            CodigoId: Guid.NewGuid(), 
            UsuarioId: codigo.UsuarioId,
            Codigo: codigo.Valor!,
            Usado: codigo.Usado!.Value,
            Expiracion: codigo.Expiracion
        );


        return ResultadoT<CodigoDto>.Exito(codigoDto);

    }
}