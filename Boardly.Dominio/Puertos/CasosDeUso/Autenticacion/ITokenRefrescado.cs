namespace Boardly.Dominio.Puertos.CasosDeUso.Autenticacion;

public interface ITokenRefrescado<TEntidad>
    where TEntidad : class
{
    TEntidad GenerarTokenRefrescado();
}