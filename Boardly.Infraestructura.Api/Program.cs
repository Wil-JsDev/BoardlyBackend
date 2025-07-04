using Boardly.Aplicacion;
using Boardly.Infraestructura.Api.ServiciosDeExtensiones;
using Boardly.Infraestructura.Compartido;
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
    builder.Services.AgregarExcepciones();
    builder.Services.AgregarPersistencia(configuracion);
    builder.Services.AgregarAplicacion();
    builder.Services.AgregarCompartido(configuracion);

    var app = builder.Build();

    app.UseExceptionHandler(_ => { });
    
    app.UseSerilogRequestLogging();
    
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowLocalhost");
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
    
}
catch (Exception ex)
{
    Log.Fatal(ex,"Ha ocurrido un error");
    Console.WriteLine("ERROR al iniciar la app:");
    Console.WriteLine(ex);
}
finally
{
    Log.CloseAndFlush();
}
