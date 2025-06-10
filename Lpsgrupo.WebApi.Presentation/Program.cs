using Lpsgrupo.WebApi.Presentation.Data;
using Lpsgrupo.WebApi.Presentation.Interfaces;
using Lpsgrupo.WebApi.Presentation.Interfaces.Services;
using Lpsgrupo.WebApi.Presentation.Middlewares;
using Lpsgrupo.WebApi.Presentation.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Nueva configuracion de MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
// Registramos MongoDbContext
builder.Services.AddSingleton<MongoDbContext>();

// Agregar servicios al contenedor
builder.Services.AddSingleton<ITaskService, TaskService>();

// Registrar IApiKeyService y ApiKeyService antes de usar el middleware
builder.Services.AddSingleton<IApiKeyService, ApiKeyService>();  // Registrar ApiKeyService

// Habilitar autenticación JWT (Descomentado si se requiere JWT)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtClientSettings:Issuer"],  // Issuer del JWT
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtClientSettings:Audience"],  // Audience del JWT
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,  // No tolerar ningún desfase de tiempo
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtClientSettings:AccessTokenSecret"]))  // Clave secreta para firmar el token
        };
    });

// Habilitar la autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiAccess", policy =>
    {
        policy.RequireAuthenticatedUser();  // Política que requiere que el usuario esté autenticado
    });
});

// Configuración de controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Construir la aplicación
var app = builder.Build();

// Habilitar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ApiKeyMiddleware>();  // Validar API Key
app.UseHttpsRedirection(); 

app.UseAuthentication();  // Verifica el JWT
app.UseAuthorization();   // Autoriza según la política definida

// Mapea los controladores a las rutas
app.MapControllers();

// Configuración de Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lpsgrupo API V1");
    c.RoutePrefix = string.Empty;  // Swagger UI en la raíz
});

// Ejecutar la aplicación
app.Run();
