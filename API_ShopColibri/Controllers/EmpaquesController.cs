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
    public class EmpaquesController : ControllerBase
    {
        private readonly ShopColibriContext _context;

        public EmpaquesController(ShopColibriContext context)
        {
            _context = context;
        }

        // GET: api/Empaques
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empaque>>> GetEmpaques()
        {
          if (_context.Empaques == null)
          {
              return NotFound();
          }
            return await _context.Empaques.ToListAsync();
        }

        // GET: api/Empaques/BuscarEmpaque?Buscar=m
        [HttpGet("BuscarEmpaque")]
        public ActionResult<IEnumerable<Empaque>> GetBuscarEmpaque(string? Buscar)
        {
            if (_context.Productos == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(Buscar))
            {
                var query = (from e in _context.Empaques
                             select new
                             {
                                 id = e.Id,
                                 nombre = e.Nombre,
                                 tamannio = e.Tamannio,
                                 stock = e.Stock
                             }).ToList();

                List<Empaque> list = new List<Empaque>();

                foreach (var item in query)
                {
                    Empaque NewItem = new Empaque();

                    NewItem.Id = item.id;
                    NewItem.Nombre = item.nombre;
                    NewItem.Tamannio = item.tamannio;
                    NewItem.Stock = item.stock;

                    list.Add(NewItem);
                }

                if (list == null)
                {
                    return NotFound();
                }

                return list;
            }
            else
            {
                var query = (from e in _context.Empaques
                             where e.Nombre.Contains(Buscar)
                             select new
                             {
                                 id = e.Id,
                                 nombre = e.Nombre,
                                 tamannio = e.Tamannio,
                                 stock = e.Stock
                             }).ToList();

                List<Empaque> list = new List<Empaque>();

                foreach (var item in query)
                {
                    Empaque NewItem = new Empaque();

                    NewItem.Id = item.id;
                    NewItem.Nombre = item.nombre;
                    NewItem.Tamannio = item.tamannio;
                    NewItem.Stock = item.stock;

                    list.Add(NewItem);
                }

                if (list == null)
                {
                    return NotFound();
                }

                return list;
            }
        }

        // GET: api/Empaques/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empaque>> GetEmpaque(int id)
        {
          if (_context.Empaques == null)
          {
              return NotFound();
          }
            var empaque = await _context.Empaques.FindAsync(id);

            if (empaque == null)
            {
                return NotFound();
            }

            return empaque;
        }

        // PUT: api/Empaques/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpaque(int id, Empaque empaque)
        {
            if (id != empaque.Id)
            {
                return BadRequest();
            }

            _context.Entry(empaque).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpaqueExists(id))
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

        // POST: api/Empaques
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Empaque>> PostEmpaque(Empaque empaque)
        {
          if (_context.Empaques == null)
          {
              return Problem("Entity set 'ShopColibriContext.Empaques'  is null.");
          }
            _context.Empaques.Add(empaque);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpaque", new { id = empaque.Id }, empaque);
        }

        // DELETE: api/Empaques/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpaque(int id)
        {
            if (_context.Empaques == null)
            {
                return NotFound();
            }
            var empaque = await _context.Empaques.FindAsync(id);
            if (empaque == null)
            {
                return NotFound();
            }

            _context.Empaques.Remove(empaque);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpaqueExists(int id)
        {
            return (_context.Empaques?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
