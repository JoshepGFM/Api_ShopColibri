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
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net;
using Microsoft.AspNetCore.JsonPatch;

namespace API_ShopColibri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ShopColibriContext _context;
        private readonly AutoMapper.Mapper _mapper;
        public Tools.Crypto MyCrypto { get; set; }

        public UsuariosController(ShopColibriContext context)
        {
            _context = context;
            MyCrypto = new Tools.Crypto();
        }

        // GET: api/Usuarios
        [ApiKey]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/UsuariosPrincipal
        [ApiKey]
        [HttpGet("UsuariosPrincipal")]
        public ActionResult<IEnumerable<Usuarios>> GetUsuariosPrincipal()
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var query = (from u in _context.Usuarios
                         join t in _context.Tusuarios
                         on u.TusuarioId equals t.Id
                         where u.Estado == true && (u.TusuarioId == 1 || u.TusuarioId == 2)
                         select new
                         {
                             id = u.IdUsuario,
                             nombre = u.Nombre,
                             apellido1 = u.Apellido1,
                             apellido2 = u.Apellido2,
                             email = u.Email,
                             emailResp = u.EmailResp,
                             telefono = u.Telefono,
                             tUsuario = u.TusuarioId,
                             tipo = t.Tipo
                         }).ToList();
            List<Usuarios> list = new List<Usuarios>();

            foreach (var item in query)
            {
                Usuarios NewItem = new Usuarios();

                NewItem.IdUsuario = item.id;
                NewItem.Nombre = item.nombre;
                NewItem.Apellido1 = item.apellido1;
                NewItem.Apellido2 = item.apellido2;
                NewItem.Email = item.email;
                NewItem.EmailResp = item.emailResp;
                NewItem.Telefono = item.telefono;
                NewItem.TusuarioId = item.tUsuario;
                NewItem.Tipo = item.tipo;

                list.Add(NewItem);
            }



            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        //Get: api/Usuarios/GetUsuario?email=admin%40gmail.com
        [ApiKey]
        [HttpGet("GetUsuario")]
        public ActionResult<IEnumerable<Usuarios>> GetUserInfo(string email)
        {
            var query = (from u in _context.Usuarios
                         join t in _context.Tusuarios on
                         u.TusuarioId equals t.Id
                         where (u.Email == email || u.EmailResp == email) || (u.Nombre + " " + u.Apellido1 + " " + u.Apellido2).Contains(email) && u.Estado == true
                         select new
                         {
                             idusuario = u.IdUsuario,
                             nombre = u.Nombre,
                             apellido1 = u.Apellido1,
                             apellido2 = u.Apellido2,
                             email = u.Email,
                             emailResp = u.EmailResp,
                             telefono = u.Telefono,
                             tUsuario = t.Id,
                             tipo = t.Tipo
                         }).ToList();

            List<Usuarios> list = new List<Usuarios>();

            foreach (var item in query)
            {
                Usuarios NewItem = new Usuarios();

                NewItem.IdUsuario = item.idusuario;
                NewItem.Nombre = item.nombre;
                NewItem.Apellido1 = item.apellido1;
                NewItem.Apellido2 = item.apellido2;
                NewItem.Email = item.email;
                NewItem.EmailResp = item.emailResp;
                NewItem.Telefono = item.telefono;
                NewItem.TusuarioId = item.tUsuario;
                NewItem.Tipo = item.tipo;

                list.Add(NewItem);
            }



            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        //GET: api/Usuarios/GetUsuarioBuscar?buscar=dwf&estado=true&Cliente=true
        [ApiKey]
        [HttpGet("GetUsuarioBuscar")]
        public ActionResult<IEnumerable<Usuarios>> GetUserBuscar(string? buscar, bool estado, bool Cliente)
        {
            if (Cliente)
            {
                if (string.IsNullOrEmpty(buscar))
                {
                    var query = (from u in _context.Usuarios
                                 join t in _context.Tusuarios on
                                 u.TusuarioId equals t.Id
                                 where u.Estado == estado
                                 select new
                                 {
                                     idusuario = u.IdUsuario,
                                     nombre = u.Nombre,
                                     apellido1 = u.Apellido1,
                                     apellido2 = u.Apellido2,
                                     email = u.Email,
                                     emailResp = u.EmailResp,
                                     telefono = u.Telefono,
                                     tUsuario = t.Id,
                                     tipo = t.Tipo
                                 }).ToList();

                    List<Usuarios> list = new List<Usuarios>();

                    foreach (var item in query)
                    {
                        Usuarios NewItem = new Usuarios();

                        NewItem.IdUsuario = item.idusuario;
                        NewItem.Nombre = item.nombre;
                        NewItem.Apellido1 = item.apellido1;
                        NewItem.Apellido2 = item.apellido2;
                        NewItem.Email = item.email;
                        NewItem.EmailResp = item.emailResp;
                        NewItem.Telefono = item.telefono;
                        NewItem.TusuarioId = item.tUsuario;
                        NewItem.Tipo = item.tipo;

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
                    var query = (from u in _context.Usuarios
                                 join t in _context.Tusuarios on
                                 u.TusuarioId equals t.Id
                                 where (u.Nombre + " " + u.Apellido1 + " " + u.Apellido2).Contains(buscar)
                                 select new
                                 {
                                     idusuario = u.IdUsuario,
                                     nombre = u.Nombre,
                                     apellido1 = u.Apellido1,
                                     apellido2 = u.Apellido2,
                                     email = u.Email,
                                     emailResp = u.EmailResp,
                                     telefono = u.Telefono,
                                     tUsuario = t.Id,
                                     tipo = t.Tipo
                                 }).ToList();

                    List<Usuarios> list = new List<Usuarios>();

                    foreach (var item in query)
                    {
                        Usuarios NewItem = new Usuarios();

                        NewItem.IdUsuario = item.idusuario;
                        NewItem.Nombre = item.nombre;
                        NewItem.Apellido1 = item.apellido1;
                        NewItem.Apellido2 = item.apellido2;
                        NewItem.Email = item.email;
                        NewItem.EmailResp = item.emailResp;
                        NewItem.Telefono = item.telefono;
                        NewItem.TusuarioId = item.tUsuario;
                        NewItem.Tipo = item.tipo;

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
                if (string.IsNullOrEmpty(buscar))
                {
                    var query = (from u in _context.Usuarios
                                 join t in _context.Tusuarios on
                                 u.TusuarioId equals t.Id
                                 where u.Estado == estado & (u.TusuarioId == 1 || u.TusuarioId == 2)
                                 select new
                                 {
                                     idusuario = u.IdUsuario,
                                     nombre = u.Nombre,
                                     apellido1 = u.Apellido1,
                                     apellido2 = u.Apellido2,
                                     email = u.Email,
                                     emailResp = u.EmailResp,
                                     telefono = u.Telefono,
                                     tUsuario = t.Id,
                                     tipo = t.Tipo
                                 }).ToList();

                    List<Usuarios> list = new List<Usuarios>();

                    foreach (var item in query)
                    {
                        Usuarios NewItem = new Usuarios();

                        NewItem.IdUsuario = item.idusuario;
                        NewItem.Nombre = item.nombre;
                        NewItem.Apellido1 = item.apellido1;
                        NewItem.Apellido2 = item.apellido2;
                        NewItem.Email = item.email;
                        NewItem.EmailResp = item.emailResp;
                        NewItem.Telefono = item.telefono;
                        NewItem.TusuarioId = item.tUsuario;
                        NewItem.Tipo = item.tipo;

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
                    var query = (from u in _context.Usuarios
                                 join t in _context.Tusuarios on
                                 u.TusuarioId equals t.Id
                                 where (u.Nombre + " " + u.Apellido1 + " " + u.Apellido2).Contains(buscar) & (u.TusuarioId == 1 || u.TusuarioId == 2)
                                 select new
                                 {
                                     idusuario = u.IdUsuario,
                                     nombre = u.Nombre,
                                     apellido1 = u.Apellido1,
                                     apellido2 = u.Apellido2,
                                     email = u.Email,
                                     emailResp = u.EmailResp,
                                     telefono = u.Telefono,
                                     tUsuario = t.Id,
                                     tipo = t.Tipo
                                 }).ToList();

                    List<Usuarios> list = new List<Usuarios>();

                    foreach (var item in query)
                    {
                        Usuarios NewItem = new Usuarios();

                        NewItem.IdUsuario = item.idusuario;
                        NewItem.Nombre = item.nombre;
                        NewItem.Apellido1 = item.apellido1;
                        NewItem.Apellido2 = item.apellido2;
                        NewItem.Email = item.email;
                        NewItem.EmailResp = item.emailResp;
                        NewItem.Telefono = item.telefono;
                        NewItem.TusuarioId = item.tUsuario;
                        NewItem.Tipo = item.tipo;

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

        // GET: api/Usuarios/5
        [ApiKey]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // GET: api/Usuarios/ValidarLogin?Usuario=admin%40gmail.com&pass=123
        [ApiKey]
        [HttpGet("ValidarLogin")]
        public async Task<ActionResult<Usuario>> ValidarLogin(string Usuario, string pass)
        {
            string PassEncry = MyCrypto.EncriptarEnUnSentido(pass);

            var usuario = await _context.Usuarios.SingleOrDefaultAsync(e => (e.Email == Usuario || e.EmailResp == Usuario) || (e.Nombre + " " + e.Apellido1 + " " + e.Apellido2).Contains(Usuario) && e.Contrasennia == PassEncry && e.Estado == true);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // GET: api/Usuarios/ValidarConexion
        [ApiKey]
        [HttpGet("ValidarConexion")]
        public async Task<ActionResult<Usuarios>> ValidarConexion()
        {
            return Ok();
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [ApiKey]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios?s=1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [ApiKey]
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuarios usuario, int s)
        {
            string EncriptedPass = MyCrypto.EncriptarEnUnSentido(usuario.Contrasennia);

            usuario.Contrasennia = EncriptedPass;

                Usuario NewItem = new Usuario();

                NewItem.Nombre = usuario.Nombre;
                NewItem.Apellido1 = usuario.Apellido1;
                NewItem.Apellido2 = usuario.Apellido2;
                NewItem.Email = usuario.Email;
                NewItem.Contrasennia = usuario.Contrasennia;
                NewItem.EmailResp = usuario.EmailResp;
                NewItem.Telefono = usuario.Telefono;
                NewItem.TusuarioId = usuario.TusuarioId;
                if (s == 1) { NewItem.Estado = true; }
                else { NewItem.Estado = false; }
                

            if (_context.Usuarios == null)
          {
              return Problem("Entity set 'ShopColibriContext.Usuarios'  is null.");
          }
   
            _context.Usuarios.Add(NewItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario }, usuario);
        }
        //PATCH: api/Usuarios/ModificarUsuario?id=1
        [ApiKey]
        [HttpPatch("ModificarUsuario")]
        public async Task<ActionResult> PatchUsuario(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            string EncryPass = MyCrypto.EncriptarEnUnSentido(user.Contrasennia);
            user.Contrasennia = EncryPass;
            if (user.Estado == false)
            {
                user.Estado = true;
            }
            _context.Usuarios.Add(user);

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // DELETE: api/Usuarios/Validar?id=25
        [HttpGet("Validar")]
        public async Task<IActionResult> ValidarUsuario(int id, bool R)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (!R)
            {
                if (user.Estado != true)
                {
                    user.Estado = true;
                }
                else
                {
                    return Ok("Ya esta acctiva la cuenta");
                }
            }
            else
            {
                if (user.Estado == true)
                {
                    user.Estado = false;
                }
                else
                {
                    user.Estado = true;
                }
            }
            _context.Usuarios.Add(user);

            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            if (!R)
            {
                return Ok("Se ha activado con exito la cuenta");
            }
            else
            {
                return Ok();
            }
        }

        // DELETE: api/Usuarios/5
        [ApiKey]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var producto = await _context.Usuarios.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
