﻿namespace API_ShopColibri.Models.DTO
{
    public class Registros
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int HorasL { get; set; }

        public int HorasX { get; set; }

        public int HorasM { get; set; }

        public int HorasJ { get; set; }

        public int HorasV { get; set; }

        public int HorasS { get; set; }

        public int TotalHoras { get; set; }

        public decimal CostoHora { get; set; }

        public decimal Total { get; set; }

        public int UsuarioIdUsuario { get; set; }

        public string UsuarioName { get; set;}
    }
}
