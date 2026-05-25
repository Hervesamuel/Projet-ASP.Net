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
	public class ParcoursController : ControllerBase
	{
		private readonly AppBdContext _context;

		public ParcoursController(AppBdContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Parcours>>> Get()
		{
			try
			{
				return Ok(await _context.Parcours
					.Include(p => p.Niveau)
					.Include(p => p.Seances)
					.ToListAsync());
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create(Parcours p)
		{
			try
			{
				await _context.Parcours.AddAsync(p);
				await _context.SaveChangesAsync();
				return Ok("Parcours ajouté");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, Parcours p)
		{
			try
			{
				if (id != p.IdParcours) return BadRequest();

				_context.Entry(p).State = EntityState.Modified;
				await _context.SaveChangesAsync();

				return Ok("Modifié");
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
				var p = await _context.Parcours.FindAsync(id);
				if (p == null) return NotFound();

				_context.Parcours.Remove(p);
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