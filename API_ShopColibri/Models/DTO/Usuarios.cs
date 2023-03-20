namespace API_ShopColibri.Models.DTO
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido1 { get; set; } = null!;
        public string Apellido2 { get; set; } = null!;
        public string? Email { get; set; }
        public string Contrasennia { get; set; } = null!;
        public string? EmailResp { get; set; }
        public string Telefono { get; set; } = null!;
        public int TusuarioId { get; set; }
        public string? Tipo { get; set; } = null!;
    }
}
