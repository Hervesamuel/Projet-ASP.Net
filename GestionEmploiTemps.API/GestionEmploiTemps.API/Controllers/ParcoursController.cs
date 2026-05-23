using Microsoft.AspNetCore.Mvc;
using GestionEmploiTemps.API.Data;
using GestionEmploiTemps.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmploiTemps.API.Controllers
{
    [ApiController] // API REST
    [Route("api/[controller]")] // api/parcours
    public class ParcoursController : ControllerBase
    {
        private readonly AppDbContext _context;

        // 🔹 Injection du DbContext
        public ParcoursController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // 🔹 GET : tous les parcours
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parcours>>> GetParcours()
        {
            var parcours = await _context.Parcours
                .Include(p => p.Niveau)   // 🔥 récupérer le niveau lié
                .Include(p => p.Seances)  // 🔥 récupérer les séances liées
                .ToListAsync();

            return Ok(parcours);
        }

        // =========================
        // 🔹 GET : parcours par ID
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Parcours>> GetParcoursById(int id)
        {
            var parcours = await _context.Parcours
                .Include(p => p.Niveau)
                .Include(p => p.Seances)
                .FirstOrDefaultAsync(p => p.IdParcours == id);

            if (parcours == null)
                return NotFound("Parcours non trouvé");

            return Ok(parcours);
        }

        // =========================
        // 🔹 POST : ajouter parcours
        // =========================
        [HttpPost]
        public async Task<ActionResult<Parcours>> CreateParcours(Parcours parcours)
        {
            // 🔥 Vérifier si le niveau existe
            var niveau = await _context.Niveaux.FindAsync(parcours.IdNiveau);

            if (niveau == null)
                return BadRequest("Niveau invalide");

            _context.Parcours.Add(parcours);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetParcoursById), new { id = parcours.IdParcours }, parcours);
        }

        // =========================
        // 🔹 PUT : modifier parcours
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParcours(int id, Parcours parcours)
        {
            var existing = await _context.Parcours.FindAsync(id);

            if (existing == null)
                return NotFound("Parcours non trouvé");

            // 🔥 Mise à jour
            existing.Nom = parcours.Nom;
            existing.IdNiveau = parcours.IdNiveau;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // =========================
        // 🔹 DELETE : supprimer parcours
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParcours(int id)
        {
            var parcours = await _context.Parcours.FindAsync(id);

            if (parcours == null)
                return NotFound("Parcours non trouvé");

            _context.Parcours.Remove(parcours);
            await _context.SaveChangesAsync();

            return Ok("Parcours supprimé");
        }
    }
}