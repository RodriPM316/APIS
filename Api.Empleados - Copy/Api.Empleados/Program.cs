using Api.Empleados;
using Api.Empleados.Datos;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// area de servicios

builder.Services.AddTransient<ServicioTransient>();
builder.Services.AddScoped<ServicioScoped>();
builder.Services.AddSingleton<ServicioSingleton>();

builder.Services.AddSingleton<IRepositorioValores, RepositorioValoresOracle>();

builder.Services.AddControllers().AddJsonOptions(opciones => 
opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Genera el documento JSON (por defecto en /openapi/v1.json)
    app.MapOpenApi();

    // Levanta la interfaz gráfica de Scalar
    app.MapScalarApiReference();
}

// area de middlewares 

app.UseLogueaPeticion();

app.UseBloqueaPeticion();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
