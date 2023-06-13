using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class Inventario
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public int Stock { get; set; }

    public decimal PrecioUn { get; set; }

    public string Origen { get; set; } = null!;

    public int ProductoCodigo { get; set; }

    public int EmpaqueId { get; set; }

    public virtual Empaque? Empaque { get; set; } = null!;

    public virtual ICollection<FechaIngre> FechaIngres { get; set; } = new List<FechaIngre>();

    public virtual ICollection<Imagen> Imagens { get; set; } = new List<Imagen>();

    public virtual ICollection<PedidosInventario> PedidosInventarios { get; set; } = new List<PedidosInventario>();

    public virtual Producto? ProductoCodigoNavigation { get; set; } = null!;
}
