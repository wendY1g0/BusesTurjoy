using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace personalMantencion
{
    public partial class BusmanagementappContext : DbContext
    {
        public BusmanagementappContext(DbContextOptions<BusmanagementappContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Driver> Drivers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build()
                    .GetConnectionString("BusManagementApp");

                optionsBuilder.UseMySQL(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasKey(e => e.DriverId).HasName("PRIMARY");

                entity.ToTable("drivers");

                entity.Property(e => e.DriverId).HasColumnType("int(11)");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsRequired();
                entity.Property(e => e.IsAvailable)
                    .HasDefaultValue("YES")
                    .HasColumnType("varchar(3)");
                entity.Property(e => e.KilometersDriven)
                    .HasDefaultValue(0)
                    .HasColumnType("int(11)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public void ExportDriversToJson(string filePath)
        {
            var drivers = Drivers.ToList();
            var jsonData = JsonSerializer.Serialize(drivers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonData);
            Console.WriteLine("Data exported to JSON file: " + filePath);
        }
    }
}
