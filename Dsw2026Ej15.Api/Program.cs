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

// CONSTRUIR LA APLICACIÓN
var app = builder.Build();

// Configurar el Middleware para manejo de excepciones
app.UseMiddleware<ExceptionMiddleware>();

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Mapear el endpoint de Health Check
app.MapHealthChecks("/health-check");

app.Run();