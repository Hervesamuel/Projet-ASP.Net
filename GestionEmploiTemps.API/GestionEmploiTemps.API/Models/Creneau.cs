using System.ComponentModel.DataAnnotations;

namespace GestionEmploiTemps.API.Models
{
    public class Creneau
    {
        [Key]
        public int IdCreneau { get; set; }

        [Required]
        public string Jour { get; set; } = string.Empty;

        [Required]
        public TimeSpan HeureDebut { get; set; }

        [Required]
        public TimeSpan HeureFin { get; set; }

        public List<Seance>? Seances { get; set; }
    }
}