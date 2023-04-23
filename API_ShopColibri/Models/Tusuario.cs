using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class Tusuario
{
    public int Id { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
