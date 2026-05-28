namespace GestionEmploiTemps.Web.Models
{
    public class Parcours
    {
        public int IdParcours { get; set; }

        public string Nom { get; set; } = string.Empty;

        public int IdNiveau { get; set; }
        public string NomNiveau { get; set; } = string.Empty;
    }
}