namespace GestionEmploiTemps.API.Models
{
    public class Enseigner
    {
        public int IdEns { get; set; }
        public required Enseignant Enseignant { get; set; }

        public int IdMatiere { get; set; }
        public required Matiere Matiere { get; set; }
    }
}
