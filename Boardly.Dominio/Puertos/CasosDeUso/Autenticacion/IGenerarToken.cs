namespace Boardly.Dominio.Puertos.CasosDeUso.Autenticacion;

public interface IGenerarToken<TEntidad> 
where TEntidad : class
{
    Task<string> GenerarTokenJwt(TEntidad entidad, CancellationToken cancellationToken);
}