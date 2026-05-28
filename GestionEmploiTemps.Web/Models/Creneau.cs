namespace GestionEmploiTemps.Web.Models
{
    public class Creneau
    {
        public int IdCreneau { get; set; }

        public string Jour { get; set; } = string.Empty;

        public TimeSpan HeureDebut { get; set; }

        public TimeSpan HeureFin { get; set; }
    }
}