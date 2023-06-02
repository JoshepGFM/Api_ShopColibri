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
    public class UsuarioFechaIngresController : ControllerBase
    {
        private readonly ShopColibriContext _context;

        public UsuarioFechaIngresController(ShopColibriContext context)
        {
            _context = context;
        }

        // GET: api/UsuarioFechaIngres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioFechaIngre>>> GetUsuarioFechaIngres()
        {
          if (_context.UsuarioFechaIngres == null)
          {
              return NotFound();
          }
            return await _context.UsuarioFechaIngres.ToListAsync();
        }

        // GET: api/UsuarioFechaIngres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioFechaIngre>> GetUsuarioFechaIngre(int id)
        {
          if (_context.UsuarioFechaIngres == null)
          {
              return NotFound();
          }
            var usuarioFechaIngre = await _context.UsuarioFechaIngres.FindAsync(id);

            if (usuarioFechaIngre == null)
            {
                return NotFound();
            }

            return usuarioFechaIngre;
        }

        // PUT: api/UsuarioFechaIngres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioFechaIngre(int id, UsuarioFechaIngre usuarioFechaIngre)
        {
            if (id != usuarioFechaIngre.DetalleId)
            {
                return BadRequest();
            }

            _context.Entry(usuarioFechaIngre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioFechaIngreExists(id))
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

        // POST: api/UsuarioFechaIngres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioFechaIngre>> PostUsuarioFechaIngre(UsuarioFechaIngre usuarioFechaIngre)
        {
          if (_context.UsuarioFechaIngres == null)
          {
              return Problem("Entity set 'ShopColibriContext.UsuarioFechaIngres'  is null.");
          }
            _context.UsuarioFechaIngres.Add(usuarioFechaIngre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarioFechaIngre", new { id = usuarioFechaIngre.DetalleId }, usuarioFechaIngre);
        }

        // DELETE: api/UsuarioFechaIngres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioFechaIngre(int id)
        {
            if (_context.UsuarioFechaIngres == null)
            {
                return NotFound();
            }
            var query = (from f in _context.FechaIngres
                         where f.EmpaqueId == id
                         select new
                         {
                             id = f.Id,
                             fecha = f.Fecha,
                             entrada = f.Entrada,
                             empaqueId = f.EmpaqueId
                         }).ToList();
            List<FechaIngre> list = new List<FechaIngre>();
            foreach (var item in query)
            {
                FechaIngre NewItem = new FechaIngre();

                NewItem.Id = item.id;
                NewItem.Fecha = item.fecha;
                NewItem.Entrada = item.entrada;
                NewItem.EmpaqueId = item.empaqueId;

                list.Add(NewItem);
            }
            for (int i = 0; i < list.Count; ++i)
            {
                var query2 = (from u in _context.UsuarioFechaIngres
                             where u.FechaIngreId == list[i].Id
                             select new
                             {
                                 detalleId = u.DetalleId,
                                 usuarioId = u.UsuarioIdUsuario,
                                 fechaIngreId = u.FechaIngreId,
                                 fecha = u.Fecha
                             }).ToList();
                List<UsuarioFechaIngre> list2 = new List<UsuarioFechaIngre>();
                foreach (var item in query2)
                {
                    UsuarioFechaIngre NewItem = new UsuarioFechaIngre();

                    NewItem.DetalleId = item.detalleId;
                    NewItem.UsuarioIdUsuario = item.usuarioId;
                    NewItem.FechaIngreId = item.fechaIngreId;
                    NewItem.Fecha = item.fecha;

                    list2.Add(NewItem);
                }
                for (int j = 0; j < list2.Count; ++j)
                {
                    var usuarioFechaIngre = await _context.UsuarioFechaIngres.FindAsync(list2[j].DetalleId);
                    if (usuarioFechaIngre == null)
                    {
                        return NotFound();
                    }

                    _context.UsuarioFechaIngres.Remove(usuarioFechaIngre);
                    await _context.SaveChangesAsync();
                }
            }

            return NoContent();
        }

        private bool UsuarioFechaIngreExists(int id)
        {
            return (_context.UsuarioFechaIngres?.Any(e => e.DetalleId == id)).GetValueOrDefault();
        }
    }
}
