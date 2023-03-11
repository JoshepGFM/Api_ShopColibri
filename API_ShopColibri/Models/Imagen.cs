using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class Imagen
{
    public int Id { get; set; }

    public string Imagen1 { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; } = new List<Inventario>();
}
