using System.ComponentModel.DataAnnotations;

namespace GestionEmploiTemps.API.Models
{
    public class Niveau
    {
        [Key]
        public int IdNiveau { get; set; }

        [Required]
        public string Nom { get; set; } = string.Empty;

        public List<Parcours>? Parcours { get; set; }
    }
}