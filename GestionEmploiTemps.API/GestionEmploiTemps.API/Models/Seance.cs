using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEmploiTemps.API.Models
{
    public class Seance
    {
        [Key]
        public int IdSeance { get; set; }

        // FK
        public int IdParcours { get; set; }
        public int IdMatiere { get; set; }
        public int IdEns { get; set; }
        public int IdSalle { get; set; }
        public int IdCreneau { get; set; }

        // Navigation
        [ForeignKey("IdParcours")]
        public required Parcours Parcours { get; set; }

        [ForeignKey("IdMatiere")]
        public required Matiere Matiere { get; set; }

        [ForeignKey("IdEns")]
        public required Enseignant Enseignant { get; set; }

        [ForeignKey("IdSalle")]
        public required Salle Salle { get; set; }

        [ForeignKey("IdCreneau")]
        public required Creneau Creneau { get; set; }
    }
}
