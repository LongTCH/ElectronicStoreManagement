using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Models;

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

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillCombo> BillCombos { get; set; }

    public virtual DbSet<BillProduct> BillProducts { get; set; }

    public virtual DbSet<Combo> Combos { get; set; }

    public virtual DbSet<Import> Imports { get; set; }

    public virtual DbSet<ImportProduct> ImportProducts { get; set; }

    public virtual DbSet<Laptop> Laptops { get; set; }

    public virtual DbSet<Monitor> Monitors { get; set; }

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

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.Property(e => e.PhoneNvarchar30).IsFixedLength();
            entity.Property(e => e.PurchasedTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.StaffId).IsFixedLength();

            entity.HasOne(d => d.Staff).WithMany(p => p.Bills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BILL_ACCOUNT");
        });

        modelBuilder.Entity<BillCombo>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ComboId).IsFixedLength();
            entity.Property(e => e.PhoneNvarchar30).IsFixedLength();
            entity.Property(e => e.PurchasedTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.StaffId).IsFixedLength();

            entity.HasOne(d => d.Combo).WithMany(p => p.BillCombos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BILL_COMBO_COMBO");

            entity.HasOne(d => d.Staff).WithMany(p => p.BillCombos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BILL_COMBO_ACCOUNT");
        });

        modelBuilder.Entity<BillProduct>(entity =>
        {
            entity.Property(e => e.ProductId).IsFixedLength();

            entity.HasOne(d => d.Bill).WithMany(p => p.BillProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BILL_PRODUCT_BILL");
        });

        modelBuilder.Entity<Combo>(entity =>
        {
            entity.Property(e => e.Id).IsFixedLength();
        });

        modelBuilder.Entity<Import>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.StaffId).IsFixedLength();

            entity.HasOne(d => d.Staff).WithMany(p => p.Imports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMPORT_ACCOUNT");
        });

        modelBuilder.Entity<ImportProduct>(entity =>
        {
            entity.Property(e => e.ProductId).IsFixedLength();

            entity.HasOne(d => d.Import).WithMany(p => p.ImportProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMPORT_PRODUCT_IMPORT");
        });

        modelBuilder.Entity<Laptop>(entity =>
        {
            entity.Property(e => e.Id).IsFixedLength();
            entity.Property(e => e.Unit).IsFixedLength();
        });

        modelBuilder.Entity<Monitor>(entity =>
        {
            entity.Property(e => e.Id).IsFixedLength();
        });

        modelBuilder.Entity<Pc>(entity =>
        {
            entity.Property(e => e.Id).IsFixedLength();
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
