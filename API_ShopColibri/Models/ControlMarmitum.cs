using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class ControlMarmitum
{
    public int Codigo { get; set; }

    public DateTime Fecha { get; set; }

    public TimeSpan HoraEn { get; set; }

    public TimeSpan HoraAp { get; set; }

    public int Temperatura { get; set; }

    public string IntensidadMov { get; set; } = null!;

    public string Lote { get; set; } = null!;

    public virtual ICollection<Usuario> UsuarioIdUsuarios { get; } = new List<Usuario>();
}
