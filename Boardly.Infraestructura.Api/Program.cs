using Boardly.Aplicacion;
using Boardly.Infraestructura.Api.ServiciosDeExtensiones;
using Boardly.Infraestructura.Compartido;
using Boardly.Infraestructura.Compartido.Adaptadores.SignaIR.Hubs;
using Boardly.Infraestructura.Persistencia;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuracion = builder.Configuration;
    
    Log.Information("Iniciando servidor");

    builder.Host.UseSerilog((context, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    });
    
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowLocalhost",
            builder => builder.WithOrigins("http://localhost:4200")  
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("Content-Disposition"));
    });
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    builder.Services.AgregarVersionado();
    builder.Services.AgregarValidaciones();
    builder.Services.AgregarPersistencia(configuracion);
    builder.Services.AgregarAplicacion();
    builder.Services.AgregarCompartido(configuracion);

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    
    app.AgregarMiddlewares();
    
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    
    app.UseCors("AllowLocalhost");
    
    app.UseRouting();

    app.UseWebSockets();
    
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.MapHub<TareasHub>("/hubs/tareas");
    
    app.Run();
    
}
catch (Exception ex)
{
    Log.Fatal(ex,"Ha ocurrido un error");
    Console.WriteLine(ex);
}
finally
{
    Log.CloseAndFlush();
}
