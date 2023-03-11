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

    public int ImagenId { get; set; }

    public int EmpaqueId { get; set; }

    public virtual Empaque Empaque { get; set; } = null!;

    public virtual Imagen Imagen { get; set; } = null!;

    public virtual Producto ProductoCodigoNavigation { get; set; } = null!;

    public virtual ICollection<Pedido> PedidosCodigos { get; } = new List<Pedido>();
}
