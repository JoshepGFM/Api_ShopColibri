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
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace API_ShopColibri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class InventariosController : ControllerBase
    {
        private readonly ShopColibriContext _context;

        public InventariosController(ShopColibriContext context)
        {
            _context = context;
        }

        // GET: api/Inventarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventario>>> GetInventarios()
        {
          if (_context.Inventarios == null)
          {
              return NotFound();
          }
            return await _context.Inventarios.ToListAsync();
        }

        // GET: api/Inventarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventario>> GetInventario(int id)
        {
          if (_context.Inventarios == null)
          {
              return NotFound();
          }
            var inventario = await _context.Inventarios.FindAsync(id);

            if (inventario == null)
            {
                return NotFound();
            }

            return inventario;
        }

        //GET: api/Inventarios/BusquedaIventario?buscar=mie&estado=false
        [HttpGet("BusquedaIventario")]
        public ActionResult<IEnumerable<Inventarios>> GetUserBuscar(string? buscar, bool estado)
        {

            if (!string.IsNullOrEmpty(buscar))
            {
                if (estado)
                {
                    var query = (from i in _context.Inventarios
                                 join p in _context.Productos on
                                 i.ProductoCodigo equals p.Codigo
                                 join e in _context.Empaques on
                                 i.EmpaqueId equals e.Id
                                 where (p.Nombre + " " + e.Nombre + " " + e.Tamannio).Contains(buscar) && i.Stock > 0
                                 select new
                                 {
                                     id = i.Id,
                                     fecha = i.Fecha,
                                     stock = i.Stock,
                                     precioUn = i.PrecioUn,
                                     origen = i.Origen,
                                     productoCodigo = i.ProductoCodigo,
                                     empaqueId = i.EmpaqueId,
                                     nombrePro = p.Nombre,
                                     nombreEmp = e.Nombre + " " + e.Tamannio,
                                     DescripcionPro = p.Descripcion,
                                     imagenes = (from im in _context.Imagens
                                                 where im.InventarioId == i.Id
                                                 select new
                                                 {
                                                     id = im.Id,
                                                     imagen = im.Imagen1,
                                                     idInven = im.InventarioId
                                                 }).ToList()
                                 }).ToList();

                    List<Inventarios> list = new List<Inventarios>();

                    foreach (var item in query)
                    {
                        Inventarios NewItem = new Inventarios();

                        NewItem.Id = item.id;
                        NewItem.Fecha = item.fecha;
                        NewItem.Stock = item.stock;
                        NewItem.PrecioUn = item.precioUn;
                        NewItem.Origen = item.origen;
                        NewItem.ProductoCodigo = item.productoCodigo;
                        NewItem.EmpaqueId = item.empaqueId;
                        NewItem.NombrePro = item.nombrePro;
                        NewItem.NombreEmp = item.nombreEmp;
                        NewItem.DescripcionPro = item.DescripcionPro;

                        ObservableCollection<Imagen> listIma = new ObservableCollection<Imagen>();
                        foreach (var item2 in item.imagenes)
                        {
                            Imagen imagen = new Imagen();

                            imagen.Id = item2.id;
                            imagen.Imagen1 = item2.imagen;
                            imagen.InventarioId = item2.idInven;

                            listIma.Add(imagen);
                        }
                        if (listIma != null)
                        {
                            NewItem.imagenes = listIma;
                        }
                        else
                        {
                            NewItem.imagenes = null;
                        }

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
                    var query = (from i in _context.Inventarios
                                 join p in _context.Productos on
                                 i.ProductoCodigo equals p.Codigo
                                 join e in _context.Empaques on
                                 i.EmpaqueId equals e.Id
                                 where (p.Nombre + " " + e.Nombre + " " + e.Tamannio).Contains(buscar) && i.Stock <= 0
                                 select new
                                 {
                                     id = i.Id,
                                     fecha = i.Fecha,
                                     stock = i.Stock,
                                     precioUn = i.PrecioUn,
                                     origen = i.Origen,
                                     productoCodigo = i.ProductoCodigo,
                                     empaqueId = i.EmpaqueId,
                                     nombrePro = p.Nombre,
                                     nombreEmp = e.Nombre + " " + e.Tamannio,
                                     DescripcionPro = p.Descripcion,
                                     imagenes = (from im in _context.Imagens
                                                 where im.InventarioId == i.Id
                                                 select new
                                                 {
                                                     id = im.Id,
                                                     imagen = im.Imagen1,
                                                     idInven = im.InventarioId
                                                 }).ToList()
                                 }).ToList();

                    List<Inventarios> list = new List<Inventarios>();

                    foreach (var item in query)
                    {
                        Inventarios NewItem = new Inventarios();

                        NewItem.Id = item.id;
                        NewItem.Fecha = item.fecha;
                        NewItem.Stock = item.stock;
                        NewItem.PrecioUn = item.precioUn;
                        NewItem.Origen = item.origen;
                        NewItem.ProductoCodigo = item.productoCodigo;
                        NewItem.EmpaqueId = item.empaqueId;
                        NewItem.NombrePro = item.nombrePro;
                        NewItem.NombreEmp = item.nombreEmp;
                        NewItem.DescripcionPro = item.DescripcionPro;

                        ObservableCollection<Imagen> listIma = new ObservableCollection<Imagen>();
                        foreach (var item2 in item.imagenes)
                        {
                            Imagen imagen = new Imagen();

                            imagen.Id = item2.id;
                            imagen.Imagen1 = item2.imagen;
                            imagen.InventarioId = item2.idInven;

                            listIma.Add(imagen);
                        }
                        if (listIma != null)
                        {
                            NewItem.imagenes = listIma;
                        }
                        else
                        {
                            NewItem.imagenes = null;
                        }

                        list.Add(NewItem);
                    }

                    if (list == null)
                    {
                        return NotFound();
                    }

                    return list;
                }
            }
            else
            {
                if (estado)
                {
                    var query = (from i in _context.Inventarios
                                 join p in _context.Productos on
                                 i.ProductoCodigo equals p.Codigo
                                 join e in _context.Empaques on
                                 i.EmpaqueId equals e.Id
                                 where i.Stock > 0
                                 select new
                                 {
                                     id = i.Id,
                                     fecha = i.Fecha,
                                     stock = i.Stock,
                                     precioUn = i.PrecioUn,
                                     origen = i.Origen,
                                     productoCodigo = i.ProductoCodigo,
                                     empaqueId = i.EmpaqueId,
                                     nombrePro = p.Nombre,
                                     nombreEmp = e.Nombre + " " + e.Tamannio,
                                     DescripcionPro = p.Descripcion,
                                     imagenes = (from im in _context.Imagens
                                                 where im.InventarioId == i.Id
                                                 select new
                                                 {
                                                     id = im.Id,
                                                     imagen = im.Imagen1,
                                                     idInven = im.InventarioId
                                                 }).ToList()
                                 }).ToList();

                    List<Inventarios> list = new List<Inventarios>();

                    foreach (var item in query)
                    {
                        Inventarios NewItem = new Inventarios();

                        NewItem.Id = item.id;
                        NewItem.Fecha = item.fecha;
                        NewItem.Stock = item.stock;
                        NewItem.PrecioUn = item.precioUn;
                        NewItem.Origen = item.origen;
                        NewItem.ProductoCodigo = item.productoCodigo;
                        NewItem.EmpaqueId = item.empaqueId;
                        NewItem.NombrePro = item.nombrePro;
                        NewItem.NombreEmp = item.nombreEmp;
                        NewItem.DescripcionPro = item.DescripcionPro;

                        ObservableCollection<Imagen> listIma = new ObservableCollection<Imagen>();
                        foreach (var item2 in item.imagenes)
                        {
                            Imagen imagen = new Imagen();

                            imagen.Id = item2.id;
                            imagen.Imagen1 = item2.imagen;
                            imagen.InventarioId = item2.idInven;

                            listIma.Add(imagen);
                        }
                        if (listIma != null)
                        {
                            NewItem.imagenes = listIma;
                        }
                        else
                        {
                            NewItem.imagenes = null;
                        }

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
                    var query = (from i in _context.Inventarios
                                 join p in _context.Productos on
                                 i.ProductoCodigo equals p.Codigo
                                 join e in _context.Empaques on
                                 i.EmpaqueId equals e.Id
                                 where i.Stock <= 0
                                 select new
                                 {
                                     id = i.Id,
                                     fecha = i.Fecha,
                                     stock = i.Stock,
                                     precioUn = i.PrecioUn,
                                     origen = i.Origen,
                                     productoCodigo = i.ProductoCodigo,
                                     empaqueId = i.EmpaqueId,
                                     nombrePro = p.Nombre,
                                     nombreEmp = e.Nombre + " " + e.Tamannio,
                                     DescripcionPro = p.Descripcion,
                                     imagenes = (from im in _context.Imagens
                                                 where im.InventarioId == i.Id
                                                 select new
                                                 {
                                                     id = im.Id,
                                                     imagen = im.Imagen1,
                                                     idInven = im.InventarioId
                                                 }).ToList()
                                 }).ToList();

                    List<Inventarios> list = new List<Inventarios>();

                    foreach (var item in query)
                    {
                        Inventarios NewItem = new Inventarios();

                        NewItem.Id = item.id;
                        NewItem.Fecha = item.fecha;
                        NewItem.Stock = item.stock;
                        NewItem.PrecioUn = item.precioUn;
                        NewItem.Origen = item.origen;
                        NewItem.ProductoCodigo = item.productoCodigo;
                        NewItem.EmpaqueId = item.empaqueId;
                        NewItem.NombrePro = item.nombrePro;
                        NewItem.NombreEmp = item.nombreEmp;
                        NewItem.DescripcionPro = item.DescripcionPro;

                        ObservableCollection<Imagen> listIma = new ObservableCollection<Imagen>();
                        foreach (var item2 in item.imagenes)
                        {
                            Imagen imagen = new Imagen();

                            imagen.Id = item2.id;
                            imagen.Imagen1 = item2.imagen;
                            imagen.InventarioId = item2.idInven;

                            listIma.Add(imagen);
                        }
                        if (listIma != null)
                        {
                            NewItem.imagenes = listIma;
                        }
                        else
                        {
                            NewItem.imagenes = null;
                        }

                        list.Add(NewItem);
                    }

                    if (list == null)
                    {
                        return NotFound();
                    }

                    return list;
                }
            }
        }


        // PUT: api/Inventarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventario(int id, Inventario inventario)
        {
            if (id != inventario.Id)
            {
                return BadRequest();
            }

            _context.Entry(inventario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventarioExists(id))
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

        // POST: api/Inventarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventario>> PostInventario(Inventario inventario)
        {
          if (_context.Inventarios == null)
          {
              return Problem("Entity set 'ShopColibriContext.Inventarios'  is null.");
          }
            _context.Inventarios.Add(inventario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventario", new { id = inventario.Id }, inventario);
        }

        // DELETE: api/Inventarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventario(int id)
        {
            if (_context.Inventarios == null)
            {
                return NotFound();
            }
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }

            _context.Inventarios.Remove(inventario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventarioExists(int id)
        {
            return (_context.Inventarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
