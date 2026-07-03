using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Registrar el DbContext leyendo la cadena de conexión desde appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar la nueva persistencia con Entity Framework
builder.Services.AddScoped<IPersistence, PersistenceEf>();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.MapHealthChecks("/health-check");

app.Run();