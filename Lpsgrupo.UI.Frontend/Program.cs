using Lpsgrupo.UI.Frontend.Components;
using Lpsgrupo.UI.Frontend.Configuration;
using Lpsgrupo.UI.Frontend.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Primero, registrar los servicios en el contenedor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configuración de tu API backend
var apiSection = configuration.GetSection("ApiSettings");
var apisettings = apiSection.Get<ApiSettings>();
// Registrar la configuración de ApiSettings
builder.Services.AddHttpClient("ApiGrupoLps", client =>
{
    client.BaseAddress = new Uri(apisettings!.BaseUrl);
    client.DefaultRequestHeaders.Add("Accept","application/json");
    client.DefaultRequestHeaders.Add("X-API-KEY", apisettings.Apikey);
});

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7187/") });  // Ajusta la URL del backend

// Registrar el servicio TaskService
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<ApiService>();

// Luego, construir la aplicación
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();  // Configuración para producción
}

app.UseHttpsRedirection();
app.UseAntiforgery();  // Protección contra ataques CSRF

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Iniciar la aplicación
app.Run();
