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

namespace API_ShopColibri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class FechaIngresController : ControllerBase
    {
        private readonly ShopColibriContext _context;

        public FechaIngresController(ShopColibriContext context)
        {
            _context = context;
        }

        // GET: api/FechaIngres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FechaIngre>>> GetFechaIngres()
        {
          if (_context.FechaIngres == null)
          {
              return NotFound();
          }
            return await _context.FechaIngres.ToListAsync();
        }

        // GET: api/FechaIngres/Consulta?inicio=32&final=43&seleccion=true&todo=true
        [HttpGet("Consulta")]
        public async Task<IEnumerable<FechaIngres>> GetFechaIngre(DateTime inicio, DateTime final, bool? seleccion, bool todo)
        {
            if (_context.FechaIngres == null)
            {
                return (IEnumerable<FechaIngres>)NotFound();
            }
            if (inicio <= final && !todo)
            {
                if(seleccion == null) 
                {
                    var query1 = (from f in _context.FechaIngres
                                 join e in _context.Empaques
                                 on f.EmpaqueId equals e.Id
                                 join i in _context.Inventarios
                                 on f.InventarioId equals i.Id
                                 where f.Fecha.Date.CompareTo(inicio) >= 0 && f.Fecha.Date.CompareTo(final) <= 0
                                 select new
                                 {
                                     id = f.Id,
                                     fecha = f.Fecha,
                                     entrada = f.Entrada,
                                     empaqueId = f.EmpaqueId,
                                     inventarioId = f.InventarioId,
                                     empaqueNombre = e.Nombre + " " + e.Tamannio,
                                     inventarioNombre = ""
                                 }).ToList();
                    var query2 = (from f in _context.FechaIngres
                                  join i in _context.Inventarios
                                  on f.InventarioId equals i.Id
                                  join p in _context.Productos
                                  on i.ProductoCodigo equals p.Codigo
                                  join ei in _context.Empaques
                                  on i.EmpaqueId equals ei.Id
                                  where f.Fecha.Date.CompareTo(inicio) >= 0 && f.Fecha.Date.CompareTo(final) <= 0
                                  select new
                                  {
                                      id = f.Id,
                                      fecha = f.Fecha,
                                      entrada = f.Entrada,
                                      empaqueId = f.EmpaqueId,
                                      inventarioId = f.InventarioId,
                                      empaqueNombre = "",
                                      inventarioNombre = p.Nombre + " " + ei.Nombre + " " + ei.Tamannio
                                  }).ToList();
                    List<FechaIngres> fechaIngre = new List<FechaIngres>();
                    foreach (var item in query1)
                    {
                        FechaIngres NewItem = new FechaIngres();

                        NewItem.Id = item.id;
                        NewItem.Fecha = item.fecha;
                        NewItem.Entrada = item.entrada;
                        NewItem.EmpaqueId = item.empaqueId;
                        NewItem.InventarioId = item.inventarioId;
                        NewItem.EmpaqueNombre = item.empaqueNombre;
                        NewItem.InventarioNombre = item.inventarioNombre;

                        fechaIngre.Add(NewItem);
                    }
                    foreach (var item in query2)
                    {
                        FechaIngres NewItem = new FechaIngres();

                        NewItem.Id = item.id;
                        NewItem.Fecha = item.fecha;
                        NewItem.Entrada = item.entrada;
                        NewItem.EmpaqueId = item.empaqueId;
                        NewItem.InventarioId = item.inventarioId;
                        NewItem.EmpaqueNombre = item.empaqueNombre;
                        NewItem.InventarioNombre = item.inventarioNombre;

                        fechaIngre.Add(NewItem);
                    }
                    if (fechaIngre == null && seleccion == false)
                    {
                        return (IEnumerable<FechaIngres>)NotFound();
                    }

                    return fechaIngre;
                }
                else if (seleccion == true)
                {
                    var query = (from f in _context.FechaIngres
                                 join e in _context.Empaques
                                 on f.EmpaqueId equals e.Id
                                 where f.Fecha.Date.CompareTo(inicio) >= 0 && f.Fecha.Date.CompareTo(final) <= 0 && f.EmpaqueId > 0
                                 select new
                                 {
                                     id = f.Id,
                                     fecha = f.Fecha,
                                     entrada = f.Entrada,
                                     empaqueId = f.EmpaqueId,
                                     inventarioId = f.InventarioId,
                                     empaqueNombre = e.Nombre + " " + e.Tamannio,
                                     inventarioNombre = ""
                                 }).ToList();
                    List<FechaIngres> fechaIngre = new List<FechaIngres>();
                    foreach (var item in query)
                    {
                        FechaIngres NewItem = new FechaIngres();

                        NewItem.Id = item.id;
                        NewItem.Fecha = item.fecha;
                        NewItem.Entrada = item.entrada;
                        NewItem.EmpaqueId = item.empaqueId;
                        NewItem.InventarioId = item.inventarioId;
                        NewItem.EmpaqueNombre = item.empaqueNombre;
                        NewItem.InventarioNombre = item.inventarioNombre;

                        fechaIngre.Add(NewItem);
                    }

                    if (fechaIngre == null && seleccion == false)
                    {
                        return (IEnumerable<FechaIngres>)NotFound();
                    }

                    return fechaIngre;
                }
                else
                {
                    var query = (from f in _context.FechaIngres
                                 join i in _context.Inventarios
                                 on f.InventarioId equals i.Id
                                 join p in _context.Productos
                                 on i.ProductoCodigo equals p.Codigo
                                 join e in _context.Empaques
                                 on i.EmpaqueId equals e.Id
                                 where f.Fecha.Date.CompareTo(inicio) >= 0 && f.Fecha.Date.CompareTo(final) <= 0 && f.InventarioId > 0
                                 select new
                                 {
                                     id = f.Id,
                                     fecha = f.Fecha,
                                     entrada = f.Entrada,
                                     empaqueId = f.EmpaqueId,
                                     inventarioId = f.InventarioId,
                                     empaqueNombre = "",
                                     inventarioNombre = p.Nombre + " " + e.Nombre + " " + e.Tamannio
                                 }).ToList();
                    List<FechaIngres> fechaIngre = new List<FechaIngres>();
                    foreach (var item in query)
                    {
                        FechaIngres NewItem = new FechaIngres();

                        NewItem.Id = item.id;
                        NewItem.Fecha = item.fecha;
                        NewItem.Entrada = item.entrada;
                        NewItem.EmpaqueId = item.empaqueId;
                        NewItem.InventarioId = item.inventarioId;
                        NewItem.EmpaqueNombre = item.empaqueNombre;
                        NewItem.InventarioNombre = item.inventarioNombre;

                        fechaIngre.Add(NewItem);
                    }

                    if (fechaIngre == null)
                    {
                        return (IEnumerable<FechaIngres>)NotFound();
                    }

                    return fechaIngre;
                }
            }
            else 
            {
                var query1 = (from f in _context.FechaIngres
                             join e in _context.Empaques
                             on f.EmpaqueId equals e.Id
                             select new
                             {
                                 id = f.Id,
                                 fecha = f.Fecha,
                                 entrada = f.Entrada,
                                 empaqueId = f.EmpaqueId,
                                 inventarioId = f.InventarioId,
                                 empaqueNombre = e.Nombre + " " + e.Tamannio,
                                 inventarioNombre = ""
                             }).ToList();
                var query2 = (from f in _context.FechaIngres
                             join i in _context.Inventarios
                             on f.InventarioId equals i.Id
                             join p in _context.Productos
                             on i.ProductoCodigo equals p.Codigo
                             join ei in _context.Empaques
                             on i.EmpaqueId equals ei.Id
                             select new
                             {
                                 id = f.Id,
                                 fecha = f.Fecha,
                                 entrada = f.Entrada,
                                 empaqueId = f.EmpaqueId,
                                 inventarioId = f.InventarioId,
                                 empaqueNombre = "",
                                 inventarioNombre = p.Nombre + " " + ei.Nombre + " " + ei.Tamannio
                             }).ToList();
                List<FechaIngres> fechaIngre = new List<FechaIngres>();
                foreach (var item in query1)
                {
                    FechaIngres NewItem = new FechaIngres();

                    NewItem.Id = item.id;
                    NewItem.Fecha = item.fecha;
                    NewItem.Entrada = item.entrada;
                    NewItem.EmpaqueId = item.empaqueId;
                    NewItem.InventarioId = item.inventarioId;
                    NewItem.EmpaqueNombre = item.empaqueNombre;
                    NewItem.InventarioNombre = item.inventarioNombre;

                    fechaIngre.Add(NewItem);
                }
                foreach (var item in query2)
                {
                    FechaIngres NewItem = new FechaIngres();

                    NewItem.Id = item.id;
                    NewItem.Fecha = item.fecha;
                    NewItem.Entrada = item.entrada;
                    NewItem.EmpaqueId = item.empaqueId;
                    NewItem.InventarioId = item.inventarioId;
                    NewItem.EmpaqueNombre = item.empaqueNombre;
                    NewItem.InventarioNombre = item.inventarioNombre;

                    fechaIngre.Add(NewItem);
                }

                if (fechaIngre == null)
                {
                    return (IEnumerable<FechaIngres>)NotFound();
                }

                return fechaIngre;
            }
        }

        // GET: api/FechaIngres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FechaIngre>> GetFechaIngre(int id)
        {
          if (_context.FechaIngres == null)
          {
              return NotFound();
          }
            var fechaIngre = await _context.FechaIngres.FindAsync(id);

            if (fechaIngre == null)
            {
                return NotFound();
            }

            return fechaIngre;
        }

        // PUT: api/FechaIngres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFechaIngre(int id, FechaIngre fechaIngre)
        {
            if (id != fechaIngre.Id)
            {
                return BadRequest();
            }

            _context.Entry(fechaIngre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FechaIngreExists(id))
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

        // POST: api/FechaIngres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FechaIngre>> PostFechaIngre(FechaIngre fechaIngre)
        {
          if (_context.FechaIngres == null)
          {
              return Problem("Entity set 'ShopColibriContext.FechaIngres'  is null.");
          }
            _context.FechaIngres.Add(fechaIngre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFechaIngre", new { id = fechaIngre.Id }, fechaIngre);
        }

        // DELETE: api/FechaIngres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFechaIngre(int id)
        {
            if (_context.FechaIngres == null)
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
            for(int i = 0; i < list.Count; ++i)
            {
                var fechaIngre = await _context.FechaIngres.FindAsync(list[i].Id);
                if (fechaIngre == null)
                {
                    return NotFound();
                }

                _context.FechaIngres.Remove(fechaIngre);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool FechaIngreExists(int id)
        {
            return (_context.FechaIngres?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
