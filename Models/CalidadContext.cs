using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AeropuertoCalidad.Models
{
    public partial class CalidadContext : DbContext
    {
        public CalidadContext()
        {
        }

        public CalidadContext(DbContextOptions<CalidadContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aeropuerto> Aeropuerto { get; set; }
        public virtual DbSet<Ruta> Ruta { get; set; }
        public virtual DbSet<Vuelo> Vuelo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=Calidad;Username=postgres;Password=Navigo4.");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum(null, "estado", new[] { "A", "D" });

            modelBuilder.Entity<Aeropuerto>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("aeropuerto_pkey");

                entity.ToTable("aeropuerto");

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasMaxLength(3);

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasMaxLength(128);

                entity.Property(e => e.Habilitado).HasColumnName("habilitado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<Ruta>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("ruta_pkey");

                entity.ToTable("ruta");

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasMaxLength(6);

                entity.Property(e => e.Capacidadmaxima).HasColumnName("capacidadmaxima");

                entity.Property(e => e.Codigoaeropuerto)
                    .HasColumnName("codigoaeropuerto")
                    .HasMaxLength(6);

                entity.Property(e => e.Empresa)
                    .IsRequired()
                    .HasColumnName("empresa")
                    .HasMaxLength(16);

                entity.Property(e => e.Hora)
                    .HasColumnName("hora")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasConversion(e => e.ToString(),
                    e => (EstadoType)Enum.Parse(typeof(EstadoType), e));

                entity.Property(e => e.Lugar)
                    .IsRequired()
                    .HasColumnName("lugar")
                    .HasMaxLength(32);

                entity.HasOne(d => d.CodigoaeropuertoNavigation)
                    .WithMany(p => p.Ruta)
                    .HasForeignKey(d => d.Codigoaeropuerto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ruta_codigoaeropuerto_fkey");
            });

            modelBuilder.Entity<Vuelo>(entity =>
            {
                entity.HasKey(e => new { e.Codigoruta, e.Fecha })
                    .HasName("vuelo_pkey");

                entity.ToTable("vuelo");

                entity.Property(e => e.Codigoruta)
                    .HasColumnName("codigoruta")
                    .HasMaxLength(6);

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.Capacidadreal).HasColumnName("capacidadreal");

                entity.HasOne(d => d.CodigorutaNavigation)
                    .WithMany(p => p.Vuelo)
                    .HasForeignKey(d => d.Codigoruta)
                    .HasConstraintName("vuelo_codigoruta_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
