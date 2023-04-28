namespace API_ShopColibri.Models.DTO
{
    public class Pedidos
    {
        public int Codigo { get; set; }

        public DateTime Fecha { get; set; }

        public DateTime FechaEn { get; set; }

        public decimal Total { get; set; }

        public int UsuarioIdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        public List<PedidosCalcu> inventarios { get; set; }
    }
}
