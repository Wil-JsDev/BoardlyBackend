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
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AgregarPersistencia(configuracion);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
    
}
catch (Exception ex)
{
    Log.Fatal(ex,"Ha ocurrido un error");
}
finally
{
    Log.CloseAndFlush();
}
