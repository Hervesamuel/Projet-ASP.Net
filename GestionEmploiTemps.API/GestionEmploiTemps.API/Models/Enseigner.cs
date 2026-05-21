namespace GestionEmploiTemps.API.Models
{
    public class Enseigner
    {
        public int IdEns { get; set; }
        public Enseignant Enseignant { get; set; }

        public int IdMatiere { get; set; }
        public Matiere Matiere { get; set; }
    }
}
