namespace API_ShopColibri.Models.DTO
{
    public class FechaIngres
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int Entrada { get; set; }

        public int? EmpaqueId { get; set; }

        public int? InventarioId { get; set; }

        public string? EmpaqueNombre { get; set; }

        public string? InventarioNombre { get; set;}
    }
}
