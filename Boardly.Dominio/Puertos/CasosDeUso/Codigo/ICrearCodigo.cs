using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Codigo;

public interface ICrearCodigo
{
    Task <ResultadoT<string>> CrearCodigoAsync(Guid usuarioId, CancellationToken cancellationToken);
}