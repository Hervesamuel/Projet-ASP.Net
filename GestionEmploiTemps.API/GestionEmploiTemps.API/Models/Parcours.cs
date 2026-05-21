
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEmploiTemps.API.Models
{
    public class Parcours
    {
        [Key]
        public int IdParcours { get; set; }

        [Required]
        public string Nom { get; set; }

        // FK
        public int IdNiveau { get; set; }

        [ForeignKey("IdNiveau")]
        public Niveau Niveau { get; set; }

        // Navigation
        public ICollection<Seance> Seances { get; set; }
    }
}
