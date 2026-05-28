using GestionEmploiTemps.Web.Models;

namespace GestionEmploiTemps.Web.Models.ViewModels
{
    public class AcademiqueVM
    {
        public List<Niveau> Niveaux { get; set; } = new();

        public List<Parcours> Parcours { get; set; } = new();
        public string NouveauNiveau { get; set; } = string.Empty;
        public string NouveauParcours { get; set; } = string.Empty;

        public int IdNiveau { get; set; }
        public int IdParcours { get; set; }
    }
}