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
    public class UsuarioControlMarmitumsController : ControllerBase
    {
        private readonly ShopColibriContext _context;

        public UsuarioControlMarmitumsController(ShopColibriContext context)
        {
            _context = context;
        }

        // GET: api/UsuarioControlMarmitums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioControlMarmitum>>> GetUsuarioControlMarmita()
        {
          if (_context.UsuarioControlMarmita == null)
          {
              return NotFound();
          }
            return await _context.UsuarioControlMarmita.ToListAsync();
        }

        // GET: api/UsuarioControlMarmitums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioControlMarmitum>> GetUsuarioControlMarmitum(int id)
        {
          if (_context.UsuarioControlMarmita == null)
          {
              return NotFound();
          }
            var usuarioControlMarmitum = await _context.UsuarioControlMarmita.FindAsync(id);

            if (usuarioControlMarmitum == null)
            {
                return NotFound();
            }

            return usuarioControlMarmitum;
        }

        // GET: api/UsuarioControlMarmitums/BuscaControl?id=11
        [HttpGet("BuscaControl")]
        public async Task<ActionResult<UsuarioControlMarmitum>> GetUsuarioControlMarmitumBusca(int id)
        {
            if (_context.UsuarioControlMarmita == null)
            {
                return NotFound();
            }
            var query = (from uc in _context.UsuarioControlMarmita
                                          where uc.ControlMarmitaCodigo == id
                                          select new
                                          {
                                              detalleId = uc.DetalleId,
                                              usuarioid = uc.UsuarioIdUsuario,
                                              controlCodigo = uc.ControlMarmitaCodigo,
                                              fecha = uc.Fecha
                                          }).ToList();
            UsuarioControlMarmitum control = new UsuarioControlMarmitum();
            foreach (var item in query)
            {

                control.DetalleId = item.detalleId;
                control.UsuarioIdUsuario = item.usuarioid;
                control.ControlMarmitaCodigo = item.controlCodigo;
                control.Fecha = item.fecha;
            }

            if (control == null)
            {
                return NotFound();
            }

            return control;
        }

        // PUT: api/UsuarioControlMarmitums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioControlMarmitum(int id, UsuarioControlMarmitum usuarioControlMarmitum)
        {
            if (id != usuarioControlMarmitum.DetalleId)
            {
                return BadRequest();
            }

            _context.Entry(usuarioControlMarmitum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioControlMarmitumExists(id))
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

        // POST: api/UsuarioControlMarmitums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioControlMarmitum>> PostUsuarioControlMarmitum(UsuarioControlMarmitum usuarioControlMarmitum)
        {
          if (_context.UsuarioControlMarmita == null)
          {
              return Problem("Entity set 'ShopColibriContext.UsuarioControlMarmita'  is null.");
          }
            _context.UsuarioControlMarmita.Add(usuarioControlMarmitum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarioControlMarmitum", new { id = usuarioControlMarmitum.DetalleId }, usuarioControlMarmitum);
        }

        // DELETE: api/UsuarioControlMarmitums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioControlMarmitum(int id)
        {
            if (_context.UsuarioControlMarmita == null)
            {
                return NotFound();
            }
            var usuarioControlMarmitum = await _context.UsuarioControlMarmita.FindAsync(id);
            if (usuarioControlMarmitum == null)
            {
                return NotFound();
            }

            _context.UsuarioControlMarmita.Remove(usuarioControlMarmitum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioControlMarmitumExists(int id)
        {
            return (_context.UsuarioControlMarmita?.Any(e => e.DetalleId == id)).GetValueOrDefault();
        }
    }
}
