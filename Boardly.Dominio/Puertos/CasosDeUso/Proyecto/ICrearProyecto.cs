using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Proyecto;

public interface ICrearProyecto<in TSolicitud, TRespuesta> 
    where TSolicitud : class
    where TRespuesta : class 
{
    Task<ResultadoT<TRespuesta>> CrearProyectoAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}