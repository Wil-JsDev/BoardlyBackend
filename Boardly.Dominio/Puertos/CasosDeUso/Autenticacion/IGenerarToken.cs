namespace Boardly.Dominio.Puertos.CasosDeUso.Autenticacion;

public interface IGenerarToken<TEntidad> 
where TEntidad : class
{
    string GenerarTokenJwt(TEntidad entidad);
}