using GestionEmploiTemps.API.Data;
using GestionEmploiTemps.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var seances =
                    await _context.Seances

                    .Include(s => s.Parcours)
                    .Include(s => s.Matiere)
                    .Include(s => s.Enseignant)
                    .Include(s => s.Salle)
                    .Include(s => s.Creneau)

                    .ToListAsync();

                return Ok(seances);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    ex.Message
                );
            }
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(
            Seance s)
        {
            try
            {
                //  collision salle
                bool salleOccupee =
                    await _context.Seances.AnyAsync(x =>
                        x.IdSalle == s.IdSalle &&
                        x.IdCreneau == s.IdCreneau
                    );

                if (salleOccupee)
                {
                    return BadRequest(
                        "Salle déjà occupée."
                    );
                }

                // collision enseignant
                bool enseignantOccupe =
                    await _context.Seances.AnyAsync(x =>
                        x.IdEns == s.IdEns &&
                        x.IdCreneau == s.IdCreneau
                    );

                if (enseignantOccupe)
                {
                    return BadRequest(
                        "Enseignant déjà occupé."
                    );
                }

                // collision parcours
                bool parcoursOccupe =
                    await _context.Seances.AnyAsync(x =>
                        x.IdParcours == s.IdParcours &&
                        x.IdCreneau == s.IdCreneau
                    );

                if (parcoursOccupe)
                {
                    return BadRequest(
                        "Parcours déjà occupé."
                    );
                }

                await _context.Seances.AddAsync(s);

                await _context.SaveChangesAsync();

                return Ok(
                    "Séance ajoutée"
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    ex.Message
                );
            }
        }

        //GETBYID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var seance = await _context.Seances
                    .Include(s => s.Parcours)
                    .Include(s => s.Matiere)
                    .Include(s => s.Enseignant)
                    .Include(s => s.Salle)
                    .Include(s => s.Creneau)
                    .FirstOrDefaultAsync(s => s.IdSeance == id);

                if (seance == null)
                    return NotFound("Séance introuvable");

                return Ok(seance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            
            }
        }
        //EDIT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Seance s)
        {
            try
            {
                if (id != s.IdSeance)
                    return BadRequest("ID invalide");

                // collisions 
                bool salleOccupee =
                    await _context.Seances.AnyAsync(x =>
                        x.IdSeance != id &&
                        x.IdSalle == s.IdSalle &&
                        x.IdCreneau == s.IdCreneau
                    );

                if (salleOccupee)
                    return BadRequest("Salle déjà occupée");

                bool enseignantOccupe =
                    await _context.Seances.AnyAsync(x =>
                        x.IdSeance != id &&
                        x.IdEns == s.IdEns &&
                        x.IdCreneau == s.IdCreneau
                    );

                if (enseignantOccupe)
                    return BadRequest("Enseignant déjà occupé");

                bool parcoursOccupe =
                    await _context.Seances.AnyAsync(x =>
                        x.IdSeance != id &&
                        x.IdParcours == s.IdParcours &&
                        x.IdCreneau == s.IdCreneau
                    );

                if (parcoursOccupe)
                    return BadRequest("Parcours déjà occupé");

                _context.Entry(s).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok("Séance modifiée");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            try
            {
                var seance =
                    await _context.Seances
                    .FindAsync(id);

                if (seance == null)
                {
                    return NotFound(
                        "Séance introuvable"
                    );
                }

                _context.Seances.Remove(seance);

                await _context.SaveChangesAsync();

                return Ok(
                    "Supprimée"
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    ex.Message
                );
            }
        }
    }
}