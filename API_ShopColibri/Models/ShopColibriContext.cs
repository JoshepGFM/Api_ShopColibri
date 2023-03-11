using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_ShopColibri.Models;

public partial class ShopColibriContext : DbContext
{
    public ShopColibriContext()
    {
    }

    public ShopColibriContext(DbContextOptions<ShopColibriContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bitacora> Bitacoras { get; set; }

    public virtual DbSet<BitacoraSalida> BitacoraSalidas { get; set; }

    public virtual DbSet<ControlMarmitum> ControlMarmita { get; set; }

    public virtual DbSet<Empaque> Empaques { get; set; }

    public virtual DbSet<FechaIngre> FechaIngres { get; set; }

    public virtual DbSet<Imagen> Imagens { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Registro> Registros { get; set; }

    public virtual DbSet<Tusuario> Tusuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("SERVER=JOSHEP\\MSSQLSERVER01; DATABASE=ShopColibri;INTEGRATED SECURITY=TRUE; Encrypt=False; User Id=;Password=");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bitacora>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bitacora__3214EC0786DEA732");

            entity.ToTable("Bitacora");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
        });

        modelBuilder.Entity<BitacoraSalida>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bitacora__3214EC07494B6B97");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.ObjetoRef)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ControlMarmitum>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__ControlM__06370DAD2045DEF9");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IntensidadMov)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Lote)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empaque>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empaque__3214EC271302FA2F");

            entity.ToTable("Empaque");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Tamannio)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FechaIngre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FechaIng__3214EC07B7D14E13");

            entity.ToTable("FechaIngre");

            entity.Property(e => e.EmpaqueId).HasColumnName("EmpaqueID");
            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.Empaque).WithMany(p => p.FechaIngres)
                .HasForeignKey(d => d.EmpaqueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFechaIngre973916");
        });

        modelBuilder.Entity<Imagen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Imagen__3214EC074DE10971");

            entity.ToTable("Imagen");

            entity.Property(e => e.Imagen1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Imagen");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventar__3214EC0743F4176D");

            entity.ToTable("Inventario");

            entity.Property(e => e.EmpaqueId).HasColumnName("EmpaqueID");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Origen)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.PrecioUn).HasColumnType("decimal(19, 0)");

            entity.HasOne(d => d.Empaque).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.EmpaqueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKInventario106659");

            entity.HasOne(d => d.Imagen).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.ImagenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKInventario649567");

            entity.HasOne(d => d.ProductoCodigoNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.ProductoCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKInventario414748");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Pedidos__06370DAD65CB21FC");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaEn).HasColumnType("datetime");
            entity.Property(e => e.Precio).HasColumnType("decimal(19, 0)");
            entity.Property(e => e.Total).HasColumnType("decimal(19, 0)");
            entity.Property(e => e.UsuarioIdUsuario).HasColumnName("UsuarioId_Usuario");

            entity.HasOne(d => d.UsuarioIdUsuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.UsuarioIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPedidos224413");

            entity.HasMany(d => d.Inventarios).WithMany(p => p.PedidosCodigos)
                .UsingEntity<Dictionary<string, object>>(
                    "PedidosInventario",
                    r => r.HasOne<Inventario>().WithMany()
                        .HasForeignKey("InventarioId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKPedidos_In738396"),
                    l => l.HasOne<Pedido>().WithMany()
                        .HasForeignKey("PedidosCodigo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKPedidos_In236078"),
                    j =>
                    {
                        j.HasKey("PedidosCodigo", "InventarioId").HasName("PK__Pedidos___3EB9A2C1F0C8344A");
                        j.ToTable("Pedidos_Inventario");
                    });
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Producto__06370DAD349E3628");

            entity.ToTable("Producto");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Registro__3214EC07167D8155");

            entity.ToTable("Registro");

            entity.Property(e => e.CostoHora).HasColumnType("decimal(19, 0)");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(19, 0)");
            entity.Property(e => e.UsuarioIdUsuario).HasColumnName("UsuarioId_Usuario");

            entity.HasOne(d => d.UsuarioIdUsuarioNavigation).WithMany(p => p.Registros)
                .HasForeignKey(d => d.UsuarioIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKRegistro320380");
        });

        modelBuilder.Entity<Tusuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TUsuario__3214EC0748C8A2D9");

            entity.ToTable("TUsuario");

            entity.Property(e => e.Tipo)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__63C76BE2FE1679AA");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534B2B5BA02").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.Apellido1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Contrasennia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EmailResp)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TusuarioId).HasColumnName("TUsuarioId");

            entity.HasOne(d => d.Tusuario).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TusuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUsuario221017");

            entity.HasMany(d => d.ControlMarmitaCodigos).WithMany(p => p.UsuarioIdUsuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioControlMarmitum",
                    r => r.HasOne<ControlMarmitum>().WithMany()
                        .HasForeignKey("ControlMarmitaCodigo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKUsuario_Co363337"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("UsuarioIdUsuario")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKUsuario_Co377774"),
                    j =>
                    {
                        j.HasKey("UsuarioIdUsuario", "ControlMarmitaCodigo").HasName("PK__Usuario___F82F0B42E1D52407");
                        j.ToTable("Usuario_ControlMarmita");
                        j.IndexerProperty<int>("UsuarioIdUsuario").HasColumnName("UsuarioId_Usuario");
                    });

            entity.HasMany(d => d.FechaIngres).WithMany(p => p.UsuarioIdUsuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioFechaIngre",
                    r => r.HasOne<FechaIngre>().WithMany()
                        .HasForeignKey("FechaIngreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKUsuario_Fe391300"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("UsuarioIdUsuario")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKUsuario_Fe999044"),
                    j =>
                    {
                        j.HasKey("UsuarioIdUsuario", "FechaIngreId").HasName("PK__Usuario___147A47C56E758E06");
                        j.ToTable("Usuario_FechaIngre");
                        j.IndexerProperty<int>("UsuarioIdUsuario").HasColumnName("UsuarioId_Usuario");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
