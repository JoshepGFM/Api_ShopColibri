using Newtonsoft.Json;

namespace API_ShopColibri.Models
{
    public class ImagenDrive
    {
        public string Archivo { get ; set; }

        public static IFormFile ConvertBase64(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            var file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "image/jpeg", $"{Guid.NewGuid()}.jpg");
            return file;
        }
    }
}