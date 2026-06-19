using Dsw2026Ej15.Data;
using Dsw2026Ej15.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/health-check", () => Results.Ok("OK"));

app.Run();