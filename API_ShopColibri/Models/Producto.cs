﻿using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class Producto
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
