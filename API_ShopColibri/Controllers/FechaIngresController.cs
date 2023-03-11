using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ShopColibri.Models;

namespace API_ShopColibri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FechaIngresController : ControllerBase
    {
        private readonly ShopColibriContext _context;

        public FechaIngresController(ShopColibriContext context)
        {
            _context = context;
        }

        // GET: api/FechaIngres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FechaIngre>>> GetFechaIngres()
        {
          if (_context.FechaIngres == null)
          {
              return NotFound();
          }
            return await _context.FechaIngres.ToListAsync();
        }

        // GET: api/FechaIngres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FechaIngre>> GetFechaIngre(int id)
        {
          if (_context.FechaIngres == null)
          {
              return NotFound();
          }
            var fechaIngre = await _context.FechaIngres.FindAsync(id);

            if (fechaIngre == null)
            {
                return NotFound();
            }

            return fechaIngre;
        }

        // PUT: api/FechaIngres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFechaIngre(int id, FechaIngre fechaIngre)
        {
            if (id != fechaIngre.Id)
            {
                return BadRequest();
            }

            _context.Entry(fechaIngre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FechaIngreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FechaIngres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FechaIngre>> PostFechaIngre(FechaIngre fechaIngre)
        {
          if (_context.FechaIngres == null)
          {
              return Problem("Entity set 'ShopColibriContext.FechaIngres'  is null.");
          }
            _context.FechaIngres.Add(fechaIngre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFechaIngre", new { id = fechaIngre.Id }, fechaIngre);
        }

        // DELETE: api/FechaIngres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFechaIngre(int id)
        {
            if (_context.FechaIngres == null)
            {
                return NotFound();
            }
            var fechaIngre = await _context.FechaIngres.FindAsync(id);
            if (fechaIngre == null)
            {
                return NotFound();
            }

            _context.FechaIngres.Remove(fechaIngre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FechaIngreExists(int id)
        {
            return (_context.FechaIngres?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
