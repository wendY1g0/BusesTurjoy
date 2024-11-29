using System;
using Microsoft.EntityFrameworkCore;

namespace appConsola
{
    public partial class TurjoyContext : DbContext
    {
        public TurjoyContext()
        {
        }

        public TurjoyContext(DbContextOptions<TurjoyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bus> Bus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Directly using the hardcoded connection string
                var connectionString = "server=localhost;port=3307;database=busmanagementapp;user=alumno;password=pass123";

                // Check if the connection string is null or empty
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentNullException("connectionString", "Connection string is missing or empty.");
                }

                optionsBuilder.UseMySQL(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bus>(entity =>
            {
                entity.HasKey(e => e.IdBus).HasName("PRIMARY");

                entity.ToTable("bus");

                entity.Property(e => e.IdBus).HasColumnName("idBus");
                entity.Property(e => e.CodigoBus)
                    .HasMaxLength(50)
                    .HasColumnName("codigoBus");
                entity.Property(e => e.EsHabilitado).HasColumnName("esHabilitado");
                entity.Property(e => e.Kilometraje).HasColumnName("kilometraje");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
