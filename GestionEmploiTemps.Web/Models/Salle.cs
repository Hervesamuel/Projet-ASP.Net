namespace GestionEmploiTemps.Web.Models
{
    public class Salle
    {
        public int IdSalle { get; set; }

        public string Nom { get; set; } = string.Empty;

        public int Capacite { get; set; }
    }
}