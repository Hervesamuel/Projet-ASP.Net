using GestionEmploiTemps.Web.Models;

namespace GestionEmploiTemps.Web.Models.ViewModels
{
    public class SeanceVM
    {
        // Formulaire
        public Seance NouvelleSeance { get; set; } = new();

        // Données affichage
        public List<Seance> Seances { get; set; } = new();

        // Dropdowns
        public List<Enseignant> Enseignants { get; set; } = new();

        public List<Matiere> Matieres { get; set; } = new();

        public List<Salle> Salles { get; set; } = new();

        public List<Creneau> Creneaux { get; set; } = new();

        public List<Parcours> Parcours { get; set; } = new();
    }
}