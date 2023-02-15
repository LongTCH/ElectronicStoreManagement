using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[PrimaryKey("OrderId", "ProductId")]
[Table("ORDER_PRODUCT")]
public partial class OrderProduct
{
    [Key]
    [Column("OrderID")]
    public int OrderId { get; set; }

    [Key]
    [Column("ProductID")]
    [StringLength(9)]
    [Unicode(false)]
    public string ProductId { get; set; } = null!;

    public int Number { get; set; }

    [Column("Warranty: nvarchar(50)")]
    [StringLength(50)]
    public string? WarrantyNvarchar50 { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderProducts")]
    public virtual Order Order { get; set; } = null!;
}
