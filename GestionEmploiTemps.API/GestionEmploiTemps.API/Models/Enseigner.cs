using System.ComponentModel.DataAnnotations;

namespace GestionEmploiTemps.API.Models
{
    public class Enseigner
    {
        [Key]
        public int IdEns { get; set; }
        public required Enseignant Enseignant { get; set; }

        public int IdMatiere { get; set; }
        public required Matiere Matiere { get; set; }
    }
}
