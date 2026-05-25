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
    public class UtilisateurController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UtilisateurController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _context.Utilisateurs.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Utilisateur u)
        {
            try
            {
                u.DateCreation = DateTime.Now;

                await _context.Utilisateurs.AddAsync(u);
                await _context.SaveChangesAsync();

                return Ok("Utilisateur ajouté");
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
                var u = await _context.Utilisateurs.FindAsync(id);
                if (u == null) return NotFound();

                _context.Utilisateurs.Remove(u);
                await _context.SaveChangesAsync();

                return Ok("Supprimé");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}