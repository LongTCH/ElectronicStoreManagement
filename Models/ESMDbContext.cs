using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Models;

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

    public virtual DbSet<Attribute> Attributes { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Need> Needs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source =.\\sqlexpress; Initial Catalog = ESMDatabase; Integrated Security = True; TrustServerCertificate = True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Role).HasDefaultValueSql("('Customer')");

            entity.HasMany(d => d.Products).WithMany(p => p.Accounts)
                .UsingEntity<Dictionary<string, object>>(
                    "Cart",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CART_PRODUCT"),
                    l => l.HasOne<Account>().WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CART_ACCOUNT"),
                    j =>
                    {
                        j.HasKey("AccountId", "ProductId");
                        j.ToTable("CART");
                    });
        });

        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ATTRIBUT__3214EC074CEB49B6");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__COMPANY__3214EC07D6C8675E");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Need>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NEED__3214EC07D781739A");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ORDER__3214EC07F09B3388");

            entity.Property(e => e.PhoneNvarchar30).IsFixedLength();
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasOne(d => d.Order).WithMany(p => p.OrderProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_PRODUCT_ORDER");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_PRODUCT_PRODUCT");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT__3214EC07B1639778");

            entity.HasOne(d => d.Company).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_COMPANY");

            entity.HasOne(d => d.Need).WithMany(p => p.Products).HasConstraintName("FK_PRODUCT_NEED");

            entity.HasOne(d => d.Type).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_TYPE");
        });

        modelBuilder.Entity<ProductAttribute>(entity =>
        {
            entity.HasOne(d => d.Attribute).WithMany(p => p.ProductAttributes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_ATTRIBUTE_ATTRIBUTE");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAttributes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_ATTRIBUTE_PRODUCT");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TYPE__3214EC076EE2AFF1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
