using System.ComponentModel.DataAnnotations;

namespace GestionEmploiTemps.API.Models
{
    public class Utilisateur
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(100)]
        public required string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        [StringLength(100)]
        public required string Prenom { get; set; }

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        public required string MotDePasse { get; set; }

        [Required]
        public required string Role { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;
    }
}