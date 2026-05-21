using System.ComponentModel.DataAnnotations;

namespace GestionEmploiTemps.API.Models
{
    public class Creneau
    {
        [Key]
        public int IdCreneau { get; set; }

        [Required]
        public string Jour { get; set; }

        public TimeSpan HeureDebut { get; set; }

        public TimeSpan HeureFin { get; set; }

        public ICollection<Seance> Seances { get; set; }
    }
}
