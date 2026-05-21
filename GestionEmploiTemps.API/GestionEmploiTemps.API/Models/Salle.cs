using System.ComponentModel.DataAnnotations;

namespace GestionEmploiTemps.API.Models
{
    public class Salle
    {
        [Key]
        public int IdSalle { get; set; }

        [Required]
        public required string Nom { get; set; }

        public int Capacite { get; set; }

        public required ICollection<Seance> Seances { get; set; }
    }
}
