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
          if(_context.ControlMarmita == null)
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

        // GET: api/ControlMarmitums/ListaControl?buscar=10
        [HttpGet("ListaControl")]
        public ActionResult<IEnumerable<ControlMarmitums>> GetControlMarmitumLista(string? buscar)
        {
            if (buscar == null)
            {
                var query = (from c in _context.ControlMarmita
                             select new
                             {
                                 codigo = c.Codigo,
                                 fecha = c.Fecha,
                                 horaEn = c.HoraEn,
                                 horaAp = c.HoraAp,
                                 temperatura = c.Temperatura,
                                 intencidadMov = c.IntensidadMov,
                                 lote = c.Lote,
                                 usuarios = (from u in _context.Usuarios
                                             join uc in _context.UsuarioControlMarmita
                                             on u.IdUsuario equals uc.UsuarioIdUsuario
                                             where uc.ControlMarmitaCodigo == c.Codigo
                                             select new
                                             {
                                                 idUsuario = u.IdUsuario,
                                                 nombre = u.Nombre,
                                                 apellido1 = u.Apellido1,
                                                 apellido2 = u.Apellido2,
                                                 email = u.Email,
                                                 emailRes = u.EmailResp,
                                                 telefono = u.Telefono,
                                                 Tusuario = u.TusuarioId
                                             }).ToList()
                             });
                List<ControlMarmitums> list = new List<ControlMarmitums>();
                foreach (var item in query)
                {
                    ControlMarmitums NewItem = new ControlMarmitums();

                    NewItem.Codigo = item.codigo;
                    NewItem.Fecha = item.fecha;
                    NewItem.HoraEn = item.horaEn;
                    NewItem.HoraAp = item.horaAp;
                    NewItem.Temperatura = item.temperatura;
                    NewItem.IntensidadMov = item.intencidadMov;
                    NewItem.Lote = item.lote;
                    List<Usuario> listUs = new List<Usuario>();
                    foreach (var item2 in item.usuarios)
                    {
                        Usuario NewItemUs = new Usuario();

                        NewItemUs.IdUsuario = item2.idUsuario;
                        NewItemUs.Nombre = item2.nombre;
                        NewItemUs.Apellido1 = item2.apellido1;
                        NewItemUs.Apellido2 = item2.apellido2;
                        NewItemUs.Email = item2.email;
                        NewItemUs.EmailResp = item2.emailRes;
                        NewItemUs.Telefono = item2.telefono;
                        NewItemUs.TusuarioId = item2.Tusuario;

                        listUs.Add(NewItemUs);
                    }
                    if (listUs != null)
                    {
                        NewItem.Usuarios = listUs;
                    }
                    else
                    {
                        NewItem.Usuarios = null;
                    }

                    list.Add(NewItem);
                }

                if (list == null)
                {
                    return null;
                }

                return list;
            }
            else
            {
                var query = (from c in _context.ControlMarmita
                             where c.Codigo.ToString().Contains(buscar) || c.Lote.Contains(buscar)
                             select new
                             {
                                 codigo = c.Codigo,
                                 fecha = c.Fecha,
                                 horaEn = c.HoraEn,
                                 horaAp = c.HoraAp,
                                 temperatura = c.Temperatura,
                                 intencidadMov = c.IntensidadMov,
                                 lote = c.Lote,
                                 usuarios = (from u in _context.Usuarios
                                             join uc in _context.UsuarioControlMarmita
                                             on u.IdUsuario equals uc.UsuarioIdUsuario
                                             where uc.ControlMarmitaCodigo == c.Codigo
                                             select new
                                             {
                                                 idUsuario = u.IdUsuario,
                                                 nombre = u.Nombre,
                                                 apellido1 = u.Apellido1,
                                                 apellido2 = u.Apellido2,
                                                 email = u.Email,
                                                 emailRes = u.EmailResp,
                                                 telefono = u.Telefono,
                                                 Tusuario = u.TusuarioId
                                             }).ToList()
                             });
                List<ControlMarmitums> list = new List<ControlMarmitums>();
                foreach (var item in query)
                {
                    ControlMarmitums NewItem = new ControlMarmitums();

                    NewItem.Codigo = item.codigo;
                    NewItem.Fecha = item.fecha;
                    NewItem.HoraEn = item.horaEn;
                    NewItem.HoraAp = item.horaAp;
                    NewItem.Temperatura = item.temperatura;
                    NewItem.IntensidadMov = item.intencidadMov;
                    NewItem.Lote = item.lote;
                    List<Usuario> listUs = new List<Usuario>();
                    foreach (var item2 in item.usuarios)
                    {
                        Usuario NewItemUs = new Usuario();

                        NewItemUs.IdUsuario = item2.idUsuario;
                        NewItemUs.Nombre = item2.nombre;
                        NewItemUs.Apellido1 = item2.apellido1;
                        NewItemUs.Apellido2 = item2.apellido2;
                        NewItemUs.Email = item2.email;
                        NewItemUs.EmailResp = item2.emailRes;
                        NewItemUs.Telefono = item2.telefono;
                        NewItemUs.TusuarioId = item2.Tusuario;

                        listUs.Add(NewItemUs);
                    }
                    if (listUs != null)
                    {
                        NewItem.Usuarios = listUs;
                    }
                    else
                    {
                        NewItem.Usuarios = null;
                    }

                    list.Add(NewItem);
                }

                if (list == null)
                {
                    return null;
                }

                return list;
            }
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
