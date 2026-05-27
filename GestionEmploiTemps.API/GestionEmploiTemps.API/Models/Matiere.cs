using System.ComponentModel.DataAnnotations;

namespace GestionEmploiTemps.API.Models
{
    public class Matiere
    {
        [Key]
        public int IdMatiere { get; set; }

        [Required]
        public required string Nom { get; set; }

        public List <Enseigner>? Enseignants { get; set; }
        public List <Seance>? Seances { get; set; }
    }
}
