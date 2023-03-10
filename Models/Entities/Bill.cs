using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("BILL")]
public partial class Bill
{
    [Key]
    public int Id { get; set; }

    [Column("StaffID")]
    [StringLength(9)]
    [Unicode(false)]
    public string StaffId { get; set; } = null!;

    [StringLength(50)]
    public string? CustomerName { get; set; }

    [Column("Phone nvarchar(30)")]
    [StringLength(10)]
    public string? PhoneNvarchar30 { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    [StringLength(50)]
    public string? District { get; set; }

    [Column("Sub_district")]
    [StringLength(50)]
    public string? SubDistrict { get; set; }

    [StringLength(100)]
    public string? Street { get; set; }

    [Precision(3)]
    public DateTime PurchasedTime { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalAmount { get; set; }

    [InverseProperty("Bill")]
    public virtual ICollection<BillProduct> BillProducts { get; } = new List<BillProduct>();

    [ForeignKey("StaffId")]
    [InverseProperty("Bills")]
    public virtual Account Staff { get; set; } = null!;
}
