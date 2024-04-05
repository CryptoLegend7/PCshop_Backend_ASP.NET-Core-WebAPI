using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PCShop.Models;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pc> Pcs { get; set; }

    public virtual DbSet<Processor> Processors { get; set; }

    public virtual DbSet<Render> Renders { get; set; }

    public virtual DbSet<StorageType> StorageTypes { get; set; }

    public virtual DbSet<StorageUnit> StorageUnits { get; set; }

    public virtual DbSet<WeightUnit> WeightUnits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=.\\sqlexpress;Initial Catalog=PCShop;user id=sa;password=123!@#QWEqwe;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pc>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.MemoryUnitNavigation).WithMany(p => p.Pcs).HasConstraintName("FK_PC_WeightUnit");
        });

        modelBuilder.Entity<Processor>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Render>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<StorageType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Storage");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<StorageUnit>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Unit).IsFixedLength();
        });

        modelBuilder.Entity<WeightUnit>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Unit).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
