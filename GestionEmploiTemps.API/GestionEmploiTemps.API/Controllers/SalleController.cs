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
    public class SalleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalleController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _context.Salles
                    .Include(s => s.Seances)
                    .ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Salle s)
        {
            try
            {
                await _context.Salles.AddAsync(s);
                await _context.SaveChangesAsync();
                return Ok("Salle ajoutée");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Salle s)
        {
            try
            {
                if (id != s.IdSalle) return BadRequest();

                _context.Entry(s).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok("Modifiée");
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
                var s = await _context.Salles.FindAsync(id);
                if (s == null) return NotFound();

                _context.Salles.Remove(s);
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