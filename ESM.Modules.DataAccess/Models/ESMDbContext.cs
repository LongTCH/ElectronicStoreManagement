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

    public virtual DbSet<Discount> Discounts { get; set; }

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
            entity.ToTable("ACCOUNT");

            entity.HasIndex(e => e.Id, "IX_ACCOUNT_Id");

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AvatarPath)
                .HasMaxLength(200)
                .HasColumnName("Avatar_Path");
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.EmailAddress).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(128);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.Street).HasMaxLength(100);
            entity.Property(e => e.SubDistrict)
                .HasMaxLength(50)
                .HasColumnName("Sub-district");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.ToTable("BILL");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.PurchasedTime)
                .HasPrecision(3)
                .HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.StaffId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("StaffID");
            entity.Property(e => e.Street).HasMaxLength(100);
            entity.Property(e => e.SubDistrict)
                .HasMaxLength(50)
                .HasColumnName("Sub_district");
            entity.Property(e => e.TotalAmount).HasColumnType("money");

            entity.HasOne(d => d.Staff).WithMany(p => p.Bills)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BILL_ACCOUNT");
        });

        modelBuilder.Entity<BillCombo>(entity =>
        {
            entity.ToTable("BILL_COMBO");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.ComboId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ComboID");
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.Number).HasDefaultValueSql("((1))");
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.PurchasedTime)
                .HasPrecision(3)
                .HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.StaffId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("StaffID");
            entity.Property(e => e.Street).HasMaxLength(100);
            entity.Property(e => e.SubDistrict)
                .HasMaxLength(50)
                .HasColumnName("Sub_district");
            entity.Property(e => e.TotalAmount).HasColumnType("money");

            entity.HasOne(d => d.Combo).WithMany(p => p.BillCombos)
                .HasForeignKey(d => d.ComboId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BILL_COMBO_COMBO");

            entity.HasOne(d => d.Staff).WithMany(p => p.BillCombos)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BILL_COMBO_ACCOUNT");
        });

        modelBuilder.Entity<BillProduct>(entity =>
        {
            entity.HasKey(e => new { e.BillId, e.ProductId });

            entity.ToTable("BILL_PRODUCT");

            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ProductID");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.SellPrice).HasColumnType("money");
            entity.Property(e => e.Unit).HasMaxLength(50);
            entity.Property(e => e.Warranty)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Bill).WithMany(p => p.BillProducts)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BILL_PRODUCT_BILL");
        });

        modelBuilder.Entity<Combo>(entity =>
        {
            entity.ToTable("COMBO");

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ProductIdlist)
                .HasMaxLength(200)
                .HasColumnName("ProductIDList");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.ToTable("DISCOUNT");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Discount1).HasColumnName("Discount");
            entity.Property(e => e.EndDate).HasPrecision(3);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.ProductIdlist)
                .HasMaxLength(200)
                .HasColumnName("ProductIDList");
            entity.Property(e => e.StartDate).HasPrecision(3);
        });

        modelBuilder.Entity<Import>(entity =>
        {
            entity.ToTable("IMPORT");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.ImportDate).HasColumnType("date");
            entity.Property(e => e.Provider).HasMaxLength(100);
            entity.Property(e => e.ProviderBillId)
                .HasMaxLength(100)
                .HasColumnName("Provider_Bill_ID");
            entity.Property(e => e.StaffId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("StaffID");
            entity.Property(e => e.Street).HasMaxLength(100);
            entity.Property(e => e.SubDistrict)
                .HasMaxLength(50)
                .HasColumnName("Sub_district");
            entity.Property(e => e.TotalAmount).HasColumnType("money");

            entity.HasOne(d => d.Staff).WithMany(p => p.Imports)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMPORT_ACCOUNT");
        });

        modelBuilder.Entity<ImportProduct>(entity =>
        {
            entity.HasKey(e => new { e.ImportId, e.ProductId });

            entity.ToTable("IMPORT_PRODUCT");

            entity.Property(e => e.ImportId).HasColumnName("ImportID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ProductID");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.SellPrice).HasColumnType("money");
            entity.Property(e => e.Unit).HasMaxLength(50);
            entity.Property(e => e.Warranty)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Import).WithMany(p => p.ImportProducts)
                .HasForeignKey(d => d.ImportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMPORT_PRODUCT_IMPORT");
        });

        modelBuilder.Entity<Laptop>(entity =>
        {
            entity.ToTable("LAPTOP");

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AvatarPath)
                .HasMaxLength(200)
                .HasColumnName("Avatar_Path");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Cpu)
                .HasMaxLength(50)
                .HasColumnName("CPU");
            entity.Property(e => e.DetailPath)
                .HasMaxLength(200)
                .HasColumnName("Detail_Path");
            entity.Property(e => e.Graphic)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImagePath)
                .HasMaxLength(200)
                .HasColumnName("Image_Path");
            entity.Property(e => e.Name).HasMaxLength(300);
            entity.Property(e => e.Need).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Ram)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RAM");
            entity.Property(e => e.Series).HasMaxLength(50);
            entity.Property(e => e.Storage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Monitor>(entity =>
        {
            entity.ToTable("MONITOR");

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AvatarPath)
                .HasMaxLength(200)
                .HasColumnName("Avatar_Path");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.DetailPath)
                .HasMaxLength(200)
                .HasColumnName("Detail_Path");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(200)
                .HasColumnName("Image_Path");
            entity.Property(e => e.Name).HasMaxLength(300);
            entity.Property(e => e.Need).HasMaxLength(200);
            entity.Property(e => e.Panel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.RefreshRate).HasMaxLength(50);
            entity.Property(e => e.Series).HasMaxLength(50);
            entity.Property(e => e.Size)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<Pc>(entity =>
        {
            entity.ToTable("PC");

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AvatarPath)
                .HasMaxLength(200)
                .HasColumnName("Avatar_Path");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Cpu)
                .HasMaxLength(50)
                .HasColumnName("CPU");
            entity.Property(e => e.DetailPath)
                .HasMaxLength(200)
                .HasColumnName("Detail_Path");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(200)
                .HasColumnName("Image_Path");
            entity.Property(e => e.Name).HasMaxLength(300);
            entity.Property(e => e.Need).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Ram)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RAM");
            entity.Property(e => e.Series).HasMaxLength(50);
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<Pccpu>(entity =>
        {
            entity.ToTable("PCCPU");

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AvatarPath)
                .HasMaxLength(200)
                .HasColumnName("Avatar_Path");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.DetailPath)
                .HasMaxLength(200)
                .HasColumnName("Detail_Path");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(200)
                .HasColumnName("Image_Path");
            entity.Property(e => e.Name).HasMaxLength(300);
            entity.Property(e => e.Need).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Series).HasMaxLength(50);
            entity.Property(e => e.Socket)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<Pcharddisk>(entity =>
        {
            entity.ToTable("PCHARDDISK");

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AvatarPath)
                .HasMaxLength(200)
                .HasColumnName("Avatar_Path");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Connect)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DetailPath)
                .HasMaxLength(200)
                .HasColumnName("Detail_Path");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(200)
                .HasColumnName("Image_Path");
            entity.Property(e => e.Name).HasMaxLength(300);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Series).HasMaxLength(50);
            entity.Property(e => e.Storage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<Smartphone>(entity =>
        {
            entity.ToTable("SMARTPHONE");

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AvatarPath)
                .HasMaxLength(200)
                .HasColumnName("Avatar_Path");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Cpu)
                .HasMaxLength(50)
                .HasColumnName("CPU");
            entity.Property(e => e.DetailPath)
                .HasMaxLength(200)
                .HasColumnName("Detail_Path");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(200)
                .HasColumnName("Image_Path");
            entity.Property(e => e.Name).HasMaxLength(300);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Ram)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RAM");
            entity.Property(e => e.Series).HasMaxLength(50);
            entity.Property(e => e.Storage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<Vga>(entity =>
        {
            entity.ToTable("VGA");

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AvatarPath)
                .HasMaxLength(200)
                .HasColumnName("Avatar_Path");
            entity.Property(e => e.Chip)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Chipset)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.DetailPath)
                .HasMaxLength(200)
                .HasColumnName("Detail_Path");
            entity.Property(e => e.Gen)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ImagePath)
                .HasMaxLength(200)
                .HasColumnName("Image_Path");
            entity.Property(e => e.Name).HasMaxLength(300);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Series).HasMaxLength(50);
            entity.Property(e => e.Unit).HasMaxLength(50);
            entity.Property(e => e.Vram)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VRAM");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
