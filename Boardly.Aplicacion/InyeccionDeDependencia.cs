using Boardly.Aplicacion.Adaptadores.Codigo;
using Boardly.Aplicacion.DTOs.Codigo;
using Boardly.Dominio.Puertos.CasosDeUso;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boardly.Aplicacion;

public static class InyeccionDeDependencia
{
    public static void AgregarAplicacion(this IServiceCollection servicios, IConfiguration coonfiguracion)
    {

        #region Codigo
        servicios.AddScoped<ICrearCodigo, CrearCodigo>();
        servicios.AddScoped<IObtenerCodigo<CodigoDto>,ObtenerCodigo>();
        servicios.AddScoped<ICodigoDisponible<Resultado>, CodigoDisponible>();
        servicios.AddScoped<IConfirmarCuenta<Resultado>, ConfirmarCuenta>();
        servicios.AddScoped<IEliminarCodigo<Resultado>, EliminarCodigo>();

        #endregion
    }
}