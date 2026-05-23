using Microsoft.AspNetCore.Mvc;
using GestionEmploiTemps.API.Data;
using GestionEmploiTemps.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmploiTemps.API.Controllers
{
    // Indique que c’est une API REST
    [ApiController]

    // URL : api/seance
    [Route("api/[controller]")]
    public class SeanceController : ControllerBase
    {
        // Accès à la base de données
        private readonly AppDbContext _context;

        // Constructeur (injection du DbContext)
        public SeanceController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // 🔹 GET : toutes les séances
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seance>>> GetSeances()
        {
            // Include = charger les relations (IMPORTANT)
            var seances = await _context.Seances
                .Include(s => s.Parcours)
                .Include(s => s.Matiere)
                .Include(s => s.Enseignant)
                .Include(s => s.Salle)
                .Include(s => s.Creneau)
                .ToListAsync();

            return Ok(seances); // retourne 200 + données
        }

        // =========================
        // 🔹 GET : une séance par ID
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Seance>> GetSeance(int id)
        {
            var seance = await _context.Seances
                .Include(s => s.Parcours)
                .Include(s => s.Matiere)
                .Include(s => s.Enseignant)
                .Include(s => s.Salle)
                .Include(s => s.Creneau)
                .FirstOrDefaultAsync(s => s.IdSeance == id); // ⚠️ IdSeance

            // Vérification
            if (seance == null)
                return NotFound("Séance non trouvée");

            return Ok(seance);
        }

        // =========================
        // 🔹 POST : ajouter une séance
        // =========================
        [HttpPost]
        public async Task<ActionResult<Seance>> CreateSeance(Seance seance)
        {
            // Ajouter dans la base
            _context.Seances.Add(seance);

            // Sauvegarder en base
            await _context.SaveChangesAsync();

            // Retour propre avec l'ID créé
            return CreatedAtAction(
                nameof(GetSeance),                // méthode de référence
                new { id = seance.IdSeance },     // ⚠️ IdSeance
                seance
            );
        }

        // =========================
        // 🔹 PUT : modifier une séance
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeance(int id, Seance seance)
        {
            // Chercher la séance existante
            var existing = await _context.Seances.FindAsync(id);

            if (existing == null)
                return NotFound("Séance non trouvée");

            // Mise à jour des clés étrangères
            existing.IdParcours = seance.IdParcours;
            existing.IdMatiere = seance.IdMatiere;
            existing.IdEns = seance.IdEns;
            existing.IdSalle = seance.IdSalle;
            existing.IdCreneau = seance.IdCreneau;

            // Sauvegarde
            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // =========================
        // 🔹 DELETE : supprimer
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeance(int id)
        {
            var seance = await _context.Seances.FindAsync(id);

            if (seance == null)
                return NotFound("Séance non trouvée");

            // Suppression
            _context.Seances.Remove(seance);
            await _context.SaveChangesAsync();

            return Ok("Séance supprimée");
        }
    }
}