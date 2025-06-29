using Boardly.Aplicacion.DTOs.Codigo;
using Boardly.Dominio.Puertos.CasosDeUso;
using Boardly.Dominio.Puertos.CasosDeUso.Codigo;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;

namespace Boardly.Aplicacion.Adaptadores.Codigo;

public class EliminarCodigo(ICodigoRepositorio codigoRepositorio) : IEliminarCodigo<Resultado>
{
    public async Task<Resultado> EliminarCodigoAsync(Guid codigoId, CancellationToken cancellationToken)
    {
        var codigo = await codigoRepositorio.ObtenerCodigoPorIdAsync(codigoId, cancellationToken);
        if (codigo == null)
        {
    
            return ResultadoT<CodigoDto>.Fallo(Error.NoEncontrado("404", "Código no encontrado"));
        }

        await codigoRepositorio.EliminarCodigoAsync(codigo, cancellationToken);


        return Resultado.Exito();

    }
}