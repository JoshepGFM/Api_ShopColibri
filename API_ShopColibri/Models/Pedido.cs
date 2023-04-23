using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class Pedido
{
    public int Codigo { get; set; }

    public DateTime Fecha { get; set; }

    public DateTime FechaEn { get; set; }

    public decimal Total { get; set; }

    public int UsuarioIdUsuario { get; set; }

    public virtual ICollection<PedidosInventario> PedidosInventarios { get; set; } = new List<PedidosInventario>();

    public virtual Usuario? UsuarioIdUsuarioNavigation { get; set; } = null!;
}
