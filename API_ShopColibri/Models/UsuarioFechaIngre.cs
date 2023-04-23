using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class UsuarioFechaIngre
{
    public int DetalleId { get; set; }

    public int UsuarioIdUsuario { get; set; }

    public int FechaIngreId { get; set; }

    public DateTime Fecha { get; set; }

    public virtual FechaIngre? FechaIngre { get; set; } = null!;

    public virtual Usuario? UsuarioIdUsuarioNavigation { get; set; } = null!;
}
