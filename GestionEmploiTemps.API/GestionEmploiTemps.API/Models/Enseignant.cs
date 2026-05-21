using System.ComponentModel.DataAnnotations;

namespace GestionEmploiTemps.API.Models
{
    public class Enseignant
    {
        [Key]
        public int IdEns { get; set; }

        [Required]
        public required string Nom { get; set; }

        public required ICollection<Enseigner> Enseignements { get; set; }
        public required ICollection<Seance> Seances { get; set; }
    }
}
