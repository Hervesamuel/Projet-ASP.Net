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
    public class EnseignerController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Injection du DbContext
        public EnseignerController(AppDbContext context)
        {
            _context = context;
        }

        // GET : api/Enseigner
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enseigner>>> GetEnseignements()
        {
            try
            {
                var enseignements = await _context.Enseignements 
                    .Include(e => e.Enseignant)
                    .Include(e => e.Matiere)
                    .ToListAsync();

                return Ok(enseignements);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    $"Erreur lors de la récupération : {ex.Message}"
                );
            }
        }

        // GET : api/Enseigner/1/2
        [HttpGet("{idEns}/{idMatiere}")]
        public async Task<ActionResult<Enseigner>> GetEnseignement(
            int idEns,
            int idMatiere)
        {
            try
            {
                var enseignement = await _context.Enseignements 
                    .Include(e => e.Enseignant)
                    .Include(e => e.Matiere)
                    .FirstOrDefaultAsync(e =>
                        e.IdEns == idEns &&
                        e.IdMatiere == idMatiere);

                if (enseignement == null)
                {
                    return NotFound("Association introuvable.");
                }

                return Ok(enseignement);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    $"Erreur : {ex.Message}"
                );
            }
        }

        // POST : api/Enseigner
        [HttpPost]
        public async Task<ActionResult> CreateEnseignement(
            Enseigner enseignement)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool existe = await _context.Enseignements 
                    .AnyAsync(e =>
                        e.IdEns == enseignement.IdEns &&
                        e.IdMatiere == enseignement.IdMatiere);

                if (existe)
                {
                    return BadRequest(
                        "Cette association existe déjà."
                    );
                }

                await _context.Enseignements
                    .AddAsync(enseignement);

                await _context.SaveChangesAsync();

                return Ok(
                    "Association ajoutée avec succès."
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        // DELETE : api/Enseigner/1/2
        [HttpDelete("{idEns}/{idMatiere}")]
        public async Task<IActionResult> DeleteEnseignement(
            int idEns,
            int idMatiere)
        {
            try
            {
                var enseignement = await _context.Enseignements
                    .FirstOrDefaultAsync(e =>
                        e.IdEns == idEns &&
                        e.IdMatiere == idMatiere);

                if (enseignement == null)
                {
                    return NotFound(
                        "Association introuvable."
                    );
                }

                _context.Enseignements
                    .Remove(enseignement);

                await _context.SaveChangesAsync();

                return Ok(
                    "Association supprimée avec succès."
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