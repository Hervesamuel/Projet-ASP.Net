using Microsoft.EntityFrameworkCore;
using GestionEmploiTemps.API.Models;

namespace GestionEmploiTemps.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Niveau> Niveaux { get; set; }
        public DbSet<Parcours> Parcours { get; set; }
        public DbSet<Enseignant> Enseignants { get; set; }
        public DbSet<Matiere> Matieres { get; set; }
        public DbSet<Salle> Salles { get; set; }
        public DbSet<Creneau> Creneaux { get; set; }
        public DbSet<Enseigner> Enseigners { get; set; }
        public DbSet<Seance> Seances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //ModelBuilder = configuration avancée EF Core

            modelBuilder.Entity<Enseigner>()
                //.HasKey(...) = définit la clé primaire
                .HasKey(e => new { e.IdEns, e.IdMatiere });
        }
    }
}
