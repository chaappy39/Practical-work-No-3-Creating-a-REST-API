using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class AgarContext : DbContext
{
    public AgarContext()
    {
    }

    public AgarContext(DbContextOptions<AgarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdOrder> AdOrders { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=PC1;Database=Agar;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AdOrders__3214EC0708D7EAA6");

            entity.Property(e => e.AdCode).HasMaxLength(50);
            entity.Property(e => e.AdCost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AdName).HasMaxLength(100);
            entity.Property(e => e.Firm).HasMaxLength(100);

            entity.HasOne(d => d.AdCodeNavigation).WithMany(p => p.AdOrders)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.AdCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AdOrders__AdCode__47DBAE45");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Registra__3214EC07BAA047BF");

            entity.HasIndex(e => e.Code, "UQ_Code").IsUnique();

            entity.Property(e => e.AdName).HasMaxLength(100);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.PricePerTimeUnit).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
