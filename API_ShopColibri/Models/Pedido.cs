using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class Pedido
{
    public int Codigo { get; set; }

    public DateTime Fecha { get; set; }

    public DateTime FechaEn { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public decimal Total { get; set; }

    public int UsuarioIdUsuario { get; set; }

    public virtual Usuario UsuarioIdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; } = new List<Inventario>();
}
