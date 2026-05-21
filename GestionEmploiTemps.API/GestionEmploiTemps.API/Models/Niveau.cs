using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace GestionEmploiTemps.API.Models
{
    public class Niveau
    {
        [Key]
        public int IdNiveau { get; set; }

        [Required]
        public string Nom { get; set; }

        public ICollection<Parcours> Parcours { get; set; }
    }
}
