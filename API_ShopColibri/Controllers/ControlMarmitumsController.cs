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
    public class ControlMarmitumsController : ControllerBase
    {
        private readonly ShopColibriContext _context;

        public ControlMarmitumsController(ShopColibriContext context)
        {
            _context = context;
        }

        // GET: api/ControlMarmitums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ControlMarmitum>>> GetControlMarmita()
        {
          if (_context.ControlMarmita == null)
          {
              return NotFound();
          }
            return await _context.ControlMarmita.ToListAsync();
        }

        // GET: api/ControlMarmitums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ControlMarmitum>> GetControlMarmitum(int id)
        {
          if (_context.ControlMarmita == null)
          {
              return NotFound();
          }
            var controlMarmitum = await _context.ControlMarmita.FindAsync(id);

            if (controlMarmitum == null)
            {
                return NotFound();
            }

            return controlMarmitum;
        }

        // PUT: api/ControlMarmitums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutControlMarmitum(int id, ControlMarmitum controlMarmitum)
        {
            if (id != controlMarmitum.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(controlMarmitum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControlMarmitumExists(id))
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

        // POST: api/ControlMarmitums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ControlMarmitum>> PostControlMarmitum(ControlMarmitum controlMarmitum)
        {
          if (_context.ControlMarmita == null)
          {
              return Problem("Entity set 'ShopColibriContext.ControlMarmita'  is null.");
          }
            _context.ControlMarmita.Add(controlMarmitum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetControlMarmitum", new { id = controlMarmitum.Codigo }, controlMarmitum);
        }

        // DELETE: api/ControlMarmitums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteControlMarmitum(int id)
        {
            if (_context.ControlMarmita == null)
            {
                return NotFound();
            }
            var controlMarmitum = await _context.ControlMarmita.FindAsync(id);
            if (controlMarmitum == null)
            {
                return NotFound();
            }

            _context.ControlMarmita.Remove(controlMarmitum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ControlMarmitumExists(int id)
        {
            return (_context.ControlMarmita?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
