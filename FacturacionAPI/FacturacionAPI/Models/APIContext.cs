using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FacturacionAPI.Models
{
    public partial class APIContext : DbContext
    {
        public APIContext()
        {
        }

        public APIContext(DbContextOptions<APIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Empresa> Empresas { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost; Database=API; Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.CompaniaId).HasColumnName("companiaId");

                entity.Property(e => e.Correo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Rnc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("rnc");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.Compania)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.CompaniaId)
                    .HasConstraintName("FK__Cliente__compani__300424B4");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("Empresa");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Itbis).HasColumnName("itbis");

                entity.Property(e => e.Ncf)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ncf");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Rnc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("rnc");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("factura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClienteId).HasColumnName("clienteId");

                entity.Property(e => e.CompaniaId).HasColumnName("companiaId");

                entity.Property(e => e.CondicionPago)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("condicionPago");

                entity.Property(e => e.Estado)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.ModoPago)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("modoPago");

                entity.Property(e => e.Ncf)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ncf");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.UsaComprobante).HasColumnName("usaComprobante");

                entity.Property(e => e.Vence)
                    .HasColumnType("date")
                    .HasColumnName("vence");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK__factura__cliente__3F466844");

                entity.HasOne(d => d.Compania)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.CompaniaId)
                    .HasConstraintName("FK__factura__compani__403A8C7D");
            });

            modelBuilder.Entity<FacturaDetalle>(entity =>
            {
                entity.ToTable("facturaDetalle");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.FacturaId).HasColumnName("facturaId");

                entity.Property(e => e.ProductoId).HasColumnName("productoId");

                entity.HasOne(d => d.Factura)
                    .WithMany(p => p.FacturaDetalles)
                    .HasForeignKey(d => d.FacturaId)
                    .HasConstraintName("FK__facturaDe__factu__46E78A0C");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.FacturaDetalles)
                    .HasForeignKey(d => d.ProductoId)
                    .HasConstraintName("FK__facturaDe__produ__47DBAE45");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.CompaniaId).HasColumnName("companiaId");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.Compania)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.CompaniaId)
                    .HasConstraintName("FK__Producto__compan__36B12243");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
