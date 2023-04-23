using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class Imagen
{
    public int Id { get; set; }

    public string Imagen1 { get; set; } = null!;

    public int InventarioId { get; set; }

    public virtual Inventario? Inventario { get; set; } = null!;
}
