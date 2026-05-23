using Microsoft.AspNetCore.Mvc;
using GestionEmploiTemps.API.Data;
using GestionEmploiTemps.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmploiTemps.API.Controllers
{
    [ApiController] // Indique que c'est une API REST
    [Route("api/[controller]")] // URL : api/matiere
    public class MatiereController : ControllerBase
    {
        private readonly AppDbContext _context;

        // 🔹 Injection du DbContext (connexion base de données)
        public MatiereController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // 🔹 GET : toutes les matières
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matiere>>> GetMatieres()
        {
            var matieres = await _context.Matieres
                .Include(m => m.Enseignants) // 🔥 relation avec table Enseigner
                .Include(m => m.Seances)     // 🔥 relation avec Seance
                .ToListAsync();

            return Ok(matieres);
        }

        // =========================
        // 🔹 GET : une matière par ID
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Matiere>> GetMatiere(int id)
        {
            var matiere = await _context.Matieres
                .Include(m => m.Enseignants)
                .Include(m => m.Seances)
                .FirstOrDefaultAsync(m => m.IdMatiere == id);

            if (matiere == null)
                return NotFound("Matière non trouvée");

            return Ok(matiere);
        }

        // =========================
        // 🔹 POST : ajouter matière
        // =========================
        [HttpPost]
        public async Task<ActionResult<Matiere>> CreateMatiere(Matiere matiere)
        {
            _context.Matieres.Add(matiere); // ajout
            await _context.SaveChangesAsync(); // sauvegarde

            return CreatedAtAction(nameof(GetMatiere), new { id = matiere.IdMatiere }, matiere);
        }

        // =========================
        // 🔹 PUT : modifier matière
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatiere(int id, Matiere matiere)
        {
            var existing = await _context.Matieres.FindAsync(id);

            if (existing == null)
                return NotFound("Matière non trouvée");

            // 🔥 Mise à jour du nom seulement
            existing.Nom = matiere.Nom;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // =========================
        // 🔹 DELETE : supprimer matière
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatiere(int id)
        {
            var matiere = await _context.Matieres.FindAsync(id);

            if (matiere == null)
                return NotFound("Matière non trouvée");

            _context.Matieres.Remove(matiere);
            await _context.SaveChangesAsync();

            return Ok("Matière supprimée");
        }
    }
}