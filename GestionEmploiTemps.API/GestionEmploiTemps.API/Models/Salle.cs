using System.ComponentModel.DataAnnotations;

namespace GestionEmploiTemps.API.Models
{
    public class Salle
    {
        [Key]
        public int IdSalle { get; set; }

        [Required]
        public string Nom { get; set; }

        public int Capacite { get; set; }

        public ICollection<Seance> Seances { get; set; }
    }
}
