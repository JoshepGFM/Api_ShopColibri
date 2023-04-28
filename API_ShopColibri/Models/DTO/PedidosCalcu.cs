namespace API_ShopColibri.Models.DTO
{
    public class PedidosCalcu
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int Stock { get; set; }

        public decimal PrecioUn { get; set; }

        public string Origen { get; set; } = null!;

        public int ProductoCodigo { get; set; }

        public int EmpaqueId { get; set; }

        public string NombreEmp { get; set; }

        public string NombrePro { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public decimal Total { get; set; }
    }
}
