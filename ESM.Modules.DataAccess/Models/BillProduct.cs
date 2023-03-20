using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Models;

[PrimaryKey("BillId", "ProductId")]
[Table("BILL_PRODUCT")]
public partial class BillProduct
{
    [Key]
    [Column("BillID")]
    public int BillId { get; set; }

    [Key]
    [Column("ProductID")]
    [StringLength(9)]
    [Unicode(false)]
    public string ProductId { get; set; } = null!;

    public int Number { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Warranty { get; set; }

    [StringLength(50)]
    public string Unit { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal SellPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [ForeignKey("BillId")]
    [InverseProperty("BillProducts")]
    public virtual Bill Bill { get; set; } = null!;
}
