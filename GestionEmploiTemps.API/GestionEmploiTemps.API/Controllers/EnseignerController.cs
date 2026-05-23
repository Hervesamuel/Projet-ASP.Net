using Microsoft.AspNetCore.Mvc;
using GestionEmploiTemps.API.Data;
using GestionEmploiTemps.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmploiTemps.API.Controllers
{
    [ApiController] // API REST
    [Route("api/[controller]")] // api/enseigner
    public class EnseignerController : ControllerBase
    {
        private readonly AppDbContext _context;

        // 🔹 Injection DbContext
        public EnseignerController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // 🔹 GET : toutes les relations
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enseigner>>> GetEnseignements()
        {
            var list = await _context.Enseigners
                .Include(e => e.Enseignant) // 🔥 récupérer le prof
                .Include(e => e.Matiere)    // 🔥 récupérer la matière
                .ToListAsync();

            return Ok(list);
        }

        // =========================
        // 🔹 GET : par IdEns + IdMatiere
        // =========================
        [HttpGet("{idEns}/{idMatiere}")]
        public async Task<ActionResult<Enseigner>> GetEnseigner(int idEns, int idMatiere)
        {
            var enseigner = await _context.Enseigners
                .Include(e => e.Enseignant)
                .Include(e => e.Matiere)
                .FirstOrDefaultAsync(e => e.IdEns == idEns && e.IdMatiere == idMatiere);

            if (enseigner == null)
                return NotFound("Relation non trouvée");

            return Ok(enseigner);
        }

        // =========================
        // 🔹 POST : ajouter relation
        // =========================
        [HttpPost]
        public async Task<ActionResult<Enseigner>> CreateEnseigner(Enseigner enseigner)
        {
            // 🔥 Vérifier si déjà existe (éviter doublon)
            var exists = await _context.Enseigners
                .AnyAsync(e => e.IdEns == enseigner.IdEns && e.IdMatiere == enseigner.IdMatiere);

            if (exists)
                return BadRequest("Cette relation existe déjà");

            _context.Enseigners.Add(enseigner);
            await _context.SaveChangesAsync();

            return Ok(enseigner);
        }

        // =========================
        // 🔹 DELETE : supprimer relation
        // =========================
        [HttpDelete("{idEns}/{idMatiere}")]
        public async Task<IActionResult> DeleteEnseigner(int idEns, int idMatiere)
        {
            var enseigner = await _context.Enseigners
                .FirstOrDefaultAsync(e => e.IdEns == idEns && e.IdMatiere == idMatiere);

            if (enseigner == null)
                return NotFound("Relation non trouvée");

            _context.Enseigners.Remove(enseigner);
            await _context.SaveChangesAsync();

            return Ok("Relation supprimée");
        }
    }
}