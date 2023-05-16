using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ShopColibri.Models;
using API_ShopColibri.Models.DTO;
using API_ShopColibri.Attributes;

namespace API_ShopColibri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class RegistroesController : ControllerBase
    {
        private readonly ShopColibriContext _context;

        public RegistroesController(ShopColibriContext context)
        {
            _context = context;
        }

        // GET: api/Registroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registro>>> GetRegistros()
        {
          if (_context.Registros == null)
          {
              return NotFound();
          }
            return await _context.Registros.ToListAsync();
        }

        // GET: api/Registroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Registro>> GetRegistro(int id)
        {
          if (_context.Registros == null)
          {
              return NotFound();
          }
            var registro = await _context.Registros.FindAsync(id);

            if (registro == null)
            {
                return NotFound();
            }

            return registro;
        }

        // GET: api/Registroes/5
        [HttpGet("BusquedaReg")]
        public ActionResult<IEnumerable<Registros>> GetRegistroBusca(string? Filter)
        {
            if (!string.IsNullOrWhiteSpace(Filter))
            {
                var query = (from r in _context.Registros
                             join u in _context.Usuarios
                             on r.UsuarioIdUsuario equals u.IdUsuario
                             where (u.Nombre + " " + u.Apellido1 + " " + u.Apellido2).Contains(Filter) || (r.Fecha.ToString()).Contains(Filter)
                             select new
                             {
                                 id = r.Id,
                                 fecha = r.Fecha,
                                 hl = r.HorasL,
                                 hx = r.HorasX,
                                 hm = r.HorasM,
                                 hj = r.HorasJ,
                                 hv = r.HorasV,
                                 hs = r.HorasS,
                                 th = r.TotalHoras,
                                 ch = r.CostoHora,
                                 total = r.Total,
                                 idusuario = r.UsuarioIdUsuario,
                                 nombre = u.Nombre + " " + u.Apellido1 + " " + u.Apellido2
                             }).ToList();
                List<Registros> list = new List<Registros>();

                foreach (var item in query)
                {
                    Registros NewItem = new Registros();

                    NewItem.Id = item.id;
                    NewItem.Fecha = item.fecha;
                    NewItem.HorasL = item.hl;
                    NewItem.HorasX = item.hx;
                    NewItem.HorasM = item.hm;
                    NewItem.HorasJ = item.hj;
                    NewItem.HorasV = item.hv;
                    NewItem.HorasS = item.hs;
                    NewItem.TotalHoras = item.th;
                    NewItem.CostoHora = item.ch;
                    NewItem.Total = item.total;
                    NewItem.UsuarioIdUsuario = item.idusuario;
                    NewItem.UsuarioName = item.nombre;

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
                var query = (from r in _context.Registros
                             join u in _context.Usuarios
                             on r.UsuarioIdUsuario equals u.IdUsuario
                             select new
                             {
                                 id = r.Id,
                                 fecha = r.Fecha,
                                 hl = r.HorasL,
                                 hx = r.HorasX,
                                 hm = r.HorasM,
                                 hj = r.HorasJ,
                                 hv = r.HorasV,
                                 hs = r.HorasS,
                                 th = r.TotalHoras,
                                 ch = r.CostoHora,
                                 total = r.Total,
                                 idusuario = r.UsuarioIdUsuario,
                                 nombre = u.Nombre + " " + u.Apellido1 + " " + u.Apellido2
                             }).ToList();
                List<Registros> list = new List<Registros>();

                foreach (var item in query)
                {
                    Registros NewItem = new Registros();

                    NewItem.Id = item.id;
                    NewItem.Fecha = item.fecha;
                    NewItem.HorasL = item.hl;
                    NewItem.HorasX = item.hx;
                    NewItem.HorasM = item.hm;
                    NewItem.HorasJ = item.hj;
                    NewItem.HorasV = item.hv;
                    NewItem.HorasS = item.hs;
                    NewItem.TotalHoras = item.th;
                    NewItem.CostoHora = item.ch;
                    NewItem.Total = item.total;
                    NewItem.UsuarioIdUsuario = item.idusuario;
                    NewItem.UsuarioName = item.nombre;

                    list.Add(NewItem);
                }
                if (list == null)
                {
                    return NotFound();
                }
                return list;
            }
        }

        // PUT: api/Registroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistro(int id, Registro registro)
        {
            if (id != registro.Id)
            {
                return BadRequest();
            }

            _context.Entry(registro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroExists(id))
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

        // POST: api/Registroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Registro>> PostRegistro(Registro registro)
        {
          if (_context.Registros == null)
          {
              return Problem("Entity set 'ShopColibriContext.Registros'  is null.");
          }
            _context.Registros.Add(registro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistro", new { id = registro.Id }, registro);
        }

        // DELETE: api/Registroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistro(int id)
        {
            if (_context.Registros == null)
            {
                return NotFound();
            }
            var registro = await _context.Registros.FindAsync(id);
            if (registro == null)
            {
                return NotFound();
            }

            _context.Registros.Remove(registro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistroExists(int id)
        {
            return (_context.Registros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
