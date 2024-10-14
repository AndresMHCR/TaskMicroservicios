using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PersonaService.Models;

namespace PersonaService.Repository
{
    public partial class PersonaContext : DbContext
    {
        public PersonaContext()
        {
        }

        public PersonaContext(DbContextOptions<PersonaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; } = null!;
        public virtual DbSet<Persona> Persona { get; set; } = null!;

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Idcliente);

                entity.ToTable("CLIENTE");

                entity.HasComment("Modelo Cliente AH");

                entity.HasIndex(e => e.Idpersona, "PERSONA_ES_CLIENTE_FK");

                entity.Property(e => e.Idcliente).HasColumnName("IDCLIENTE");

                entity.Property(e => e.Clienteid)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("CLIENTEID");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("CONTRASENA");

                entity.Property(e => e.Estadocliente).HasColumnName("ESTADOCLIENTE");

                entity.Property(e => e.Idpersona).HasColumnName("IDPERSONA");

                entity.HasOne(d => d.IdpersonaNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Idpersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CLIENTE_PERSONA_E_PERSONA");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.Idpersona);

                entity.ToTable("PERSONA");

                entity.Property(e => e.Idpersona).HasColumnName("IDPERSONA");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.Edad).HasColumnName("EDAD");

                entity.Property(e => e.Genero)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("GENERO");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("IDENTIFICACION");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
