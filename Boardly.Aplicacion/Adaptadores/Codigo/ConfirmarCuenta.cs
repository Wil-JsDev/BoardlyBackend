using Boardly.Dominio.Puertos.CasosDeUso;
using Boardly.Dominio.Puertos.CasosDeUso.Codigo;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;

namespace Boardly.Aplicacion.Adaptadores.Codigo;

public class ConfirmarCuenta(ICodigoRepositorio codigoRepositorio, IUsuarioRepositorio usuarioRepositorio)
    : IConfirmarCuenta<Resultado>
{
    public async Task<Resultado> ConfirmarCuentaAsync(Guid usuarioId, string codigo, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepositorio.ObtenerByIdAsync(usuarioId, cancellationToken);
        if (usuario == null)
        {
            
            return Resultado.Fallo(Error.NoEncontrado("404", "Usuario no encontrado"));
        }

        var codigoEntidad = await codigoRepositorio.BuscarCodigoAsync(codigo, cancellationToken);
        if (codigoEntidad == null)
        {
            
            return Resultado.Fallo(Error.NoEncontrado("404", "Código no encontrado"));
        }

        if (codigoEntidad.UsuarioId != usuario.UsuarioId)
        {
            
            return Resultado.Fallo(Error.NoEncontrado("403", "El código no corresponde a este usuario"));
        }

        if (codigoEntidad.Usado is true)
        {
            
            return Resultado.Fallo(Error.NoEncontrado("400", "Este código ya ha sido usado"));
        }

        var esValido = await codigoRepositorio.ElCodigoEsValidoAsync(codigo, cancellationToken);
        if (!esValido)
        {
            return Resultado.Fallo(Error.Fallo("400", "El código ha expirado o no es válido"));
        }

        await codigoRepositorio.MarcarCodigoComoUsado(codigoEntidad.Valor!, cancellationToken);

        usuario.CuentaConfirmada = true;
        await usuarioRepositorio.ActualizarAsync(usuario, cancellationToken);

        
        return Resultado.Exito();
    }
}