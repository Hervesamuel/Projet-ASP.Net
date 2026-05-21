using Microsoft.EntityFrameworkCore;
using GestionEmploiTemps.API.Models;

namespace GestionEmploiTemps.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Salle> Salle { get; set; }
        public DbSet<Enseignant> Enseignant { get; set; }
        public DbSet<Matiere> Matiere { get; set; }
        public DbSet<Matiere> Niveau { get; set; }
    }
}