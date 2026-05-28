namespace GestionEmploiTemps.Web.Models
{
    public class Seance
    {
        public int IdSeance { get; set; }

        public int IdParcours { get; set; }

        public int IdMatiere { get; set; }

        public int IdEns { get; set; }

        public int IdSalle { get; set; }

        public int IdCreneau { get; set; }

        // Navigation affichage
        public Parcours? Parcours { get; set; }

        public Matiere? Matiere { get; set; }

        public Enseignant? Enseignant { get; set; }

        public Salle? Salle { get; set; }

        public Creneau? Creneau { get; set; }
    }
}