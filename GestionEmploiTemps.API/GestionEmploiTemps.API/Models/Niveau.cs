using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace GestionEmploiTemps.API.Models
{
    public class Niveau
    {
        [Key]
        public int IdNiveau { get; set; }

        [Required]
        public required string Nom { get; set; }

        public required ICollection<Parcours> Parcours { get; set; }
    }
}
