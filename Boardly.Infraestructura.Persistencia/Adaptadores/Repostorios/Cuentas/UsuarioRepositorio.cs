using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios.Cuentas;

public class UsuarioRepositorio(BoardlyContexto boardlyContexto) :  GenericoRepositorio<Usuario>(boardlyContexto), IUsuarioRepositorio
{
    public async Task<bool> CuentaConfirmadaAsync(Guid id, CancellationToken cancellationToken)
    {
        return await ValidarAsync(us => us.UsuarioId == id  && us.CuentaConfirmada == true, cancellationToken);
    }

    public async Task<bool> NombreUsuarioEnUso(string nombreUsuario, Guid usuarioId, CancellationToken cancellationToken)
    {
        return await ValidarAsync(usuario => usuario.Nombre == nombreUsuario && usuario.UsuarioId != usuarioId, cancellationToken);
    }

    public async Task<Usuario> BuscarPorEmailUsuarioAsync(string email, CancellationToken cancellationToken)
    {
        return (await _boardlyContexto.Set<Usuario>().FirstOrDefaultAsync(us => us.Correo == email, cancellationToken))!;
    }

    public async Task<Usuario> BuscarPorNombreUsuarioAsync(string nombreUsuario, CancellationToken cancellationToken)
    {
       return (await _boardlyContexto.Set<Usuario>().FirstOrDefaultAsync(us => us.Nombre == nombreUsuario, cancellationToken))!;
    }

    public async Task<bool> EmailEnUsoAsync(string email, Guid excluirUsuarioId, CancellationToken cancellationToken)
    {
         return  await ValidarAsync(us => us.Correo == email && us.UsuarioId != excluirUsuarioId, cancellationToken);
    }

    public async Task ActualizarContrasenaAsync(Usuario usuario, string newHashedPassword, CancellationToken cancellationToken)
    {
        usuario.Contrasena = newHashedPassword;
        await _boardlyContexto.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await ValidarAsync(us => us.Correo == email, cancellationToken);
    }

    public async Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario, CancellationToken cancellationToken)
    {
        return await ValidarAsync(us => us.NombreUsuario == nombreUsuario, cancellationToken);
    }
    
}