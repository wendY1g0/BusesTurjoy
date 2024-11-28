using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace buses;

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
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=turjoy;user=turjoyUsr;password=pass123");

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
