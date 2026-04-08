using AutoCita.Api.Core.Security;
using AutoCita.Api.Data;
using AutoCita.Api.Interfaces.Repositories;
using AutoCita.Api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddSingleton<PasswordHasher>();
builder.Services.AddScoped<IAgendaRepository, AgendaRepository>();
builder.Services.AddScoped<ICitaRepository, CitaRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IServicioRepository, ServicioRepository>();
builder.Services.AddScoped<ITipoDocumentosRepository, TipoDocumentosRepository>();
builder.Services.AddScoped<IAutenticacionRepository, AutenticacionRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();
var secretKey = builder.Configuration["ApiSettings:SecretKey"]
    ?? throw new InvalidOperationException("La clave secreta JWT no está configurada en ApiSettings:SecretKey.");
var keyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddControllers(options =>
    options.Filters.Add(new AuthorizeFilter()));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
