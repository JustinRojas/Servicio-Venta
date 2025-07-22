using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Model;

namespace SistemaVentas.DAL.DBContext
{
    public partial class DBContextSistemaVenta : DbContext
    {
        public DBContextSistemaVenta()
        {
        }

        public DBContextSistemaVenta(DbContextOptions<DBContextSistemaVenta> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuRol> MenuRols { get; set; }
        public virtual DbSet<NumeroDocumento> NumeroDocumentos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.EsActivo).HasDefaultValueSql("((1))");
                entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<MenuRol>(entity =>
            {
                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany(p => p.MenuRols)
                    .HasForeignKey(d => d.IdMenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuRol_Menu");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.MenuRols)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuRol_Rol");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.EsActivo).HasDefaultValueSql("((1))");
                entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Producto_Categoria");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.EsActivo).HasDefaultValueSql("((1))");
                entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Rol");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleVenta_Producto");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleVenta_Venta");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        public void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {

        }
    }
}
