using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class Empaque
{
    public Empaque()
    {
        FechaIngres = new HashSet<FechaIngre>();
        Inventarios = new HashSet<Inventario>();
    }
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Tamannio { get; set; } = null!;

    public int Stock { get; set; }

    public virtual ICollection<FechaIngre> FechaIngres { get; } = new List<FechaIngre>();

    public virtual ICollection<Inventario> Inventarios { get; } = new List<Inventario>();
}
