using System.ComponentModel.DataAnnotations;

namespace GestionEmploiTemps.API.Models
{
    public class Creneau
    {
        [Key]
        public int IdCreneau { get; set; }

        [Required]
        public required string Jour { get; set; }

        public required TimeSpan HeureDebut { get; set; }

        public required TimeSpan HeureFin { get; set; }

        public required ICollection<Seance> Seances { get; set; }
    }
}
