using Microsoft.AspNetCore.Mvc;
using GestionEmploiTemps.API.Data;
using GestionEmploiTemps.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmploiTemps.API.Controllers
{
    [ApiController] // API REST
    [Route("api/[controller]")] // api/utilisateur
    public class UtilisateurController : ControllerBase
    {
        private readonly AppDbContext _context;

        // 🔹 Injection du DbContext
        public UtilisateurController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // 🔹 GET : tous les utilisateurs
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            var users = await _context.Set<Utilisateur>().ToListAsync();
            return Ok(users);
        }

        // =========================
        // 🔹 GET : utilisateur par ID
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
        {
            var user = await _context.Set<Utilisateur>().FindAsync(id);

            if (user == null)
                return NotFound("Utilisateur non trouvé");

            return Ok(user);
        }

        // =========================
        // 🔹 POST : créer utilisateur
        // =========================
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> CreateUtilisateur(Utilisateur utilisateur)
        {
            // 🔥 ajouter utilisateur
            _context.Set<Utilisateur>().Add(utilisateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUtilisateur), new { id = utilisateur.Id }, utilisateur);
        }

        // =========================
        // 🔹 PUT : modifier utilisateur
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUtilisateur(int id, Utilisateur utilisateur)
        {
            var existing = await _context.Set<Utilisateur>().FindAsync(id);

            if (existing == null)
                return NotFound("Utilisateur non trouvé");

            // 🔥 mise à jour des champs
            existing.Nom = utilisateur.Nom;
            existing.Prenom = utilisateur.Prenom;
            existing.Email = utilisateur.Email;
            existing.MotDePasse = utilisateur.MotDePasse;
            existing.Role = utilisateur.Role;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // =========================
        // 🔹 DELETE : supprimer utilisateur
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var user = await _context.Set<Utilisateur>().FindAsync(id);

            if (user == null)
                return NotFound("Utilisateur non trouvé");

            _context.Set<Utilisateur>().Remove(user);
            await _context.SaveChangesAsync();

            return Ok("Utilisateur supprimé");
        }
    }
}