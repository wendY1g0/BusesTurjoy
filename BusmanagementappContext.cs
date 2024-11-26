using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;  // You will need to install this package via NuGet
using System;
using System.IO;
using System.Linq;


namespace personalMantencion
{
    public partial class BusmanagementappContext : DbContext
    {
        public BusmanagementappContext(DbContextOptions<BusmanagementappContext> options)
            : base(options)
        {
        }

        // DbSets for entities
        public virtual DbSet<Driver> Drivers { get; set; }

        // Configuring the context to use MySQL with a connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3307;database=busmanagementapp;user=alumno;password=pass123");
            }
        }

        // Model configuration (setting up relationships, constraints, indexes, etc.)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Driver>(entity =>
    {
        entity.HasKey(e => e.DriverId).HasName("PRIMARY");

        entity.ToTable("drivers");

        entity.Property(e => e.DriverId).HasColumnType("int(11)");

        // Configure CreatedAt with default value, automatically generated on add
        entity.Property(e => e.CreatedAt)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Remove ValueGeneratedOnAddOrUpdate() for UpdatedAt
        entity.Property(e => e.UpdatedAt)
            .HasColumnType("DATETIME");

        entity.Property(e => e.Name)
            .HasMaxLength(255)
            .IsRequired();

        entity.Property(e => e.IsAvailable)
            .HasDefaultValueSql("'YES'")
            .HasColumnType("varchar(3)");

        entity.HasIndex(d => d.IsAvailable)
            .HasDatabaseName("Index_IsAvailable");
    });

    OnModelCreatingPartial(modelBuilder);
}

        // Partial method for additional custom configurations
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        // Override SaveChanges to automatically update UpdatedAt when a Driver is modified
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Driver && e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                ((Driver)entry.Entity).UpdatedAt = DateTime.Now;
            }

            return base.SaveChanges();
        }
                // Method to export Drivers data to JSON
        public void ExportDriversToJson(string filePath)
        {
            // Retrieve all drivers from the database
            var drivers = Drivers.ToList();

            // Serialize the data to JSON
            var jsonData = JsonConvert.SerializeObject(drivers, Formatting.Indented);

            // Write the data to a file
            File.WriteAllText(filePath, jsonData);

            Console.WriteLine("Data exported to JSON file: " + filePath);
        }
    }
}
