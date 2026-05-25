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
    public class EnseignantController : ControllerBase
    {
        private readonly AppBdContext _context;

        // Injection du DbContext
        public EnseignantController(AppBdContext context)
        {
            _context = context;
        }

        // GET : api/Enseignant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enseignant>>> GetEnseignants()
        {
            try
            {
                var enseignants = await _context.Enseignants
                    .Include(e => e.Enseignements)
                    .Include(e => e.Seances)
                    .ToListAsync();

                return Ok(enseignants);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    $"Erreur lors de la récupération des enseignants : {ex.Message}"
                );
            }
        }

        // GET : api/Enseignant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enseignant>> GetEnseignant(int id)
        {
            try
            {
                var enseignant = await _context.Enseignants
                    .Include(e => e.Enseignements)
                    .Include(e => e.Seances)
                    .FirstOrDefaultAsync(e => e.IdEns == id);

                if (enseignant == null)
                {
                    return NotFound("Enseignant introuvable.");
                }

                return Ok(enseignant);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    $"Erreur lors de la récupération : {ex.Message}"
                );
            }
        }

        // POST : api/Enseignant
        [HttpPost]
        public async Task<ActionResult<Enseignant>> CreateEnseignant(
            Enseignant enseignant)
        {
            try
            {
                // Vérifie les données
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Vérifie si le nom est vide
                if (string.IsNullOrWhiteSpace(enseignant.Nom))
                {
                    return BadRequest(
                        "Le nom de l'enseignant est obligatoire."
                    );
                }

                // Vérifie si l'enseignant existe déjà
                bool existe = await _context.Enseignants
                    .AnyAsync(e => e.Nom.ToLower() ==
                                   enseignant.Nom.ToLower());

                if (existe)
                {
                    return BadRequest(
                        "Cet enseignant existe déjà."
                    );
                }

                await _context.Enseignants.AddAsync(enseignant);

                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetEnseignant),
                    new { id = enseignant.IdEns },
                    enseignant
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    $"Erreur lors de l'ajout : {ex.Message}"
                );
            }
        }

        // PUT : api/Enseignant/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnseignant(
            int id,
            Enseignant enseignant)
        {
            try
            {
                // Vérifie si l'ID correspond
                if (id != enseignant.IdEns)
                {
                    return BadRequest("ID non valide.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existe = await _context.Enseignants
                    .AnyAsync(e => e.IdEns == id);

                if (!existe)
                {
                    return NotFound(
                        "Enseignant introuvable."
                    );
                }

                _context.Entry(enseignant).State =
                    EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok(
                    "Enseignant modifié avec succès."
                );
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(
                    500,
                    $"Erreur base de données : {ex.Message}"
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    $"Erreur lors de la modification : {ex.Message}"
                );
            }
        }

        // DELETE : api/Enseignant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnseignant(int id)
        {
            try
            {
                var enseignant = await _context.Enseignants
                    .FirstOrDefaultAsync(e => e.IdEns == id);

                if (enseignant == null)
                {
                    return NotFound(
                        "Enseignant introuvable."
                    );
                }

                _context.Enseignants.Remove(enseignant);

                await _context.SaveChangesAsync();

                return Ok(
                    "Enseignant supprimé avec succès."
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    $"Erreur lors de la suppression : {ex.Message}"
                );
            }
        }
    }
}