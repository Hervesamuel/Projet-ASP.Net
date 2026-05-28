using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEmploiTemps.API.Models
{
    public class Enseigner
    {
        // Clé étrangère Enseignant
        [Required]
        public int IdEns { get; set; }

        [ForeignKey(nameof(IdEns))]
        public Enseignant? Enseignant { get; set; }

        //  Clé étrangère Matiere
        [Required]
        public int IdMatiere { get; set; }

        [ForeignKey(nameof(IdMatiere))]
        public Matiere? Matiere { get; set; }
    }
}