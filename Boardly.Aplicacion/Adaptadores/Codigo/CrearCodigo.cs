using Boardly.Dominio.Puertos.CasosDeUso;
using Boardly.Dominio.Puertos.CasosDeUso.Codigo;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;

namespace Boardly.Aplicacion.Adaptadores.Codigo;

public class CrearCodigo(ICodigoRepositorio codigoRepositorio, IUsuarioRepositorio usuarioRepositorio)
    : ICrearCodigo
{
    public async Task<ResultadoT<string>> CrearCodigoAsync(Guid usuarioId, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepositorio.ObtenerByIdAsync(usuarioId, cancellationToken);
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

        await codigoRepositorio.CrearCodigoAsync(codigo, cancellationToken);
        
        return ResultadoT<string>.Exito(codigo.Valor);
    }
}