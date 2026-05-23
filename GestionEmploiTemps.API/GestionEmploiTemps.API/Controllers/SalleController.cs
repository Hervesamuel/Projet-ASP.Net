using Microsoft.AspNetCore.Mvc;
using GestionEmploiTemps.API.Data;
using GestionEmploiTemps.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmploiTemps.API.Controllers
{
    [ApiController] // Indique que c’est une API
    [Route("api/[controller]")] // URL : api/salle
    public class SalleController : ControllerBase
    {
        private readonly AppDbContext _context; // Accès base de données

        // Constructeur (injection dépendance)
        public SalleController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // 🔹 GET : toutes les salles
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salle>>> GetSalles()
        {
            var salles = await _context.Salles.ToListAsync();
            return Ok(salles); // retourne liste
        }

        // =========================
        // 🔹 GET : salle par ID
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Salle>> GetSalle(int id)
        {
            var salle = await _context.Salles.FindAsync(id);

            if (salle == null)
                return NotFound("Salle non trouvée");

            return Ok(salle);
        }

        // =========================
        // 🔹 POST : ajouter salle
        // =========================
        [HttpPost]
        public async Task<ActionResult<Salle>> CreateSalle(Salle salle)
        {
            _context.Salles.Add(salle); // ajouter
            await _context.SaveChangesAsync(); // sauvegarder

            return CreatedAtAction(nameof(GetSalle), new { id = salle.IdSalle }, salle);
        }

        // =========================
        // 🔹 PUT : modifier salle
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalle(int id, Salle salle)
        {
            var existing = await _context.Salles.FindAsync(id);

            if (existing == null)
                return NotFound("Salle non trouvée");

            // Modifier champs
            existing.Nom = salle.Nom;
            existing.Capacite = salle.Capacite;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // =========================
        // 🔹 DELETE : supprimer salle
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalle(int id)
        {
            var salle = await _context.Salles.FindAsync(id);

            if (salle == null)
                return NotFound("Salle non trouvée");

            _context.Salles.Remove(salle);
            await _context.SaveChangesAsync();

            return Ok("Salle supprimée");
        }
    }
}