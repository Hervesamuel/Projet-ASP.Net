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
    public class NiveauController : ControllerBase
    {
        private readonly AppBdContext _context;

        public NiveauController(AppBdContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Niveau>>> GetNiveaux()
        {
            try
            {
                var data = await _context.Niveaux
                    .Include(n => n.Parcours)
                    .ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur : {ex.Message}");
            }
        }

        // GET by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Niveau>> GetNiveau(int id)
        {
            try
            {
                var niveau = await _context.Niveaux
                    .Include(n => n.Parcours)
                    .FirstOrDefaultAsync(n => n.IdNiveau == id);

                if (niveau == null)
                    return NotFound("Niveau introuvable.");

                return Ok(niveau);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur : {ex.Message}");
            }
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(Niveau niveau)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _context.Niveaux.AddAsync(niveau);
                await _context.SaveChangesAsync();

                return Ok("Niveau ajouté avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur : {ex.Message}");
            }
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Niveau niveau)
        {
            try
            {
                if (id != niveau.IdNiveau)
                    return BadRequest("ID invalide");

                _context.Entry(niveau).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok("Niveau modifié");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur : {ex.Message}");
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var niveau = await _context.Niveaux.FindAsync(id);

                if (niveau == null)
                    return NotFound("Introuvable");

                _context.Niveaux.Remove(niveau);
                await _context.SaveChangesAsync();

                return Ok("Supprimé");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur : {ex.Message}");
            }
        }
    }
}