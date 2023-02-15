﻿using System;
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

    [InverseProperty("Order")]
    public virtual ICollection<OrderProduct> OrderProducts { get; } = new List<OrderProduct>();
}
