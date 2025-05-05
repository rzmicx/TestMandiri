using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TestMandiri.Data.Models;

namespace TestMandiri.Data.Common;

public partial class TestMandiriContext : DbContext
{
    public TestMandiriContext()
    {
    }

    public TestMandiriContext(DbContextOptions<TestMandiriContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GridUser> GridUsers { get; set; }

    public virtual DbSet<MsdetailUser> MsdetailUsers { get; set; }

    public virtual DbSet<Msitem> Msitems { get; set; }

    public virtual DbSet<Msuser> Msusers { get; set; }

    public virtual DbSet<TrTransaction> TrTransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GridUser>(entity =>
        {
            entity.ToView("grid_user");
        });

        modelBuilder.Entity<MsdetailUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__msdetail__3213E83FCE565020");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.MsdetailUsers).HasConstraintName("FK__msdetail___idUse__398D8EEE");
        });

        modelBuilder.Entity<Msitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__msitem__3213E83F86D4B91D");

            entity.Property(e => e.Id).IsFixedLength();
        });

        modelBuilder.Entity<Msuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__msuser__3213E83F8C69FBC4");
            entity.Property(e => e.Username)
                  .HasMaxLength(50)
                  .IsUnicode(false);

            entity.Property(e => e.Active)
                .HasColumnName("active")
                .HasColumnType("bit")
                .HasDefaultValue(true);
        });

        modelBuilder.Entity<TrTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TrTransa__3213E83F92BC21EA");

            entity.Property(e => e.IdItem).IsFixedLength();

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.TrTransactions).HasConstraintName("FK__TrTransac__idIte__3F466844");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.TrTransactions).HasConstraintName("FK__TrTransac__idUse__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
