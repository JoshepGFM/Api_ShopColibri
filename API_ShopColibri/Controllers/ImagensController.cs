using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ShopColibri.Models;
using API_ShopColibri.Attributes;
using System.Drawing;
using Google.Apis.Drive.v3;

namespace API_ShopColibri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagensController : ControllerBase
    {
        private readonly ShopColibriContext _context;
        private Drive Dv;

        public ImagensController(ShopColibriContext context)
        {
            _context = context;
            Dv = new Drive();
        }

        // GET: api/Imagens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Imagen>>> GetImagens()
        {
          if (_context.Imagens == null)
          {
              return NotFound();
          }
            return await _context.Imagens.ToListAsync();
        }

        // GET: api/Imagens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Imagen>> GetImagen(int id)
        {
          if (_context.Imagens == null)
          {
              return NotFound();
          }
            var imagen = await _context.Imagens.FindAsync(id);

            if (imagen == null)
            {
                return NotFound();
            }

            return imagen;
        }

        // PUT: api/Imagens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagen(int id, Imagen imagen)
        {
            if (id != imagen.Id)
            {
                return BadRequest();
            }

            _context.Entry(imagen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagenExists(id))
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

        // POST: api/Imagens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Imagen>> PostImagen(Imagen imagen)
        {
          if (_context.Imagens == null)
          {
              return Problem("Entity set 'ShopColibriContext.Imagens'  is null.");
          }
            _context.Imagens.Add(imagen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImagen", new { id = imagen.Id }, imagen);
        }

        // DELETE: api/Imagens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagen(int id)
        {
            if (_context.Imagens == null)
            {
                return NotFound();
            }
            var imagen = await _context.Imagens.FindAsync(id);
            if (imagen == null)
            {
                return NotFound();
            }

            _context.Imagens.Remove(imagen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImagenExists(int id)
        {
            return (_context.Imagens?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #region MetodosDrive

        [HttpPost("GuardarImagen")]
        public async Task<string> GuardarImagen([FromForm] ImagenDrive Imagen)
        {
            string ruta = String.Empty;
            if (Imagen.Archivo.Length > 0)
            {
                var nombreArchivo = Guid.NewGuid().ToString()+".jpg";
                ruta = $"Imagenes/{nombreArchivo}";
                using(var stream = new FileStream(ruta, FileMode.Create))
                {
                    await Imagen.Archivo.CopyToAsync(stream);
                }
                string IdFol = Dv.ValidarFolder();
                Dv.SubirArchivo(ruta, IdFol);

                System.IO.File.Delete(ruta);
            }
            return ruta;
        }
        #endregion
    }
}
