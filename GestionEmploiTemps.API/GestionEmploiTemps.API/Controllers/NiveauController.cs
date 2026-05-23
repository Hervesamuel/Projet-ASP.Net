using Microsoft.AspNetCore.Mvc;
using GestionEmploiTemps.API.Data;
using GestionEmploiTemps.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmploiTemps.API.Controllers
{
    [ApiController] // API REST
    [Route("api/[controller]")] // api/niveau
    public class NiveauController : ControllerBase
    {
        private readonly AppDbContext _context;

        // 🔹 Injection du DbContext
        public NiveauController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // 🔹 GET : tous les niveaux
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Niveau>>> GetNiveaux()
        {
            var niveaux = await _context.Niveaux
                .Include(n => n.Parcours) // 🔥 récupérer les parcours liés
                .ToListAsync();

            return Ok(niveaux);
        }

        // =========================
        // 🔹 GET : niveau par ID
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Niveau>> GetNiveau(int id)
        {
            var niveau = await _context.Niveaux
                .Include(n => n.Parcours)
                .FirstOrDefaultAsync(n => n.IdNiveau == id);

            if (niveau == null)
                return NotFound("Niveau non trouvé");

            return Ok(niveau);
        }

        // =========================
        // 🔹 POST : ajouter niveau
        // =========================
        [HttpPost]
        public async Task<ActionResult<Niveau>> CreateNiveau(Niveau niveau)
        {
            _context.Niveaux.Add(niveau);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNiveau), new { id = niveau.IdNiveau }, niveau);
        }

        // =========================
        // 🔹 PUT : modifier niveau
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNiveau(int id, Niveau niveau)
        {
            var existing = await _context.Niveaux.FindAsync(id);

            if (existing == null)
                return NotFound("Niveau non trouvé");

            // 🔥 Mise à jour du nom seulement
            existing.Nom = niveau.Nom;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // =========================
        // 🔹 DELETE : supprimer niveau
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNiveau(int id)
        {
            var niveau = await _context.Niveaux.FindAsync(id);

            if (niveau == null)
                return NotFound("Niveau non trouvé");

            _context.Niveaux.Remove(niveau);
            await _context.SaveChangesAsync();

            return Ok("Niveau supprimé");
        }
    }
}