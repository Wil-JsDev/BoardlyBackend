using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empresa;

public interface ICrearEmpresa<in TSolicitud, TRespuesta> 
    where TSolicitud : class 
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> CrearEmpresaAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}