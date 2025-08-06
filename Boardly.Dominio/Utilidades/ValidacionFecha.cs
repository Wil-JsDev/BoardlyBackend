namespace Boardly.Dominio.Utilidades;

public static class ValidacionFecha
{
    public static async Task<bool> ValidarRangoDeFechasAsync(DateTime fechaInicio, DateTime? fechaFin, CancellationToken cancellationToken)
    {
        var hoy = DateTime.UtcNow.Date;

        bool esValido = 
            fechaFin > fechaInicio &&               // La fecha de fin debe ser posterior a la de inicio
            fechaFin > hoy;                         // La fecha de fin tampoco puede estar en el pasado

        return await Task.FromResult(esValido);
    }
}