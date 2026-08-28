var builder = WebApplication.CreateBuilder(args);

// area de servicios

builder.Services.AddControllers();

var app = builder.Build();

// area de middlewares

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
