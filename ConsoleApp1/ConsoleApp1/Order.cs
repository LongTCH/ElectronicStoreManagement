using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Table("ORDER")]
public partial class Order
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string CustomerName { get; set; } = null!;

    [Column("Phone nvarchar(30)")]
    [StringLength(10)]
    public string PhoneNvarchar30 { get; set; } = null!;

    [StringLength(50)]
    public string ReceivedCity { get; set; } = null!;

    [StringLength(50)]
    public string ReceivedDistrict { get; set; } = null!;

    [Column("ReceivedSub_district")]
    [StringLength(50)]
    public string ReceivedSubDistrict { get; set; } = null!;

    public DateTime ReceivedDate { get; set; }

    [Column(TypeName = "money")]
    public decimal Cash { get; set; }

    public bool Status { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderProduct> OrderProducts { get; } = new List<OrderProduct>();
}
