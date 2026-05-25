using GestionEmploiTemps.API.Data;
using GestionEmploiTemps.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GestionEmploiTemps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeanceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeanceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _context.Seances
                    .Include(s => s.Parcours)
                    .Include(s => s.Matiere)
                    .Include(s => s.Enseignant)
                    .Include(s => s.Salle)
                    .Include(s => s.Creneau)
                    .ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Seance s)
        {
            try
            {
                await _context.Seances.AddAsync(s);
                await _context.SaveChangesAsync();
                return Ok("Séance ajoutée");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var s = await _context.Seances.FindAsync(id);
                if (s == null) return NotFound();

                _context.Seances.Remove(s);
                await _context.SaveChangesAsync();

                return Ok("Supprimée");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}