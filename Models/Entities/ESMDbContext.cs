using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

public partial class ESMDbContext : DbContext
{
    public ESMDbContext()
    {
    }

    public ESMDbContext(DbContextOptions<ESMDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Laptop> Laptops { get; set; }

    public virtual DbSet<Monitor> Monitors { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<Pc> Pcs { get; set; }

    public virtual DbSet<Pccpu> Pccpus { get; set; }

    public virtual DbSet<Pcharddisk> Pcharddisks { get; set; }

    public virtual DbSet<Smartphone> Smartphones { get; set; }

    public virtual DbSet<Vga> Vgas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source =.\\sqlexpress; Initial Catalog = ESMDatabase; Integrated Security = True; TrustServerCertificate = True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.Id).IsFixedLength();
        });

        modelBuilder.Entity<Laptop>(entity =>
        {
            entity.Property(e => e.Id).IsFixedLength();
        });

        modelBuilder.Entity<Monitor>(entity =>
        {
            entity.Property(e => e.Id).IsFixedLength();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ORDER__3214EC07887C71B7");

            entity.Property(e => e.PhoneNvarchar30).IsFixedLength();
            entity.Property(e => e.PurchasedTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.StaffId).IsFixedLength();
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.Property(e => e.ProductId).IsFixedLength();

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_PRODUCT_ORDER");
        });

        modelBuilder.Entity<Pc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PC__3214EC077CFC82BD");
        });

        modelBuilder.Entity<Pccpu>(entity =>
        {
            entity.Property(e => e.Id).IsFixedLength();
        });

        modelBuilder.Entity<Pcharddisk>(entity =>
        {
            entity.Property(e => e.Id).IsFixedLength();
        });

        modelBuilder.Entity<Smartphone>(entity =>
        {
            entity.Property(e => e.Id).IsFixedLength();
        });

        modelBuilder.Entity<Vga>(entity =>
        {
            entity.Property(e => e.Id).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
