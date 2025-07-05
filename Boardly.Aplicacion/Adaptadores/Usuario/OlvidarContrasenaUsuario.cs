using Boardly.Aplicacion.DTOs.Email;
using Boardly.Dominio.Enum;
using Boardly.Dominio.Puertos.CasosDeUso.Codigo;
using Boardly.Dominio.Puertos.CasosDeUso.Usuario;
using Boardly.Dominio.Puertos.Email;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Usuario;

public class OlvidarContrasenaUsuario(
    IUsuarioRepositorio repositorioUsuario,
    ILogger<IUsuarioRepositorio> logger,
    ICrearCodigo codigo,
    ICorreoServicio<SolicitudCorreo> emailServicio
    ) : IOlvidarContrasenaUsuario
{
    public async Task<ResultadoT<string>> OlvidarContrasena(Guid usuarioId, CancellationToken cancellationToken)
    {
        var usuario = await repositorioUsuario.ObtenerByIdAsync(usuarioId, cancellationToken);

        if (usuario == null)
        {
            logger.LogError("No se encontró el usuario con ID '{UsuarioId}'.", usuarioId);
            return ResultadoT<string>.Fallo(
                Error.NoEncontrado("404", "Usuario no encontrado.")
            );
        }

        if (usuario.CuentaConfirmada is false)
        {
            logger.LogWarning("El usuario con ID '{UsuarioId}' intentó recuperar contraseña pero su cuenta no está confirmada.", usuarioId);
            return ResultadoT<string>.Fallo(
                Error.Conflicto("409", "La cuenta del usuario aún no ha sido confirmada.")
            );
        }

        var codigoValor = await codigo.CrearCodigoAsync(usuarioId, TipoCodigo.RecuperacionContrasena,cancellationToken);
        
        if (!codigoValor.EsExitoso)
        {
            logger.LogError("Falló la generación del código para el usuario '{UsuarioId}'. Error: {Error}",
                usuarioId, codigoValor.Error);
        
            return ResultadoT<string>.Fallo(codigoValor.Error!);
        } 
        
        await emailServicio.Execute(
            new SolicitudCorreo(
                Destinatario: usuario.Correo!,
                Cuerpo: EmailTemas.OlvidarContrasena(codigoValor.Valor, usuario.UsuarioId),
                Asunto: "Recuperación de contraseña"
            )
        );

        logger.LogInformation("Se envió el código de recuperación al correo '{Email}' para el usuario '{UsuarioId}'.",
            usuario.Correo, usuario.UsuarioId);

        return ResultadoT<string>.Exito("Se envió el código de recuperación correctamente.");
    }

}