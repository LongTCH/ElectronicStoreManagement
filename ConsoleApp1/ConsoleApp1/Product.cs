using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Table("PRODUCT")]
public partial class Product
{
    [Key]
    public int Id { get; set; }

    [Column("TypeID")]
    public int TypeId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [Column(TypeName = "money")]
    public decimal? Discount { get; set; }

    public short Remain { get; set; }

    [Column("Detail_Path")]
    [StringLength(200)]
    public string? DetailPath { get; set; }

    [Column("Image_Path")]
    [StringLength(200)]
    public string? ImagePath { get; set; }

    [StringLength(50)]
    public string? Series { get; set; }

    [StringLength(50)]
    public string Company { get; set; } = null!;

    [StringLength(50)]
    public string? Need { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<OrderProduct> OrderProducts { get; } = new List<OrderProduct>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductAttribute> ProductAttributes { get; } = new List<ProductAttribute>();

    [ForeignKey("TypeId")]
    [InverseProperty("Products")]
    public virtual Type Type { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("Products")]
    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
