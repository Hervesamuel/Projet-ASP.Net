using Microsoft.EntityFrameworkCore;
using GestionEmploiTemps.API.Data;

var builder = WebApplication.CreateBuilder(args);

#region 🔷 CONFIGURATION DES SERVICES

// Ajout des contrôleurs (API MVC)
builder.Services.AddControllers();

// 🔷 CONFIGURATION ENTITY FRAMEWORK CORE + SQL SERVER
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// OpenAPI / Swagger (si tu l'utilises)
//builder.Services.AddOpenApi();

#endregion

var app = builder.Build();

#region 🔷 PIPELINE HTTP

// Activer OpenAPI uniquement en mode développement
if (app.Environment.IsDevelopment())
{
   // app.MapOpenApi();
}

// 🔷 IMPORTANT : routage des contrôleurs
app.MapControllers();

// 🔷 AUTHORIZATION (tu peux garder même si pas encore utilisé)
app.UseAuthorization();

#endregion

app.Run();