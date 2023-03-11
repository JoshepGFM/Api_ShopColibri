using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class BitacoraSalida
{
    public DateTime Fecha { get; set; }

    public string ObjetoRef { get; set; } = null!;

    public int Salida { get; set; }

    public int Id { get; set; }
}
