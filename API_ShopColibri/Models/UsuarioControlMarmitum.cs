using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class UsuarioControlMarmitum
{
    public int DetalleId { get; set; }

    public int UsuarioIdUsuario { get; set; }

    public int ControlMarmitaCodigo { get; set; }

    public DateTime Fecha { get; set; }

    public virtual ControlMarmitum? ControlMarmitaCodigoNavigation { get; set; } = null!;

    public virtual Usuario? UsuarioIdUsuarioNavigation { get; set; } = null!;
}
