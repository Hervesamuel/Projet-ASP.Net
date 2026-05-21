using System.ComponentModel.DataAnnotations;

namespace GestionEmploiTemps.API.Models
{
    public class Matiere
    {
        [Key]
        public int IdMatiere { get; set; }

        [Required]
        public required string Nom { get; set; }

        public required ICollection<Enseigner> Enseignants { get; set; }
        public required ICollection<Seance> Seances { get; set; }
    }
}
