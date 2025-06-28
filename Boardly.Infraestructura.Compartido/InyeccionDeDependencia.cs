using Boardly.Aplicacion.DTOs.Email;
using Boardly.Dominio.Configuraciones;
using Boardly.Dominio.Puertos.Cloudinary;
using Boardly.Dominio.Puertos.Email;
using Boardly.Infraestructura.Compartido.Adaptadores;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boardly.Infraestructura.Compartido;

public static class InyeccionDeDependencia
{
    public static void AgregarCompartido(this IServiceCollection servicios, IConfiguration coonfiguracion)
    {

        servicios.AddScoped<ICorreoServicio<SolicitudCorreo>, EnviadorDeCorreos>();
        servicios.AddScoped<ICloudinaryServicio, CloudinaryServicio>();
        servicios.Configure<CorreoConfiguraciones>(coonfiguracion.GetSection("MailSettings"));
        servicios.Configure<CloudinaryConfiguraciones>(coonfiguracion.GetSection("CloudinaryConfiguraciones"));   
    }
}