using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace Models.DbContexts;

public partial class EsmdatabaseContext : DbContext
{
    public EsmdatabaseContext()
    {
    }

    public EsmdatabaseContext(DbContextOptions<EsmdatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Attribute> Attributes { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("ACCOUNT");

            entity.Property(e => e.Username).HasMaxLength(128);
            entity.Property(e => e.PasswordHash).HasMaxLength(128);
            entity.Property(e => e.AvatarPath)
                .HasMaxLength(200)
                .HasColumnName("Avatar_Path");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.EmailAddress).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValueSql("('Customer')");
            entity.Property(e => e.Sex).HasMaxLength(10);
            entity.Property(e => e.Street).HasMaxLength(100);
            entity.Property(e => e.SubDistrict)
                .HasMaxLength(50)
                .HasColumnName("Sub-district");
        });

        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ATTRIBUT__3214EC074D9AD264");

            entity.ToTable("ATTRIBUTE");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("CART");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.AuthUserId)
                .HasMaxLength(120)
                .HasColumnName("AuthUserID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(20)
                .HasColumnName("ProductID");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Cart)
                .HasForeignKey<Cart>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CART_ACCOUNT");

            entity.HasOne(d => d.Id1).WithOne(p => p.Cart)
                .HasForeignKey<Cart>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CART_PRODUCT");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ORDER__3214EC07752A4260");

            entity.ToTable("ORDER");

            entity.Property(e => e.Cash).HasColumnType("money");
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.PhoneNvarchar30)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Phone nvarchar(30)");
            entity.Property(e => e.ReceivedCity).HasMaxLength(50);
            entity.Property(e => e.ReceivedDistrict).HasMaxLength(50);
            entity.Property(e => e.ReceivedSubDistrict)
                .HasMaxLength(50)
                .HasColumnName("ReceivedSub_district");
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ORDER_PR__3214EC07E038C109");

            entity.ToTable("ORDER_PRODUCT");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_PRODUCT_ORDER");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_PRODUCT_PRODUCT");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT__3214EC07BF8053FD");

            entity.ToTable("PRODUCT");

            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.DetailPath)
                .HasMaxLength(200)
                .HasColumnName("Detail_Path");
            entity.Property(e => e.Discount).HasColumnType("money");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(200)
                .HasColumnName("Image_Path");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Need).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Series).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductAttribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT___3214EC07F69F1420");

            entity.ToTable("PRODUCT_ATTRIBUTE");

            entity.Property(e => e.AttributeId).HasColumnName("AttributeID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Value).HasMaxLength(50);

            entity.HasOne(d => d.Attribute).WithMany(p => p.ProductAttributes)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_ATTRIBUTE_ATTRIBUTE");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAttributes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_ATTRIBUTE_PRODUCT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
