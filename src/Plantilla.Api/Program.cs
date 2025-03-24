using System.Reflection;
using Plantilla.Api.Extensiones;
using Plantilla.Api.Middleware;
using Plantilla.Entidad;
using Plantilla.Infraestructura.Constantes;
using Plantilla.Infraestructura.Services;
using Plantilla.Negocio;
using Plantilla.RepositorioEfCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

InformacionApi.Nombre = Assembly.GetExecutingAssembly().GetName().Name!;

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddControllersWithViews();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:5173")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
});

// Adding Authentication
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigurarInfraestructura();
builder.Services.ConfigurarClasesDesdeAppSetting(builder.Configuration);
builder.Services.ConfigurarAutenticacionAutorizacion(builder.Configuration);
builder.Services.ConfigurarContextos(builder.Configuration);
builder.Services.ConfigurarBaseDatos(connectionString);
builder.Services.ConfigurarRepositorioSqlServer();
builder.Services.ConfigurarIdentity(builder.Configuration);

builder.Services.ConfigurarSesion();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigurarSerilog(builder);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigurarNegocio();

var app = builder.Build();

await app.EjecutarMigracionAlIniciar();
await app.EjecutarCargaInicial();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.UseMiddleware<LogMiddleware>();
app.AgregarOperacionesApi(builder.Environment.EnvironmentName);
app.MapControllers();

await app.RunAsync();