using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class Bitacora
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public string Descripcion { get; set; } = null!;
}
