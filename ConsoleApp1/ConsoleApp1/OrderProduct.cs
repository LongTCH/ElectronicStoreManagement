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
    public int ProductId { get; set; }

    public int Number { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderProducts")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderProducts")]
    public virtual Product Product { get; set; } = null!;
}
