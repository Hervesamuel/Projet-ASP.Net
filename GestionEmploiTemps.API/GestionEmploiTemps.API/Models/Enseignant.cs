using System.ComponentModel.DataAnnotations;

namespace GestionEmploiTemps.API.Models
{
    public class Enseignant
    {
        [Key]
        public int IdEns { get; set; }

        [Required]
        public string Nom { get; set; }

        public ICollection<Enseigner> Enseignements { get; set; }
        public ICollection<Seance> Seances { get; set; }
    }
}
