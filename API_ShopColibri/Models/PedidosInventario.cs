using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class PedidosInventario
{
    public int DetalleId { get; set; }

    public int PedidosCodigo { get; set; }

    public int InventarioId { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public decimal Total { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Inventario? Inventario { get; set; } = null!;

    public virtual Pedido? PedidosCodigoNavigation { get; set; } = null!;
}
