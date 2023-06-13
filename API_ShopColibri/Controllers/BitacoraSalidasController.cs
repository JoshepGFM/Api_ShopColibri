using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ShopColibri.Models;
using API_ShopColibri.Attributes;
using API_ShopColibri.Models.DTO;
using System.Globalization;

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

        // GET: api/BitacoraSalidas/Consulta?inicio=6%2F5%2F2023&final=6%2F6%2F2023&producto=%20&Todo=true
        [HttpGet("Consulta")]
        public async Task<IEnumerable<BitacoraSalida>> GetBitacoraSalida(DateTime inicio, DateTime final, string? producto, bool Todo)
        {
            if ( inicio <= final && !Todo)
            {
                if (producto == null)
                {
                    var query = _context.BitacoraSalidas
                    .Where(b => b.Fecha.Date.CompareTo(inicio) >= 0 && b.Fecha.Date.CompareTo(final) <= 0)
                    .Select(b => new
                    {
                        fecha = b.Fecha,
                        objetoRef = b.ObjetoRef,
                        salida = b.Salida,
                        id = b.Id
                    })
                    .ToList();

                    List<BitacoraSalida> list = new List<BitacoraSalida>();

                    foreach (var item in query)
                    {
                        BitacoraSalida NewItem = new BitacoraSalida();

                        NewItem.Fecha = item.fecha;
                        NewItem.ObjetoRef = item.objetoRef;
                        NewItem.Salida = item.salida;
                        NewItem.Id = item.id;

                        list.Add(NewItem);
                    }

                    if (list == null)
                    {
                        return (IEnumerable<BitacoraSalida>)NotFound();
                    }

                    return list;
                }
                else
                {
                    var query = _context.BitacoraSalidas.Where(b => b.Fecha.Date.CompareTo(inicio) >= 0 && b.Fecha.Date.CompareTo(final) <= 0 && b.ObjetoRef.Contains(producto))
                        .Select(b => new
                        {
                                     fecha = b.Fecha,
                                     objetoRef = b.ObjetoRef,
                                     salida = b.Salida,
                                     id = b.Id
                        }).ToList();

                    List<BitacoraSalida> list = new List<BitacoraSalida>();

                    foreach (var item in query)
                    {
                        BitacoraSalida NewItem = new BitacoraSalida();

                        NewItem.Fecha = item.fecha;
                        NewItem.ObjetoRef = item.objetoRef;
                        NewItem.Salida = item.salida;
                        NewItem.Id = item.id;

                        list.Add(NewItem);
                    }

                    if (list == null)
                    {
                        return (IEnumerable<BitacoraSalida>)NotFound();
                    }

                    return list;
                }
            }
            else
            {
                var query = (from b in _context.BitacoraSalidas
                             select new
                             {
                                 fecha = b.Fecha,
                                 objetoRef = b.ObjetoRef,
                                 salida = b.Salida,
                                 id = b.Id
                             }).ToList();

                List<BitacoraSalida> list = new List<BitacoraSalida>();

                foreach (var item in query)
                {
                    BitacoraSalida NewItem = new BitacoraSalida();

                    NewItem.Fecha = item.fecha;
                    NewItem.ObjetoRef = item.objetoRef;
                    NewItem.Salida = item.salida;
                    NewItem.Id = item.id;

                    list.Add(NewItem);
                }

                if (list == null)
                {
                    return (IEnumerable<BitacoraSalida>)NotFound();
                }

                return list;
            }
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

        private bool BitacoraSalidaExists(int id)
        {
            return (_context.BitacoraSalidas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
