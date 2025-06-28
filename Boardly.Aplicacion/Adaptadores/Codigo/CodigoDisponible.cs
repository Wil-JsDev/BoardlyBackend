using Boardly.Dominio.Puertos.CasosDeUso;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;

namespace Boardly.Aplicacion.Adaptadores.Codigo;

public class CodigoDisponible: ICodigoDisponible<Resultado>
{
    
    private readonly ICodigoRepositorio _codigoRepositorio;
    

    public CodigoDisponible(ICodigoRepositorio codigoRepositorio)
    {
        _codigoRepositorio = codigoRepositorio;
    }
    
    public async Task<Resultado> CodigoDisponibleAsync(string codigo, CancellationToken cancellationToken)
    {
        var codigoUsado = await _codigoRepositorio.CodigoNoUsadoAsync(codigo, cancellationToken);
    
        if (codigoUsado)
        {
            return Resultado.Fallo(Error.Conflicto("409", "El código ya ha sido usado"));
        }
        
        return Resultado.Exito();
    }
}