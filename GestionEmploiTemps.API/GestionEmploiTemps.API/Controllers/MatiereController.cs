using GestionEmploiTemps.API.Data;
using GestionEmploiTemps.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionEmploiTemps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatiereController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MatiereController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Matiere
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matiere>>> GetMatieres()
        {
            try
            {
                var matieres = await _context.Matieres
                    .Include(m => m.Enseignants)
                    .Include(m => m.Seances)
                    .ToListAsync();

                return Ok(matieres);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"Erreur lors de la récupération des matières : {ex.Message}");
            }
        }

        // GET: api/Matiere/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Matiere>> GetMatiere(int id)
        {
            try
            {
                var matiere = await _context.Matieres
                    .Include(m => m.Enseignants)
                    .Include(m => m.Seances)
                    .FirstOrDefaultAsync(m => m.IdMatiere == id);

                if (matiere == null)
                {
                    return NotFound("Matière introuvable.");
                }

                return Ok(matiere);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"Erreur lors de la récupération : {ex.Message}");
            }
        }

        // POST: api/Matiere
        [HttpPost]
        public async Task<ActionResult> CreateMatiere(Matiere matiere)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (string.IsNullOrWhiteSpace(matiere.Nom))
                    return BadRequest("Le nom de la matière est obligatoire.");

                bool existe = await _context.Matieres
                    .AnyAsync(m => m.Nom.ToLower() == matiere.Nom.ToLower());

                if (existe)
                    return BadRequest("Cette matière existe déjà.");

                await _context.Matieres.AddAsync(matiere);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMatiere),
                    new { id = matiere.IdMatiere },
                    matiere);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"Erreur lors de l'ajout : {ex.Message}");
            }
        }

        // PUT: api/Matiere/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatiere(int id, Matiere matiere)
        {
            try
            {
                if (id != matiere.IdMatiere)
                    return BadRequest("ID invalide.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existe = await _context.Matieres.AnyAsync(m => m.IdMatiere == id);

                if (!existe)
                    return NotFound("Matière introuvable.");

                _context.Entry(matiere).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok("Matière modifiée avec succès.");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500,
                    $"Erreur base de données : {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"Erreur lors de la modification : {ex.Message}");
            }
        }

        // DELETE: api/Matiere/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatiere(int id)
        {
            try
            {
                var matiere = await _context.Matieres
                    .FirstOrDefaultAsync(m => m.IdMatiere == id);

                if (matiere == null)
                    return NotFound("Matière introuvable.");

                _context.Matieres.Remove(matiere);
                await _context.SaveChangesAsync();

                return Ok("Matière supprimée avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"Erreur lors de la suppression : {ex.Message}");
            }
        }
    }
}