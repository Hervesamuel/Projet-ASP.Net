
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEmploiTemps.API.Models
{
    public class Parcours
    {
        [Key]
        public int IdParcours { get; set; }

        [Required]
        public required string Nom { get; set; }

        // FK
        public int IdNiveau { get; set; }
        [Required]
        [ForeignKey("IdNiveau")]
        public required Niveau Niveau { get; set; }

        // Navigation
        public required ICollection<Seance> Seances { get; set; }
    }
}
