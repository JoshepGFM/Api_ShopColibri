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
    public class BitacorasController : ControllerBase
    {
        private readonly ShopColibriContext _context;

        public BitacorasController(ShopColibriContext context)
        {
            _context = context;
        }

        // GET: api/Bitacoras/Consulta?inicio=5%2F1%2F2023&final=6%2F1%2F2023&busqueda=verfrew&Todo=true
        [HttpGet("Consulta")]
        public async Task<IEnumerable<Bitacora>> GetBitacora(DateTime inicio, DateTime final, string? busqueda, bool Todo)
        {
          if (_context.Bitacoras == null)
          {
              return (IEnumerable<Bitacora>)NotFound();
          }
            if (inicio <= final && !Todo) 
            {
                if (busqueda == null)
                {
                    var query = (from b in _context.Bitacoras
                                 where b.Fecha >= inicio && b.Fecha <= final
                                 select new
                                 {
                                     id = b.Id,
                                     fecha = b.Fecha,
                                     descripcion = b.Descripcion
                                 }).ToList();

                    List<Bitacora> list = new List<Bitacora>();
                    foreach (var item in query)
                    {
                        Bitacora NewItem = new Bitacora();

                        NewItem.Id = item.id;
                        NewItem.Fecha = item.fecha;
                        NewItem.Descripcion = item.descripcion;

                        list.Add(NewItem);
                    }

                    if (list == null)
                    {
                        return (IEnumerable<Bitacora>)NotFound();
                    }

                    return list;
                }
                else
                {
                    var query = (from b in _context.Bitacoras
                                 where (b.Fecha >= inicio && b.Fecha <= final) && b.Descripcion.Contains(busqueda)
                                 select new
                                 {
                                     id = b.Id,
                                     fecha = b.Fecha,
                                     descripcion = b.Descripcion
                                 }).ToList();

                    List<Bitacora> list = new List<Bitacora>();
                    foreach (var item in query)
                    {
                        Bitacora NewItem = new Bitacora();

                        NewItem.Id = item.id;
                        NewItem.Fecha = item.fecha;
                        NewItem.Descripcion = item.descripcion;

                        list.Add(NewItem);
                    }

                    if (list == null)
                    {
                        return (IEnumerable<Bitacora>)NotFound();
                    }

                    return list;
                }
            }
            else
            {
                var query = (from b in _context.Bitacoras
                             select new
                             {
                                 id = b.Id,
                                 fecha = b.Fecha,
                                 descripcion = b.Descripcion
                             }).ToList();

                List<Bitacora> list = new List<Bitacora>();
                foreach (var item in query)
                {
                    Bitacora NewItem = new Bitacora();

                    NewItem.Id = item.id;
                    NewItem.Fecha = item.fecha;
                    NewItem.Descripcion = item.descripcion;

                    list.Add(NewItem);
                }

                if (list == null)
                {
                    return (IEnumerable<Bitacora>)NotFound();
                }

                return list;
            }
        }

        // POST: api/Bitacoras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bitacora>> PostBitacora(Bitacora bitacora)
        {
          if (_context.Bitacoras == null)
          {
              return Problem("Entity set 'ShopColibriContext.Bitacoras'  is null.");
          }
            _context.Bitacoras.Add(bitacora);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBitacora", new { id = bitacora.Id }, bitacora);
        }

        private bool BitacoraExists(int id)
        {
            return (_context.Bitacoras?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
