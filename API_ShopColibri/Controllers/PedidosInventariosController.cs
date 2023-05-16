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
    public class PedidosInventariosController : ControllerBase
    {
        private readonly ShopColibriContext _context;

        public PedidosInventariosController(ShopColibriContext context)
        {
            _context = context;
        }

        // GET: api/PedidosInventarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidosInventario>>> GetPedidosInventarios()
        {
          if (_context.PedidosInventarios == null)
          {
              return NotFound();
          }
            return await _context.PedidosInventarios.ToListAsync();
        }

        // GET: api/PedidosInventarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidosInventario>> GetPedidosInventario(int id)
        {
          if (_context.PedidosInventarios == null)
          {
              return NotFound();
          }
            var pedidosInventario = await _context.PedidosInventarios.FindAsync(id);

            if (pedidosInventario == null)
            {
                return NotFound();
            }

            return pedidosInventario;
        }

        // GET: api/PedidosInventarios/BuscarPedidos?id=10
        [HttpGet("BuscarPedidos")]
        public ActionResult<IEnumerable<PedidosInventario>> GetPedidosInventarioBuscar(int id)
        {
            if (_context.PedidosInventarios == null)
            {
                return NotFound();
            }
            var query = (from pi in _context.PedidosInventarios
                         where pi.PedidosCodigo == id
                         select new
                         {
                             detalleId = pi.DetalleId,
                             inventarioId = pi.InventarioId,
                             pedidoCodigo = pi.PedidosCodigo,
                             cantidad = pi.Cantidad,
                             precio = pi.Precio,
                             total = pi.Total,
                             fecha = pi.Fecha
                         }).ToList();

            List<PedidosInventario> list = new List<PedidosInventario>();
            foreach (var item in query)
            {
                PedidosInventario NewItem = new PedidosInventario();

                NewItem.DetalleId = item.detalleId;
                NewItem.InventarioId = item.inventarioId;
                NewItem.PedidosCodigo = item.pedidoCodigo;
                NewItem.Cantidad = item.cantidad;
                NewItem.Precio = item.precio;
                NewItem.Total = item.total;
                NewItem.Fecha = item.fecha;

                list.Add(NewItem);
            }

            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        // PUT: api/PedidosInventarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedidosInventario(int id, PedidosInventario pedidosInventario)
        {
            if (id != pedidosInventario.DetalleId)
            {
                return BadRequest();
            }

            _context.Entry(pedidosInventario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidosInventarioExists(id))
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

        // POST: api/PedidosInventarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PedidosInventario>> PostPedidosInventario(PedidosInventario pedidosInventario)
        {
          if (_context.PedidosInventarios == null)
          {
              return Problem("Entity set 'ShopColibriContext.PedidosInventarios'  is null.");
          }
            _context.PedidosInventarios.Add(pedidosInventario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedidosInventario", new { id = pedidosInventario.DetalleId }, pedidosInventario);
        }

        // DELETE: api/PedidosInventarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedidosInventario(int id)
        {
            if (_context.PedidosInventarios == null)
            {
                return NotFound();
            }
            var pedidosInventario = await _context.PedidosInventarios.FindAsync(id);
            if (pedidosInventario == null)
            {
                return NotFound();
            }

            _context.PedidosInventarios.Remove(pedidosInventario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidosInventarioExists(int id)
        {
            return (_context.PedidosInventarios?.Any(e => e.DetalleId == id)).GetValueOrDefault();
        }
    }
}
