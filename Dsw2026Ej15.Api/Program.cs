using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar Health Checks
builder.Services.AddHealthChecks();

// Registrar la persistencia como Singleton
builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();

var app = builder.ApplicationServices.CreateBuilder();

var appBuild = builder.Build();

// Configurar el Middleware para manejo de excepciones
appBuild.UseMiddleware<ExceptionMiddleware>();

// Configurar el pipeline de solicitudes HTTP
if (appBuild.Environment.IsDevelopment())
{
    appBuild.UseSwagger();
    appBuild.UseSwaggerUI();
}

appBuild.UseHttpsRedirection();
appBuild.UseAuthorization();
appBuild.MapControllers();

// Mapear el endpoint de Health Check
appBuild.MapHealthChecks("/health-check");

appBuild.Run();