using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Models;

[Table("IMPORT")]
public partial class Import
{
    [Key]
    public int Id { get; set; }

    [Column("StaffID")]
    [StringLength(9)]
    [Unicode(false)]
    public string StaffId { get; set; } = null!;

    [StringLength(100)]
    public string Provider { get; set; } = null!;

    [Column("Provider_Bill_ID")]
    [StringLength(100)]
    public string ProviderBillId { get; set; } = null!;

    [StringLength(50)]
    public string City { get; set; } = null!;

    [StringLength(50)]
    public string District { get; set; } = null!;

    [Column("Sub_district")]
    [StringLength(50)]
    public string SubDistrict { get; set; } = null!;

    [StringLength(100)]
    public string Street { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime ImportDate { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalAmount { get; set; }

    [InverseProperty("Import")]
    public virtual ICollection<ImportProduct> ImportProducts { get; } = new List<ImportProduct>();

    [ForeignKey("StaffId")]
    [InverseProperty("Imports")]
    public virtual Account Staff { get; set; } = null!;
}
