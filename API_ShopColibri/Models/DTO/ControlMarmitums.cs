namespace API_ShopColibri.Models.DTO
{
    public class ControlMarmitums
    {

        public int Codigo { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan HoraEn { get; set; }

        public TimeSpan HoraAp { get; set; }

        public int Temperatura { get; set; }

        public string IntensidadMov { get; set; } = null!;

        public string Lote { get; set; } = null!;

        public List<Usuario> Usuarios { get; set; }
    }
}
