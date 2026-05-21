using System.ComponentModel.DataAnnotations;

namespace GestionEmploiTemps.API.Models
{
    public class Matiere
    {
        [Key]
        public int IdMatiere { get; set; }

        [Required]
        public string Nom { get; set; }

        public ICollection<Enseigner> Enseignants { get; set; }
        public ICollection<Seance> Seances { get; set; }
    }
}
