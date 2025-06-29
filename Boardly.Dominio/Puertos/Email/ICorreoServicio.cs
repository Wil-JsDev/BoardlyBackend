namespace Boardly.Dominio.Puertos.Email;

public interface ICorreoServicio<TEstadoDto>
{
    Task Execute(TEstadoDto dto);

}