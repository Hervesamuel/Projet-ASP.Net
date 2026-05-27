using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEmploiTemps.API.Models
{
    public class Parcours
    {
        [Key]
        public int IdParcours { get; set; }

        [Required]
        public string Nom { get; set; } = string.Empty;

        // Clé étrangère
        public int IdNiveau { get; set; }

        [ForeignKey("IdNiveau")]
        public Niveau? Niveau { get; set; }

        // Navigation
        public List<Seance>? Seances { get; set; }
    }
}