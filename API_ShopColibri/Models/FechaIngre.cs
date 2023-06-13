using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class FechaIngre
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public int Entrada { get; set; }

    public int? EmpaqueId { get; set; }

    public int? InventarioId { get; set; }

    public virtual Empaque? Empaque { get; set; }

    public virtual Inventario? Inventario { get; set; }

    public virtual ICollection<UsuarioFechaIngre> UsuarioFechaIngres { get; set; } = new List<UsuarioFechaIngre>();
}
