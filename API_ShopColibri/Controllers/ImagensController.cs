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
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace API_ShopColibri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
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
        [HttpDelete("EliminarMasivo")]
        public async Task<IActionResult> DeleteImagen(List<int> imagenes)
        {
            try
            {
                if (_context.Imagens == null)
                {
                    return NotFound();
                }
                foreach (var proces in imagenes)
                {
                    var imagen = await _context.Imagens.FindAsync(proces);
                    if (imagen == null)
                    {
                        return NotFound();
                    }

                    _context.Imagens.Remove(imagen);

                    Dv.EliminarImagen(imagen.Imagen1);
                    await _context.SaveChangesAsync();
                }
                return NoContent();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private bool ImagenExists(int id)
        {
            return (_context.Imagens?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #region MetodosDrive
        // GuardarImagen/GuardarImagen
        [HttpPost("GuardarImagen")]
        public async Task<IActionResult> GuardarImagen(ImagenDrive archivo, int IdInve)
        {
            try
            {
                if (archivo == null || archivo.Archivo.Length == 0)
                {
                    return NotFound("error imagen");
                }

                IFormFile Archivo = ImagenDrive.ConvertBase64(archivo.Archivo);

                string imagenName = Archivo.FileName;
                string extension = Path.GetExtension(imagenName);

                string[] allowedExtensions = { ".jpg", ".png", ".JPG", ".PNG", ".jpeg", ".JPEG" };

                if (!allowedExtensions.Contains(extension))
                {
                    return NotFound("error extension");
                }

                string newFileName = $"{Guid.NewGuid()}{extension}";
                string filePath = $"Imagenes/{newFileName}";

                using (var imagenL = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await Archivo.CopyToAsync(imagenL);
                }
                string IdFol = Dv.ValidarFolder();
                string IdImagen = Dv.SubirArchivo(filePath, IdFol);

                Imagen imagen = new Imagen();

                imagen.Imagen1 = IdImagen;
                imagen.InventarioId = IdInve;

                await PostImagen(imagen);

                System.IO.File.Delete(filePath);

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        #endregion
    }
}
