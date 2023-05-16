using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ShopColibri.Models;
using API_ShopColibri.Attributes;

namespace API_ShopColibri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class BitacoraSalidasController : ControllerBase
    {
        private readonly ShopColibriContext _context;

        public BitacoraSalidasController(ShopColibriContext context)
        {
            _context = context;
        }

        // GET: api/BitacoraSalidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BitacoraSalida>>> GetBitacoraSalidas()
        {
          if (_context.BitacoraSalidas == null)
          {
              return NotFound();
          }
            return await _context.BitacoraSalidas.ToListAsync();
        }

        // GET: api/BitacoraSalidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BitacoraSalida>> GetBitacoraSalida(int id)
        {
          if (_context.BitacoraSalidas == null)
          {
              return NotFound();
          }
            var bitacoraSalida = await _context.BitacoraSalidas.FindAsync(id);

            if (bitacoraSalida == null)
            {
                return NotFound();
            }

            return bitacoraSalida;
        }

        // PUT: api/BitacoraSalidas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBitacoraSalida(int id, BitacoraSalida bitacoraSalida)
        {
            if (id != bitacoraSalida.Id)
            {
                return BadRequest();
            }

            _context.Entry(bitacoraSalida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BitacoraSalidaExists(id))
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

        // POST: api/BitacoraSalidas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BitacoraSalida>> PostBitacoraSalida(BitacoraSalida bitacoraSalida)
        {
          if (_context.BitacoraSalidas == null)
          {
              return Problem("Entity set 'ShopColibriContext.BitacoraSalidas'  is null.");
          }
            _context.BitacoraSalidas.Add(bitacoraSalida);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBitacoraSalida", new { id = bitacoraSalida.Id }, bitacoraSalida);
        }

        // DELETE: api/BitacoraSalidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBitacoraSalida(int id)
        {
            if (_context.BitacoraSalidas == null)
            {
                return NotFound();
            }
            var bitacoraSalida = await _context.BitacoraSalidas.FindAsync(id);
            if (bitacoraSalida == null)
            {
                return NotFound();
            }

            _context.BitacoraSalidas.Remove(bitacoraSalida);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BitacoraSalidaExists(int id)
        {
            return (_context.BitacoraSalidas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
