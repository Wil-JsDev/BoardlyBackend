using Boardly.Dominio.Modelos;

namespace Boardly.Dominio.Puertos.Repositorios.Cuentas;

public interface IUsuarioRepositorio : IGenericoRepositorio<Usuario>
{
     /// <summary>
    /// Verifica si la cuenta de un usuario esta confirmada.
    /// </summary>
    /// <param name="id">Id del usuario.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    /// <returns>True si la cuenta esta confirmada, false en caso contrario.</returns>
    Task<bool> CuentaConfirmadaAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si un nombre de usuario esta en uso por otro usuario distinto al especificado.
    /// </summary>
    /// <param name="nombreUsuario">Nombre de usuario a verificar.</param>
    /// <param name="usuarioId">Id del usuario que se excluye de la verificacion.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    /// <returns>True si el nombre de usuario está en uso, false en caso contrario.</returns>
    Task<bool> NombreUsuarioEnUso(string nombreUsuario, Guid usuarioId, CancellationToken cancellationToken);

    /// <summary>
    /// Busca un usuario por su email.
    /// </summary>
    /// <param name="email">Email del usuario.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    /// <returns>Usuario encontrado o null si no existe.</returns>
    Task<Usuario> BuscarPorEmailUsuarioAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Busca un usuario por su nombre de usuario.
    /// </summary>
    /// <param name="nombreUsuario">Nombre de usuario a buscar.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    /// <returns>Usuario encontrado o null si no existe.</returns>
    Task<Usuario> BuscarPorNombreUsuarioAsync(string nombreUsuario, CancellationToken cancellationToken);
    
    /// <summary>
    /// Verifica si un email esta en uso, excluyendo un usuario especifico.
    /// </summary>
    /// <param name="email">Email a verificar.</param>
    /// <param name="excluirUsuarioId">Id del usuario a excluir.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    Task<bool> EmailEnUsoAsync(string email, Guid excluirUsuarioId, CancellationToken cancellationToken);

    /// <summary>
    /// Actualiza la contrasena de un usuario.
    /// </summary>
    /// <param name="usuario">Usuario al que se le actualiza la contrasena.</param>
    /// <param name="newHashedPassword">Nueva contrasena en hash.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    Task ActualizarContrasenaAsync(Usuario usuario, string newHashedPassword, CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si un email existe en la base de datos.
    /// </summary>
    /// <param name="email">Email a verificar.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si un nombre de usuario existe en la base de datos.
    /// </summary>
    /// <param name="nombreUsuario">Nombre de usuario a verificar.</param>
    /// <param name="cancellationToken">Token para cancelar la operacion.</param>
    Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario, CancellationToken cancellationToken);
    
}