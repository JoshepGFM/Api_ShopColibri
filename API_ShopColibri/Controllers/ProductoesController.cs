﻿using System;
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
    public class ProductoesController : ControllerBase
    {
        private readonly ShopColibriContext _context;

        public ProductoesController(ShopColibriContext context)
        {
            _context = context;
        }

        // GET: api/Productoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
          if (_context.Productos == null)
          {
              return NotFound();
          }
            return await _context.Productos.ToListAsync();
        }

        // GET: api/Productoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
          if (_context.Productos == null)
          {
              return NotFound();
          }
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // GET: api/Productoes/BuscarProducto?Buscar=m
        [HttpGet("BuscarProducto")]
        public ActionResult<IEnumerable<Producto>> GetBuscarProducto(string? Buscar)
        {
            if (_context.Productos == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(Buscar))
            {
                var query = (from p in _context.Productos
                             select new
                             {
                                 codigo = p.Codigo,
                                 nombre = p.Nombre,
                                 descripcion = p.Descripcion
                             }).ToList();

                List<Producto> list = new List<Producto>();

                foreach (var item in query)
                {
                    Producto NewItem = new Producto();

                    NewItem.Codigo = item.codigo;
                    NewItem.Nombre = item.nombre;
                    NewItem.Descripcion = item.descripcion;

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
                var query = (from p in _context.Productos
                             where p.Nombre.Contains(Buscar)
                             select new
                             {
                                 codigo = p.Codigo,
                                 nombre = p.Nombre,
                                 descripcion = p.Descripcion
                             }).ToList();

                List<Producto> list = new List<Producto>();

                foreach (var item in query)
                {
                    Producto NewItem = new Producto();

                    NewItem.Codigo = item.codigo;
                    NewItem.Nombre = item.nombre;
                    NewItem.Descripcion = item.descripcion;

                    list.Add(NewItem);
                }

                if (list == null)
                {
                    return NotFound();
                }

                return list;
            }
        }
        // PUT: api/Productoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Productoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
          if (_context.Productos == null)
          {
              return Problem("Entity set 'ShopColibriContext.Productos'  is null.");
          }
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.Codigo }, producto);
        }

        // DELETE: api/Productoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            if (_context.Productos == null)
            {
                return NotFound();
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return (_context.Productos?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
