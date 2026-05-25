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
	public class CreneauController : ControllerBase
	{
		private readonly AppBdContext _context;

		// Injection du DbContext
		public CreneauController(AppBdContext context)
		{
			_context = context;
		}

		// GET : api/Creneau
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Creneau>>> GetCreneaux()
		{
			try
			{
				var creneaux = await _context.Creneaux
					.Include(c => c.Seances)
					.ToListAsync();

				return Ok(creneaux);
			}
			catch (Exception ex)
			{
				return StatusCode(500,
					$"Erreur lors de la récupération des créneaux : {ex.Message}");
			}
		}

		// GET : api/Creneau/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Creneau>> GetCreneau(int id)
		{
			try
			{
				var creneau = await _context.Creneaux
					.Include(c => c.Seances)
					.FirstOrDefaultAsync(c => c.IdCreneau == id);

				if (creneau == null)
				{
					return NotFound("Créneau introuvable.");
				}

				return Ok(creneau);
			}
			catch (Exception ex)
			{
				return StatusCode(500,
					$"Erreur lors de la récupération : {ex.Message}");
			}
		}

		// POST : api/Creneau
		[HttpPost]
		public async Task<ActionResult<Creneau>> CreateCreneau(Creneau creneau)
		{
			try
			{
				// Vérifie les données reçues
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				// Vérifie l'heure
				if (creneau.HeureFin <= creneau.HeureDebut)
				{
					return BadRequest(
						"L'heure de fin doit être supérieure à l'heure de début.");
				}

				await _context.Creneaux.AddAsync(creneau);
				await _context.SaveChangesAsync();

				return CreatedAtAction(
					nameof(GetCreneau),
					new { id = creneau.IdCreneau },
					creneau
				);
			}
			catch (Exception ex)
			{
				return StatusCode(500,
					$"Erreur lors de l'ajout : {ex.Message}");
			}
		}

		// PUT : api/Creneau/5
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCreneau(int id, Creneau creneau)
		{
			try
			{
				if (id != creneau.IdCreneau)
				{
					return BadRequest("ID non valide.");
				}

				if (creneau.HeureFin <= creneau.HeureDebut)
				{
					return BadRequest(
						"L'heure de fin doit être supérieure à l'heure de début.");
				}

				var existe = await _context.Creneaux
					.AnyAsync(c => c.IdCreneau == id);

				if (!existe)
				{
					return NotFound("Créneau introuvable.");
				}

				_context.Entry(creneau).State = EntityState.Modified;

				await _context.SaveChangesAsync();

				return Ok("Créneau modifié avec succès.");
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

		// DELETE : api/Creneau/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCreneau(int id)
		{
			try
			{
				var creneau = await _context.Creneaux
					.FirstOrDefaultAsync(c => c.IdCreneau == id);

				if (creneau == null)
				{
					return NotFound("Créneau introuvable.");
				}

				_context.Creneaux.Remove(creneau);

				await _context.SaveChangesAsync();

				return Ok("Créneau supprimé avec succès.");
			}
			catch (Exception ex)
			{
				return StatusCode(500,
					$"Erreur lors de la suppression : {ex.Message}");
			}
		}
	}
}