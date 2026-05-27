using System.ComponentModel.DataAnnotations;

namespace GestionEmploiTemps.API.Models
{
    public class Enseignant
    {
        [Key]
        public int IdEns { get; set; }

        [Required]
        public string Nom { get; set; } = string.Empty;

        // Relations → pas obligatoires au POST
        public List<Enseigner>? Enseignements { get; set; }

        public List<Seance>? Seances { get; set; }
    }
}