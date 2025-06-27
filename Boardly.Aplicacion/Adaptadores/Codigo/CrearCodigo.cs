using Boardly.Dominio.Puertos.CasosDeUso;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;

namespace Boardly.Aplicacion.Adaptadores.Codigo;

public class CrearCodigo: ICrearCodigo
{
    
    private readonly ICodigoRepositorio _codigoRepositorio;
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    

    public CrearCodigo(ICodigoRepositorio codigoRepositorio, IUsuarioRepositorio usuarioRepositorio)
    {
        _codigoRepositorio = codigoRepositorio;
        _usuarioRepositorio = usuarioRepositorio;
    }
    
    public async Task<ResultadoT<string>> CrearCodigoAsync(Guid usuarioId, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepositorio.ObtenerByIdAsync(usuarioId, cancellationToken);
        if (usuario == null)
        {
    
            return ResultadoT<string>.Fallo(Error.NoEncontrado("404", "Usuario no encontrado"));
        }

        if (usuario.CuentaConfirmada)
        {
    
            return ResultadoT<string>.Fallo(Error.Conflicto("409", "La cuenta ya ha sido confirmada previamente."));
        }

        string codigoGenerado = CodigoGenerador.GenerarCodigoNumerico();

        Dominio.Modelos.Codigo codigo = new()
        {
            UsuarioId = usuario.UsuarioId,
            Valor = codigoGenerado,
            Expiracion = DateTime.UtcNow.AddMinutes(10)
        };

        await _codigoRepositorio.CrearCodigoAsync(codigo, cancellationToken);
        
        return ResultadoT<string>.Exito(codigo.Valor);
    }
}