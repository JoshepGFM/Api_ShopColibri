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
    public class TusuariosController : ControllerBase
    {
        private readonly ShopColibriContext _context;

        public TusuariosController(ShopColibriContext context)
        {
            _context = context;
        }

        // GET: api/Tusuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tusuario>>> GetTusuarios()
        {
          if (_context.Tusuarios == null)
          {
              return NotFound();
          }
            return await _context.Tusuarios.ToListAsync();
        }

        // GET: api/Tusuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tusuario>> GetTusuario(int id)
        {
          if (_context.Tusuarios == null)
          {
              return NotFound();
          }
            var tusuario = await _context.Tusuarios.FindAsync(id);

            if (tusuario == null)
            {
                return NotFound();
            }

            return tusuario;
        }

        // PUT: api/Tusuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTusuario(int id, Tusuario tusuario)
        {
            if (id != tusuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(tusuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TusuarioExists(id))
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

        // POST: api/Tusuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tusuario>> PostTusuario(Tusuario tusuario)
        {
          if (_context.Tusuarios == null)
          {
              return Problem("Entity set 'ShopColibriContext.Tusuarios'  is null.");
          }
            _context.Tusuarios.Add(tusuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTusuario", new { id = tusuario.Id }, tusuario);
        }

        // DELETE: api/Tusuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTusuario(int id)
        {
            if (_context.Tusuarios == null)
            {
                return NotFound();
            }
            var tusuario = await _context.Tusuarios.FindAsync(id);
            if (tusuario == null)
            {
                return NotFound();
            }

            _context.Tusuarios.Remove(tusuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TusuarioExists(int id)
        {
            return (_context.Tusuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
