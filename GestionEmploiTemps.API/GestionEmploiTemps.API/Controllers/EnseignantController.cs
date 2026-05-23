using Microsoft.AspNetCore.Mvc;
using GestionEmploiTemps.API.Data;
using GestionEmploiTemps.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmploiTemps.API.Controllers
{
    [ApiController] // Indique que c’est une API REST
    [Route("api/[controller]")] // URL : api/enseignant
    public class EnseignantController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Injection du DbContext
        public EnseignantController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // 🔹 GET : tous les enseignants
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enseignant>>> GetEnseignants()
        {
            var enseignants = await _context.Enseignants
                .Include(e => e.Enseignements) // relation avec table Enseigner
                .Include(e => e.Seances)       // relation avec Seance
                .ToListAsync();

            return Ok(enseignants);
        }

        // =========================
        // 🔹 GET : enseignant par ID
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Enseignant>> GetEnseignant(int id)
        {
            var enseignant = await _context.Enseignants
                .Include(e => e.Enseignements)
                .Include(e => e.Seances)
                .FirstOrDefaultAsync(e => e.IdEns == id);

            if (enseignant == null)
                return NotFound("Enseignant non trouvé");

            return Ok(enseignant);
        }

        // =========================
        // 🔹 POST : ajouter enseignant
        // =========================
        [HttpPost]
        public async Task<ActionResult<Enseignant>> CreateEnseignant(Enseignant enseignant)
        {
            _context.Enseignants.Add(enseignant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEnseignant), new { id = enseignant.IdEns }, enseignant);
        }

        // =========================
        // 🔹 PUT : modifier enseignant
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnseignant(int id, Enseignant enseignant)
        {
            var existing = await _context.Enseignants.FindAsync(id);

            if (existing == null)
                return NotFound("Enseignant non trouvé");

            // ✅ Seulement Nom (car pas de Prenom dans ton modèle)
            existing.Nom = enseignant.Nom;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // =========================
        // 🔹 DELETE : supprimer
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnseignant(int id)
        {
            var enseignant = await _context.Enseignants.FindAsync(id);

            if (enseignant == null)
                return NotFound("Enseignant non trouvé");

            _context.Enseignants.Remove(enseignant);
            await _context.SaveChangesAsync();

            return Ok("Enseignant supprimé");
        }
    }
}