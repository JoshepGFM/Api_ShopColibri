using API_ShopColibri.Controllers;
using System;
using System.Collections.Generic;

namespace API_ShopColibri.Models;

public partial class Usuario
{
    public Usuario() { 
        Pedidos = new HashSet<Pedido>();
        Registros = new HashSet<Registro>();
        ControlMarmitaCodigos = new HashSet<ControlMarmitum>();
        FechaIngres = new HashSet<FechaIngre>();
    }
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string Apellido2 { get; set; } = null!;

    public string? Email { get; set; }

    public string Contrasennia { get; set; } = null!;

    public string? EmailResp { get; set; }

    public string Telefono { get; set; } = null!;

    public bool Estado { get; set; }

    public int TusuarioId { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; } = new List<Pedido>();

    public virtual ICollection<Registro> Registros { get; } = new List<Registro>();

    public virtual Tusuario Tusuario { get; set; } = null!;

    public virtual ICollection<ControlMarmitum> ControlMarmitaCodigos { get; } = new List<ControlMarmitum>();

    public virtual ICollection<FechaIngre> FechaIngres { get; } = new List<FechaIngre>();
}
