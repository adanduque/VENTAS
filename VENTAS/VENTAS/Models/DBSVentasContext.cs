using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VENTAS.Models
{
    public partial class DBSVentasContext : IdentityDbContext<ApplicationUser>
    {
        public DBSVentasContext()
        {
        }

        public DBSVentasContext(DbContextOptions<DBSVentasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulo> Articuloes { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Comprobante> Comprobantes { get; set; }
        public virtual DbSet<Correlativo> Correlativoes { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumentoes { get; set; }
        public virtual DbSet<UndMedida> UndMedidas { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<VentaDetalle> VentaDetalles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-E50Q1F1;Initial Catalog=DBSVentas;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.ToTable("Articulo");

                entity.HasIndex(e => e.Nombre)
                    .HasName("IX_Articulo")
                    .IsUnique();

                entity.Property(e => e.Imagen)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("money");

                entity.Property(e => e.Stock).HasColumnType("numeric(5, 3)");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Articuloes)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Articulo_Categoria");

                entity.HasOne(d => d.UndMedida)
                    .WithMany(p => p.Articuloes)
                    .HasForeignKey(d => d.UndMedidaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Articulo_UndMedida");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("Categoria");

                entity.HasIndex(e => e.Nombre)
                    .HasName("IX_Categoria")
                    .IsUnique();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.HasIndex(e => e.NumDocumento)
                    .HasName("IX_Cliente")
                    .IsUnique();

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NombreRazonS)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NumDocumento)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.TipoDocumento)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.TipoDocumentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente_TipoDocumento");
            });

            modelBuilder.Entity<Comprobante>(entity =>
            {
                entity.ToTable("Comprobante");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Correlativo>(entity =>
            {
                entity.ToTable("Correlativo");

                entity.Property(e => e.Serie)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.Valor)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsFixedLength();

                entity.HasOne(d => d.Comprobante)
                    .WithMany(p => p.Correlativoes)
                    .HasForeignKey(d => d.ComprobanteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Correlativo_Comprobante");
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.ToTable("TipoDocumento");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DescripcionCorta)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DescripcionLarga)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Lo).HasColumnName("LO");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsFixedLength();
            });

            modelBuilder.Entity<UndMedida>(entity =>
            {
                entity.ToTable("UndMedida");

                entity.HasIndex(e => e.Nombre)
                    .HasName("IX_UndMedida")
                    .IsUnique();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("Venta");

                entity.Property(e => e.Correlativo)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsFixedLength();

                entity.Property(e => e.FechaVenta).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Serie)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.Total).HasColumnType("money");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Ventas)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Venta_Cliente");

                entity.HasOne(d => d.Comprobante)
                    .WithMany(p => p.Ventas)
                    .HasForeignKey(d => d.ComprobanteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Venta_Comprobante");
            });

            modelBuilder.Entity<VentaDetalle>(entity =>
            {
                entity.ToTable("VentaDetalle");

                entity.Property(e => e.Cantidad).HasColumnType("numeric(5, 3)");

                entity.Property(e => e.PrecioTotal).HasColumnType("money");

                entity.Property(e => e.PrecioUnitario).HasColumnType("money");

                entity.HasOne(d => d.Articulo)
                    .WithMany(p => p.VentaDetalles)
                    .HasForeignKey(d => d.ArticuloId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VentaDetalle_Articulo");

                entity.HasOne(d => d.Venta)
                    .WithMany(p => p.VentaDetalles)
                    .HasForeignKey(d => d.VentaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VentaDetalle_Venta");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
