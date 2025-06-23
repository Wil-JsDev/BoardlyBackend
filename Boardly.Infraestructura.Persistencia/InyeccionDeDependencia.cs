using Boardly.Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boardly.Infraestructura.Persistencia;

public static class InyeccionDeDependencia
{
    public static void AgregarPersistencia(this IServiceCollection servicio, IConfiguration configuracion)
    {
        
        #region DbContexto

        servicio.AddDbContext<BoardlyContexto>(postgres =>
        {
            postgres.UseNpgsql(configuracion.GetConnectionString("TrivoBackend"), b =>
            {
                b.MigrationsAssembly("Boardly.Infraestructura.Persistencia");
            });
        });

        #endregion
        
        #region ID
        
        #endregion

    }
    
}