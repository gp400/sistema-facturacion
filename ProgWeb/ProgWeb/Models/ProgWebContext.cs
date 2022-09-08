using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProgWeb.Models
{
    public partial class ProgWebContext : DbContext
    {
        public ProgWebContext()
        {
        }

        public ProgWebContext(DbContextOptions<ProgWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Integrante> Integrantes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;password=admin;database=ProgWeb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Integrante>(entity =>
            {
                entity.ToTable("integrantes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Alergias)
                    .HasColumnType("text")
                    .HasColumnName("alergias");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .HasColumnName("apellido");

                entity.Property(e => e.Comentario)
                    .HasColumnType("text")
                    .HasColumnName("comentario");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .HasColumnName("direccion");

                entity.Property(e => e.Documento)
                    .HasMaxLength(20)
                    .HasColumnName("documento");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.EstadoCivil)
                    .HasMaxLength(5)
                    .HasColumnName("estadoCivil")
                    .IsFixedLength();

                entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");

                entity.Property(e => e.Foto)
                    .HasColumnType("text")
                    .HasColumnName("foto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(10)
                    .HasColumnName("sexo");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(15)
                    .HasColumnName("telefono");

                entity.Property(e => e.TipoSangre)
                    .HasMaxLength(5)
                    .HasColumnName("tipoSangre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
