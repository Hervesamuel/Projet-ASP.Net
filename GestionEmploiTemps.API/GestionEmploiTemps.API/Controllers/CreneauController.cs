using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionEmploiTemps.API.Data;
using GestionEmploiTemps.API.Models;

namespace GestionEmploiTemps.API.Controllers
{
    [ApiController] // 🔹 Indique que c’est une API REST
    [Route("api/[controller]")] // 🔹 route = api/creneau
    public class CreneauController : ControllerBase
    {
        private readonly AppDbContext _context;

        // 🔹 Injection du DbContext pour accéder à la base
        public CreneauController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // 🔹 GET : tous les créneaux
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Creneau>>> GetCreneaux()
        {
            // 🔥 récupérer tous les créneaux
            return Ok(await _context.Set<Creneau>().ToListAsync());
        }

        // =========================
        // 🔹 GET : créneau par ID
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Creneau>> GetCreneau(int id)
        {
            var creneau = await _context.Set<Creneau>().FindAsync(id);

            if (creneau == null)
                return NotFound("Créneau non trouvé");

            return Ok(creneau);
        }

        // =========================
        // 🔹 POST : ajouter créneau
        // =========================
        [HttpPost]
        public async Task<ActionResult<Creneau>> CreateCreneau(Creneau creneau)
        {
            _context.Set<Creneau>().Add(creneau);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCreneau), new { id = creneau.IdCreneau }, creneau);
        }

        // =========================
        // 🔹 PUT : modifier créneau
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCreneau(int id, Creneau creneau)
        {
            var existing = await _context.Set<Creneau>().FindAsync(id);

            if (existing == null)
                return NotFound("Créneau non trouvé");

            // 🔥 mise à jour des champs
            existing.Jour = creneau.Jour;
            existing.HeureDebut = creneau.HeureDebut;
            existing.HeureFin = creneau.HeureFin;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // =========================
        // 🔹 DELETE : supprimer créneau
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreneau(int id)
        {
            var creneau = await _context.Set<Creneau>().FindAsync(id);

            if (creneau == null)
                return NotFound("Créneau non trouvé");

            _context.Set<Creneau>().Remove(creneau);
            await _context.SaveChangesAsync();

            return Ok("Créneau supprimé");
        }
    }
}