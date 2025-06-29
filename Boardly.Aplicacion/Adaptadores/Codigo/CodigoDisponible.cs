using Boardly.Dominio.Puertos.CasosDeUso.Codigo;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;

namespace Boardly.Aplicacion.Adaptadores.Codigo;

public class CodigoDisponible(ICodigoRepositorio codigoRepositorio) : ICodigoDisponible<Resultado>
{
    public async Task<Resultado> CodigoDisponibleAsync(string codigo, CancellationToken cancellationToken)
    {
        var codigoUsado = await codigoRepositorio.CodigoNoUsadoAsync(codigo, cancellationToken);
    
        return codigoUsado ? Resultado.Fallo(Error.Conflicto("409", "El código ya ha sido usado")) : Resultado.Exito();
    }
}