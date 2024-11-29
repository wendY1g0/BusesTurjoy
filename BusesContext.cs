using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Buses.Models;


namespace Buses
{
        
    public partial class BusesContext : DbContext
    {
        public BusesContext()
        {
        }

        public BusesContext(DbContextOptions<BusesContext> options)
            : base(options)
        {
        }
        public DbSet<RegistroViaje> RegistroViajes { get; set; }
        public virtual DbSet<Tramo> Tramos { get; set; }

        public virtual DbSet<Chofer> Choferes { get; set; }

        public virtual DbSet<Bus> Buses { get; set; }
        public virtual DbSet<Viaje> Viajes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseMySql("Server=localhost;database=buses;user=usr_buses;password=pass123",new MySqlServerVersion(new Version(8, 0, 37)));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bus>(entity =>
            {
                entity.HasKey(e => e.IdBus).HasName("PRIMARY");
                entity.ToTable("buses");

                entity.Property(e => e.IdBus).HasColumnName("IdBus");
                entity.Property(e => e.CodigoBus)
                    .HasMaxLength(50)
                    .HasColumnName("CodigoBus");
                entity.Property(e => e.EsHabilitado).HasColumnName("EsHabilitado");
                entity.Property(e => e.Kilometraje).HasColumnName("Kilometraje");
                
            });

            modelBuilder.Entity<Chofer>(entity =>
            {
                entity.HasKey(e => e.IdChofer).HasName("PRIMARY");
                entity.ToTable("chofer");
                entity.Property(e => e.IdChofer).HasColumnName("IdChofer");
                entity.Property(e => e.Rut).HasMaxLength(50).HasColumnName("Rut");
                entity.Property(e => e.Nombre).HasMaxLength(50).HasColumnName("Nombre");
                entity.Property(e => e.Kilometraje).HasColumnName("Kilometraje");
                entity.Property(e => e.EsHabilitado).HasColumnName("EsHabilitado");
            });
            modelBuilder.Entity<Tramo>(entity =>
            {
                entity.HasKey(e => e.IdTramo).HasName("PRIMARY");
                entity.ToTable("tramo");
                entity.Property(e => e.IdTramo).HasColumnName("IdTramo");
                entity.Property(e => e.Origen).HasMaxLength(50).HasColumnName("Origen");
                entity.Property(e => e.Destino).HasMaxLength(50).HasColumnName("Destino");
                entity.Property(e => e.Distancia).HasColumnName("Distancia");
            });
            modelBuilder.Entity<Viaje>(entity =>
            {
                entity.HasKey(e => e.IdViaje).HasName("PRIMARY");
                entity.ToTable("viaje");

                entity.Property(e => e.IdViaje).HasColumnName("IdViaje");
                entity.Property(e => e.Fecha).HasColumnName("Fecha");

                entity.HasOne(v => v.Tramo)
                    .WithMany()
                    .HasForeignKey(v => v.TramoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Viaje_Tramo");

                entity.HasOne(v => v.Bus)
                    .WithMany()
                    .HasForeignKey(v => v.BusId) 
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Viaje_Bus");

                entity.HasOne(v => v.Chofer)
                    .WithMany()
                    .HasForeignKey(v => v.ChoferId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Viaje_Chofer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
