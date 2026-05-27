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
        [ForeignKey("IdParcours")]
        public Parcours? Parcours { get; set; }

        public int IdMatiere { get; set; }
        [ForeignKey("IdMatiere")]
        public Matiere? Matiere { get; set; }

        public int IdEns { get; set; }
        [ForeignKey("IdEns")]
        public Enseignant? Enseignant { get; set; }

        public int IdSalle { get; set; }
        [ForeignKey("IdSalle")]
        public Salle? Salle { get; set; }

        public int IdCreneau { get; set; }
        [ForeignKey("IdCreneau")]
        public Creneau? Creneau { get; set; }
    }
}