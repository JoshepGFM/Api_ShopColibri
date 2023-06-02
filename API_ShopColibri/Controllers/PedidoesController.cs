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
using System.Collections.ObjectModel;

namespace API_ShopColibri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class PedidoesController : ControllerBase
    {
        private readonly ShopColibriContext _context;

        public PedidoesController(ShopColibriContext context)
        {
            _context = context;
        }

        // GET: api/Pedidoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
          if (_context.Pedidos == null)
          {
              return NotFound();
          }
            return await _context.Pedidos.ToListAsync();
        }

        // GET: api/Pedidoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
          if (_context.Pedidos == null)
          {
              return NotFound();
          }
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        //GET: api/Pedidoes/GetPedidoBuscar?Filtro=dw
        [HttpGet("GetPedidoBuscar")]
        public ActionResult<IEnumerable<Pedidos>> GetUserBuscar(string? Filtro)
        {
            if (string.IsNullOrEmpty(Filtro))
            {
                var query = (from p in _context.Pedidos
                             join u in _context.Usuarios
                             on p.UsuarioIdUsuario equals u.IdUsuario
                             select new
                             {
                                 codigo = p.Codigo,
                                 fecha = p.Fecha,
                                 fechaEn = p.FechaEn,
                                 total = p.Total,
                                 usuarioId = p.UsuarioIdUsuario,
                                 usuario = (from us in _context.Usuarios
                                            where p.UsuarioIdUsuario == us.IdUsuario
                                            select new
                                            {
                                                id = u.IdUsuario,
                                                nombre = u.Nombre,
                                                apellido1 = u.Apellido1,
                                                apellido2 = u.Apellido2,
                                                email = u.Email,
                                                emailRes = u.EmailResp,
                                                telefono = u.Telefono,
                                                Tusuario = u.TusuarioId
                                            }).ToList(),
                                 inventarios = (from i in _context.Inventarios
                                                join e in _context.Empaques
                                                on i.EmpaqueId equals e.Id
                                                join pr in _context.Productos
                                                on i.ProductoCodigo equals pr.Codigo
                                                join pi in _context.PedidosInventarios
                                                on i.Id equals pi.InventarioId
                                                where pi.PedidosCodigo == p.Codigo
                                                select new
                                                {
                                                    id = i.Id,
                                                    fecha = i.Fecha,
                                                    stock = i.Stock,
                                                    precioUn = i.PrecioUn,
                                                    origen = i.Origen,
                                                    productoCodi = i.ProductoCodigo,
                                                    empaqueId = i.EmpaqueId,
                                                    nombreEmpa = e.Nombre + " " + e.Tamannio,
                                                    nombrePro = pr.Nombre,
                                                    cantidad = pi.Cantidad,
                                                    precio = pi.Precio,
                                                    total = pi.Total,
                                                    priImagen = (from im in _context.Imagens
                                                                 where im.InventarioId == i.Id
                                                                 select new
                                                                 {
                                                                     id = im.Id,
                                                                     imagen = im.Imagen1,
                                                                     idInven = im.InventarioId
                                                                 }).ToList()
                                                }).ToList()
                             }).ToList();

                List<Pedidos> list = new List<Pedidos>();

                foreach (var item in query)
                {
                    Pedidos NewItem = new Pedidos();

                    NewItem.Codigo = item.codigo;
                    NewItem.Fecha = item.fecha;
                    NewItem.FechaEn = item.fechaEn;
                    NewItem.Total = item.total;
                    NewItem.UsuarioIdUsuario = item.usuarioId;
                    Usuario usuario = new Usuario();
                    foreach (var item1 in item.usuario)
                    {
                        usuario.IdUsuario = item1.id;
                        usuario.Nombre = item1.nombre;
                        usuario.Apellido1 = item1.apellido1;
                        usuario.Apellido2 = item1.apellido2;
                        usuario.Email = item1.email;
                        usuario.EmailResp = item1.emailRes;
                        usuario.Telefono = item1.telefono;
                        usuario.TusuarioId = item1.Tusuario;
                    }
                    NewItem.Usuario = usuario;
                    List<PedidosCalcu> inventario = new List<PedidosCalcu>(); //esta clase de PedidosCalcu se creo para trae los inventarios y sus cantidades y sumas respectivas que se encuentra en la tabla que los relaciona
                    foreach (var item2 in item.inventarios)
                    {
                        PedidosCalcu NewIven = new PedidosCalcu();

                        NewIven.Id = item2.id;
                        NewIven.Fecha = item2.fecha;
                        NewIven.Stock = item2.stock;
                        NewIven.PrecioUn = item2.precioUn;
                        NewIven.Origen = item2.origen;
                        NewIven.ProductoCodigo = item2.productoCodi;
                        NewIven.EmpaqueId = item2.empaqueId;
                        NewIven.NombreEmp = item2.nombreEmpa;
                        NewIven.NombrePro = item2.nombrePro;
                        NewIven.Cantidad = item2.cantidad;
                        NewIven.Precio = item2.precio;
                        NewIven.Total = item2.total;

                        ObservableCollection<Imagen> listIma = new ObservableCollection<Imagen>();
                        foreach (var item3 in item2.priImagen)
                        {
                            Imagen imagen = new Imagen();

                            imagen.Id = item3.id;
                            imagen.Imagen1 = item3.imagen;
                            imagen.InventarioId = item3.idInven;

                            listIma.Add(imagen);
                        }
                        if (listIma.Count > 0)
                        {
                            NewIven.priImagen = listIma[0].Imagen1;
                        }

                        inventario.Add(NewIven);
                    }
                    NewItem.inventarios = inventario;

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
                var query = (from p in _context.Pedidos
                             join u in _context.Usuarios
                             on p.UsuarioIdUsuario equals u.IdUsuario
                             where p.Codigo.ToString().Contains(Filtro) || (u.Nombre + " " + u.Apellido1 + " " + u.Apellido2).Contains(Filtro)
                             select new
                             {
                                 codigo = p.Codigo,
                                 fecha = p.Fecha,
                                 fechaEn = p.FechaEn,
                                 total = p.Total,
                                 usuarioId = p.UsuarioIdUsuario,
                                 usuario = (from us in _context.Usuarios
                                            where p.UsuarioIdUsuario == us.IdUsuario
                                            select new
                                            {
                                                id = u.IdUsuario,
                                                nombre = u.Nombre,
                                                apellido1 = u.Apellido1,
                                                apellido2 = u.Apellido2,
                                                email = u.Email,
                                                emailRes = u.EmailResp,
                                                telefono = u.Telefono,
                                                Tusuario = u.TusuarioId
                                            }).ToList(),
                                 inventarios = (from i in _context.Inventarios
                                                join e in _context.Empaques
                                                on i.EmpaqueId equals e.Id
                                                join pr in _context.Productos
                                                on i.ProductoCodigo equals pr.Codigo
                                                join pi in _context.PedidosInventarios
                                                on i.Id equals pi.InventarioId
                                                where pi.PedidosCodigo == p.Codigo
                                                select new
                                                {
                                                    id = i.Id,
                                                    fecha = i.Fecha,
                                                    stock = i.Stock,
                                                    precioUn = i.PrecioUn,
                                                    origen = i.Origen,
                                                    productoCodi = i.ProductoCodigo,
                                                    empaqueId = i.EmpaqueId,
                                                    nombreEmpa = e.Nombre + " " + e.Tamannio,
                                                    nombrePro = pr.Nombre,
                                                    cantidad = pi.Cantidad,
                                                    precio = pi.Precio,
                                                    total = pi.Total,
                                                    priImagen = (from im in _context.Imagens
                                                                 where im.InventarioId == i.Id
                                                                 select new
                                                                 {
                                                                     id = im.Id,
                                                                     imagen = im.Imagen1,
                                                                     idInven = im.InventarioId
                                                                 }).ToList()
                                                }).ToList()
                             }).ToList();

                List<Pedidos> list = new List<Pedidos>();

                foreach (var item in query)
                {
                    Pedidos NewItem = new Pedidos();

                    NewItem.Codigo = item.codigo;
                    NewItem.Fecha = item.fecha;
                    NewItem.FechaEn = item.fechaEn;
                    NewItem.Total = item.total;
                    NewItem.UsuarioIdUsuario = item.usuarioId;
                    Usuario usuario = new Usuario();
                    foreach (var item1 in item.usuario)
                    {
                        usuario.IdUsuario = item1.id;
                        usuario.Nombre = item1.nombre;
                        usuario.Apellido1 = item1.apellido1;
                        usuario.Apellido2 = item1.apellido2;
                        usuario.Email = item1.email;
                        usuario.EmailResp = item1.emailRes;
                        usuario.Telefono = item1.telefono;
                        usuario.TusuarioId = item1.Tusuario;
                    }
                    NewItem.Usuario = usuario;
                    List<PedidosCalcu> inventario = new List<PedidosCalcu>(); //esta clase de PedidosCalcu se creo para trae los inventarios y sus cantidades y sumas respectivas que se encuentra en la tabla que los relaciona
                    foreach (var item2 in item.inventarios)
                    {
                        PedidosCalcu NewIven = new PedidosCalcu();

                        NewIven.Id = item2.id;
                        NewIven.Fecha = item2.fecha;
                        NewIven.Stock = item2.stock;
                        NewIven.PrecioUn = item2.precioUn;
                        NewIven.Origen = item2.origen;
                        NewIven.ProductoCodigo = item2.productoCodi;
                        NewIven.EmpaqueId = item2.empaqueId;
                        NewIven.NombreEmp = item2.nombreEmpa;
                        NewIven.NombrePro = item2.nombrePro;
                        NewIven.Cantidad = item2.cantidad;
                        NewIven.Precio = item2.precio;
                        NewIven.Total = item2.total;

                        ObservableCollection<Imagen> listIma = new ObservableCollection<Imagen>();
                        foreach (var item3 in item2.priImagen)
                        {
                            Imagen imagen = new Imagen();

                            imagen.Id = item3.id;
                            imagen.Imagen1 = item3.imagen;
                            imagen.InventarioId = item3.idInven;

                            listIma.Add(imagen);
                        }
                        if (listIma.Count > 0)
                        {
                            NewIven.priImagen = listIma[0].Imagen1;
                        }

                        inventario.Add(NewIven);
                    }
                    NewItem.inventarios = inventario;

                    list.Add(NewItem);
                }

                if (list == null)
                {
                    return NotFound();
                }

                return list;
            }
        }

        // PUT: api/Pedidoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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

        // POST: api/Pedidoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
          if (_context.Pedidos == null)
          {
              return Problem("Entity set 'ShopColibriContext.Pedidos'  is null.");
          }
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedido", new { id = pedido.Codigo }, pedido);
        }

        // DELETE: api/Pedidoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return (_context.Pedidos?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
