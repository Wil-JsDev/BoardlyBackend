using System.Text;
using Boardly.Aplicacion.DTOs.Email;
using Boardly.Aplicacion.DTOs.JWT;
using Boardly.Dominio.Configuraciones;
using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.CasosDeUso.Autenticacion;
using Boardly.Dominio.Puertos.Cloudinary;
using Boardly.Dominio.Puertos.Email;
using Boardly.Infraestructura.Compartido.Adaptadores;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Boardly.Infraestructura.Compartido;

public static class InyeccionDeDependencia
{
    public static void AgregarCompartido(this IServiceCollection servicios, IConfiguration configuracion)
    {

        
        #region Configuraciones
        
            servicios.Configure<CorreoConfiguraciones>(configuracion.GetSection("CorreoConfiguraciones"));
            servicios.Configure<CloudinaryConfiguraciones>(configuracion.GetSection("CloudinaryConfiguraciones"));  
            servicios.Configure<CloudinaryConfiguraciones>(configuracion.GetSection("JwtConfiguraciones"));   
            
        #endregion

        #region JWT
        
            servicios.Configure<JwtConfiguraciones>(configuracion.GetSection("JWTConfiguraciones"));
            servicios.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuracion["JWTConfiguraciones:Emisor"],
                    ValidAudience = configuracion["JWTConfiguraciones:Audiencia"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracion["JWTConfiguraciones:Clave"]))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "application/json";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },

                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtRespuesta(true, "Ocurrió un error inesperado en la autenticación"));
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtRespuesta(true,
                            "No estás autorizado para acceder a este contenido"));

                        return c.Response.WriteAsync(result);
                    }
                };

            });    
        
        
        #endregion
        
        #region Servicios
        
            servicios.AddScoped<ICorreoServicio<SolicitudCorreo>, EnviadorDeCorreos>();
            servicios.AddScoped<ICloudinaryServicio, CloudinaryServicio>();
            servicios.AddScoped<IGenerarToken<Usuario>, GenerarToken>();
            servicios.AddScoped<ITokenRefrescado<TokenRefrescadoDto>, TokenRefrescado>();

        #endregion
    }
}