using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CuentaService.Models;

namespace CuentaService.Repository
{
    public partial class CuentaContext : DbContext
    {
        public CuentaContext()
        {
        }

        public CuentaContext(DbContextOptions<CuentaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cuenta> Cuenta { get; set; } = null!;
        public virtual DbSet<Movimiento> Movimiento { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.Idcuenta);

                entity.ToTable("CUENTA");

                entity.HasComment("Entidad cuenta AH");

                entity.Property(e => e.Idcuenta).HasColumnName("IDCUENTA");

                entity.Property(e => e.Estadocuenta).HasColumnName("ESTADOCUENTA");

                entity.Property(e => e.Identificacioncli)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("IDENTIFICACIONCLI");

                entity.Property(e => e.Numerocuenta)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("NUMEROCUENTA");

                entity.Property(e => e.Saldoinicial)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("SALDOINICIAL");

                entity.Property(e => e.Tipocuenta)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("TIPOCUENTA");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.Idmovimiento);

                entity.ToTable("MOVIMIENTO");

                entity.HasComment("Entidad Movimiento AH");

                entity.HasIndex(e => e.Idcuenta, "MOVIMIENTOS_CUENTA_FK");

                entity.Property(e => e.Idmovimiento).HasColumnName("IDMOVIMIENTO");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Idcuenta).HasColumnName("IDCUENTA");

                entity.Property(e => e.Saldo)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("SALDO");

                entity.Property(e => e.Tipomovimiento)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("TIPOMOVIMIENTO");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("VALOR");

                entity.HasOne(d => d.IdcuentaNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.Idcuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOVIMIEN_MOVIMIENT_CUENTA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
