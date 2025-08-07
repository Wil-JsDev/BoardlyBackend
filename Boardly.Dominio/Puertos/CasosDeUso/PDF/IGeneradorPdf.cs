namespace Boardly.Dominio.Puertos.CasosDeUso.PDF;

public interface IGeneradorPdf<T>
{
    Task<byte[]> GenerarReporteAsync(T datos);
}