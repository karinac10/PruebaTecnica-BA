using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPI.Models
{
    public partial class DBAPIContext : DbContext
    {
        public DBAPIContext()
        {
        }

        public DBAPIContext(DbContextOptions<DBAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Movimiento> Movimientos { get; set; } = null!;
        public virtual DbSet<Tarjetacredito> Tarjetacreditos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("CLIENTE");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.IdMovimiento)
                    .HasName("PK__MOVIMIEN__2A071C24CE0F63B2");

                entity.ToTable("MOVIMIENTO");

                entity.Property(e => e.IdMovimiento).HasColumnName("id_movimiento");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdTarjeta).HasColumnName("id_tarjeta");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("monto");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipo");

                entity.HasOne(d => d.IdTarjetaNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.IdTarjeta)
                    .HasConstraintName("FK_TARJETA");
            });

            modelBuilder.Entity<Tarjetacredito>(entity =>
            {
                entity.HasKey(e => e.IdTarjeta)
                    .HasName("PK__TARJETAC__E92BCFEAE0955550");

                entity.ToTable("TARJETACREDITO");

                entity.HasIndex(e => e.NumeroTarjeta, "UQ__TARJETAC__90FF5A613D2E6F25")
                    .IsUnique();

                entity.Property(e => e.IdTarjeta).HasColumnName("id_tarjeta");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.InteresBonificable)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("interes_bonificable");

                entity.Property(e => e.LimiteCredito)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("limite_credito");

                entity.Property(e => e.NumeroTarjeta)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("numero_tarjeta");

                entity.Property(e => e.SaldoActual)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("saldo_actual");

                entity.Property(e => e.SaldoDisponible)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("saldo_disponible");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Tarjetacreditos)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__IDCLIENTE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
