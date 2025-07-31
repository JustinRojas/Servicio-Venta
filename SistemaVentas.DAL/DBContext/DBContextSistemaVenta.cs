using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Model;



using Microsoft.EntityFrameworkCore;
using SistemaVentas.Model;

//public class DBContextSistemaVenta : DbContext
//{
//    public DBContextSistemaVenta(DbContextOptions<DBContextSistemaVenta> options) : base(options) { }

//    public DbSet<Rol> Rol { get; set; }
//    public DbSet<Menu> Menu { get; set; }
//    public DbSet<MenuRol> MenuRole { get; set; }
//    public DbSet<Usuario> Usuario { get; set; }
//    public DbSet<Categoria> Categoria { get; set; }
//    public DbSet<Producto> Producto { get; set; }
//    public DbSet<NumeroDocumento> NumeroDocumento { get; set; }
//    public DbSet<Venta> Venta { get; set; }
//    public DbSet<DetalleVenta> DetalleVenta { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        // Tabla Rol
//        modelBuilder.Entity<Rol>(entity =>
//        {
//            entity.HasKey(e => e.IdRol);
//            entity.Property(e => e.Nombre).HasMaxLength(50);
//            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("getdate()");
//        });

//        // Tabla Menu
//        modelBuilder.Entity<Menu>(entity =>
//        {
//            entity.HasKey(e => e.IdMenu);
//            entity.Property(e => e.Nombre).HasMaxLength(50);
//            entity.Property(e => e.Icono).HasMaxLength(50);
//            entity.Property(e => e.Url).HasMaxLength(50);
//        });

//        // Tabla MenuRol
//        modelBuilder.Entity<MenuRol>(entity =>
//        {
//            entity.HasKey(e => e.IdMenuRol);

//            entity.HasOne(e => e.IdMenuNavigation)
//                  .WithMany()
//                  .HasForeignKey(e => e.IdMenu);

//            entity.HasOne(e => e.IdRolNavigation)
//                  .WithMany()
//                  .HasForeignKey(e => e.IdRol);
//        });

//        // Tabla Usuario
//        modelBuilder.Entity<Usuario>(entity =>
//        {
//            entity.HasKey(e => e.IdUsuario);
//            entity.Property(e => e.NombreCompleto).HasMaxLength(100);
//            entity.Property(e => e.Correo).HasMaxLength(40);
//            entity.Property(e => e.Clave).HasMaxLength(40);
//            entity.Property(e => e.EsActivo).HasDefaultValue(true);
//            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("getdate()");

//            entity.HasOne(e => e.Rol)
//                  .WithMany()
//                  .HasForeignKey(e => e.IdRol);
//        });

//        // Tabla Categoria
//        modelBuilder.Entity<Categoria>(entity =>
//        {
//            entity.HasKey(e => e.IdCategoria);
//            entity.Property(e => e.Nombre).HasMaxLength(50);
//            entity.Property(e => e.EsActivo).HasDefaultValue(true);
//            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("getdate()");
//        });

//        // Tabla Producto
//        modelBuilder.Entity<Producto>(entity =>
//        {
//            entity.HasKey(e => e.IdProducto);
//            entity.Property(e => e.Nombre).HasMaxLength(100);
//            entity.Property(e => e.Precio).HasColumnType("decimal(10,2)");
//            entity.Property(e => e.EsActivo).HasDefaultValue(true);
//            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("getdate()");

//            entity.HasOne(e => e.IdCategoriaNavigation)
//                  .WithMany()
//                  .HasForeignKey(e => e.IdCategoria);
//        });

//        // Tabla NumeroDocumento
//        modelBuilder.Entity<NumeroDocumento>(entity =>
//        {
//            entity.HasKey(e => e.IdNumeroDocumento);
//            entity.Property(e => e.UltimoNumero).IsRequired();
//            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("getdate()");
//        });

//        // Tabla Venta
//        modelBuilder.Entity<Venta>(entity =>
//        {
//            entity.HasKey(e => e.IdVenta);
//            entity.Property(e => e.NumeroDocumento).HasMaxLength(40);
//            entity.Property(e => e.TipoPago).HasMaxLength(50);
//            entity.Property(e => e.Total).HasColumnType("decimal(10,2)");
//            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("getdate()");
//        });

//        // Tabla DetalleVenta
//        modelBuilder.Entity<DetalleVenta>(entity =>
//        {
//            entity.HasKey(e => e.IdDetalleVenta);
//            entity.Property(e => e.Cantidad);
//            entity.Property(e => e.Precio).HasColumnType("decimal(10,2)");
//            entity.Property(e => e.Total).HasColumnType("decimal(10,2)");

//            entity.HasOne(e => e.IdVentaNavigation)
//                  .WithMany()
//                  .HasForeignKey(e => e.IdVenta);

//            entity.HasOne(e => e.IdProductoNavigation)
//                  .WithMany()
//                  .HasForeignKey(e => e.IdProducto);
//        });
//    }
//}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentas.DAL.DBContext;

public partial class DBContextSistemaVenta : DbContext
{
    public DBContextSistemaVenta()
    {
    }

    public DBContextSistemaVenta(DbContextOptions<DBContextSistemaVenta> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRol> MenuRols { get; set; }

    public virtual DbSet<NumeroDocumento> NumeroDocumentos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:CadenaSQL");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__8A3D240C5418CF86");

            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.EsActivo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__BFE2843F2B8C89B8");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("idDetalleVenta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DetalleVe__idPro__66603565");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DetalleVe__idVen__656C112C");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__C26AF483DDBD8D23");

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("icono");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        modelBuilder.Entity<MenuRol>(entity =>
        {
            entity.HasKey(e => e.IdMenuRol).HasName("PK__MenuRol__9D6D61A436963996");

            entity.ToTable("MenuRol");

            entity.Property(e => e.IdMenuRol).HasColumnName("idMenuRol");
            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.IdRol).HasColumnName("idRol");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__MenuRol__idMenu__4E88ABD4");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__MenuRol__idRol__4F7CD00D");
        });

        modelBuilder.Entity<NumeroDocumento>(entity =>
        {
            entity.HasKey(e => e.IdNumeroDocumento).HasName("PK__NumeroDo__471E421A9E624391");

            entity.ToTable("NumeroDocumento");

            entity.Property(e => e.IdNumeroDocumento).HasColumnName("idNumeroDocumento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.UltimoNumero).HasColumnName("ultimo_Numero");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__07F4A132EF28F4EF");

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.EsActivo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Producto__idCate__5AEE82B9");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F76AE262358");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A6E473E6AF");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.EsActivo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreCompleto");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__idRol__160F4887");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__077D5614048834B3");

            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("numeroDocumento");
            entity.Property(e => e.TipoPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoPago");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
