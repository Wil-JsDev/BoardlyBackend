using Boardly.Aplicacion.DTOs.Email;
using Boardly.Aplicacion.DTOs.Usuario;
using Boardly.Dominio.Enum;
using Boardly.Dominio.Puertos.CasosDeUso.Codigo;
using Boardly.Dominio.Puertos.CasosDeUso.Usuario;
using Boardly.Dominio.Puertos.Cloudinary;
using Boardly.Dominio.Puertos.Email;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Usuario;

public class CrearUsuario(
    ILogger<CrearUsuario> logger,
    IUsuarioRepositorio repositorioUsuario,
    ICorreoServicio<SolicitudCorreo> emailServicio,
    ICrearCodigo codigo,
    ICloudinaryServicio cloudinaryServicio
    ) : ICrearUsuario<CrearUsuarioDto, UsuarioDto>
{
    public async Task<ResultadoT<UsuarioDto>> CrearUsuarioAsync(CrearUsuarioDto solicitud, CancellationToken cancellationToken)
    {
        if (solicitud is null)
        {
            logger.LogWarning("Solicitud de creación de usuario es nula.");
            return ResultadoT<UsuarioDto>.Fallo(Error.Fallo("400", "La solicitud de creación de usuario no puede ser nula."));
        }


        if (await repositorioUsuario.ExisteEmailAsync(solicitud.Correo!, cancellationToken))
        {
            logger.LogWarning("El email esta registrado: {Email}", solicitud.Correo);
            
            return ResultadoT<UsuarioDto>.Fallo(Error.Conflicto("409", "El email ya esta registrado"));
        }
        
        if (await repositorioUsuario.ExisteNombreUsuarioAsync(solicitud.NombreUsuario!, cancellationToken))
        {
            logger.LogWarning("El nombre de usuario esta registrado. Usuario: {NombreUsuario}",
                solicitud.NombreUsuario);

            return ResultadoT<UsuarioDto>.Fallo(Error.Conflicto("409", "El nombre de usuario ya esta registrado"));
        }

        string imageUrl = "";
        if (solicitud.FotoPerfil != null)
        {
            using var stream = solicitud.FotoPerfil.OpenReadStream();
            imageUrl = await cloudinaryServicio.SubirImagenAsync(
                stream,
                solicitud.FotoPerfil.FileName,
                cancellationToken);
            
            logger.LogInformation("Imagen de perfil subida correctamente para el usuario: {NombreUsuario}", solicitud.NombreUsuario);
        }

        Dominio.Modelos.Usuario usuarioEntidad = new()
        {
            UsuarioId = Guid.NewGuid(),
            Nombre = solicitud.Nombre,
            Apellido = solicitud.Apellido,
            Correo = solicitud.Correo,
            NombreUsuario = solicitud.NombreUsuario,
            Contrasena = BCrypt.Net.BCrypt.HashPassword(solicitud.Contrasena),
            Estado = nameof(EstadoUsuario.Activo),
            FotoPerfil = imageUrl
        };

        await repositorioUsuario.CrearAsync(usuarioEntidad, cancellationToken);
        logger.LogInformation("Usuario creado exitosamente en la base de datos. UsuarioId: {UsuarioId}", usuarioEntidad.UsuarioId);

        var codigoValor = await codigo.CrearCodigoAsync(usuarioEntidad.UsuarioId, TipoCodigo.ConfirmacionCuenta, cancellationToken);
        
        logger.LogInformation("Código de confirmación generado para el usuario: {UsuarioId}", usuarioEntidad.UsuarioId);

        await emailServicio.Execute(
            new SolicitudCorreo(
                Destinatario: solicitud.Correo!,
                Cuerpo: EmailTemas.ConfirmarCuenta(codigoValor.Valor, usuarioEntidad.UsuarioId),
                Asunto: "Confirmar cuenta"
            )
        );
        logger.LogInformation("Correo de confirmación enviado al usuario: {Correo}", solicitud.Correo);

        UsuarioDto usuarioDto = new(
            UsuarioId: usuarioEntidad.UsuarioId,
            Nombre: usuarioEntidad.Nombre,
            Apellido: usuarioEntidad.Apellido,
            Correo: usuarioEntidad.Correo,
            NombreUsuario: usuarioEntidad.NombreUsuario,
            FechaCreacion: usuarioEntidad.FechaCreacion,
            Estado: usuarioEntidad.Estado,
            FotoPerfil: usuarioEntidad.FotoPerfil,
            FechaRegistro: usuarioEntidad.FechaRegistro
        );

        return ResultadoT<UsuarioDto>.Exito(usuarioDto);
    }

}
