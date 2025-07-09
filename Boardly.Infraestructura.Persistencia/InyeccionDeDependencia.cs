using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Puertos.Servicios;
using Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios;
using Boardly.Infraestructura.Persistencia.Adaptadores.Repostorios.Cuentas;
using Boardly.Infraestructura.Persistencia.Adaptadores.Servicios;
using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boardly.Infraestructura.Persistencia;

public static class InyeccionDeDependencia
{
    public static void AgregarPersistencia(this IServiceCollection servicio, IConfiguration configuracion)
    {

        #region Redis

        string conexionString = configuracion.GetConnectionString("Redis")!;
        servicio.AddStackExchangeRedisCache(opciones =>
        {
            opciones.Configuration = conexionString;
        });

        #endregion
        
        
        #region DbContexto

        servicio.AddDbContext<BoardlyContexto>(postgres =>
        {
            postgres.UseNpgsql(configuracion.GetConnectionString("BoardlyBackend"), b =>
            {
                b.MigrationsAssembly("Boardly.Infraestructura.Persistencia");
            });
        });

        #endregion
        
        #region ID
        
            servicio.AddTransient(typeof(IGenericoRepositorio<>), typeof(GenericoRepositorio<>));
            servicio.AddTransient<ICeoRepositorio, CeoRepositorio>();
            servicio.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            servicio.AddTransient<IEmpleadoRepositorio, EmpleadoRepositorio>();
            servicio.AddTransient<IEmpresaRepositorio, EmpresaRepositorio>();
            servicio.AddTransient<ICodigoRepositorio, CodigoRepositorio>();
            servicio.AddTransient<IProyectoRepositorio,ProyectoRepositorio>();
            servicio.AddTransient<ITareaRepositorio, TareaRepositorio>();
            servicio.AddScoped<IObtenerRoles, ObtenerRoles>();
            servicio.AddScoped<IObtenerCeoId, ObtenerCeoId>();
            servicio.AddScoped<IObtenerEmpleadoId, ObtenerEmpleadoId>();

            #endregion

    }
    
}