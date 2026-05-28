using GestionEmploiTemps.API.Data;
using GestionEmploiTemps.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionEmploiTemps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcoursController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ParcoursController(AppDbContext context)
        {
            _context = context;
        }

        
        // GET ALL (AVEC NOM NIVEAU - VERSION PROPRE)
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await _context.Parcours
                    .Include(p => p.Niveau)
                    .Select(p => new
                    {
                        IdParcours = p.IdParcours,
                        Nom = p.Nom,
                        IdNiveau = p.IdNiveau,
                        NomNiveau = p.Niveau != null ? p.Niveau.Nom : ""
                    })
                    .ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur récupération parcours : {ex.Message}");
            }
        }

       
        //  GET BY ID
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var p = await _context.Parcours
                    .Include(x => x.Niveau)
                    .FirstOrDefaultAsync(x => x.IdParcours == id);

                if (p == null)
                    return NotFound("Parcours introuvable");

                var result = new
                {
                    IdParcours = p.IdParcours,
                    Nom = p.Nom,
                    IdNiveau = p.IdNiveau,
                    NomNiveau = p.Niveau != null ? p.Niveau.Nom : ""
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur : {ex.Message}");
            }
        }

       
        //CREATE
       
        [HttpPost]
        public async Task<IActionResult> Create(Parcours p)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(p.Nom))
                    return BadRequest("Nom obligatoire");

                if (p.IdNiveau <= 0)
                    return BadRequest("Niveau invalide");

                await _context.Parcours.AddAsync(p);
                await _context.SaveChangesAsync();

                return Ok("Action reussi");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur création : {ex.Message}");
            }
        }

       
        // UPDATE 
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Parcours p)
        {
            try
            {
                var existing = await _context.Parcours.FindAsync(id);

                if (existing == null)
                    return NotFound("Parcours introuvable");

                existing.Nom = p.Nom;
                existing.IdNiveau = p.IdNiveau;

                await _context.SaveChangesAsync();

                return Ok("Action reussi");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur modification : {ex.Message}");
            }
        }
        // DELETE
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var p = await _context.Parcours.FindAsync(id);

                if (p == null)
                    return NotFound("Parcours introuvable");

                _context.Parcours.Remove(p);
                await _context.SaveChangesAsync();

                return Ok("Action reussi");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur suppression : {ex.Message}");
            }
        }
    }
}