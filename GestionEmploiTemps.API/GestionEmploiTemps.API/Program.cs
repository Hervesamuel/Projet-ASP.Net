using Microsoft.EntityFrameworkCore;
using GestionEmploiTemps.API.Data;

var builder = WebApplication.CreateBuilder(args);

#region 🔷 CONFIGURATION DES SERVICES

builder.Services.AddControllers();

// ✅ POSTGRESQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

#endregion

var app = builder.Build();

#region 🔷 PIPELINE HTTP

if (app.Environment.IsDevelopment())
{
}

app.MapControllers();
app.UseAuthorization();

#endregion

app.Run();