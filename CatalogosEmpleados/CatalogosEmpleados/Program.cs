using CatalogosEmpleados.Datos;
using CatalogosEmpleados.Servicios;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// área de servicios

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IEmpleadoNotificacionService, EmpleadoNotificacionService>();

// Agregar la conexión a SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));


builder.Services.AddControllers();

// 1. Agregamos el generador de OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // (O app.UseSwagger(); si usas Swashbuckle)

    app.MapScalarApiReference(options => {
        options.WithTitle("API Catálogos Empleados");
    });
}

// área de middlewares

app.MapControllers();

app.Run();
