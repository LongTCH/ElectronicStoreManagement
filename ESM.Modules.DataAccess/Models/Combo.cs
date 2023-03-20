using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Models;

[Table("COMBO")]
public partial class Combo
{
    [Key]
    [StringLength(9)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    public double Discount { get; set; }

    [Column("ProductIDList")]
    [StringLength(200)]
    public string ProductIdlist { get; set; } = null!;

    [InverseProperty("Combo")]
    public virtual ICollection<BillCombo> BillCombos { get; } = new List<BillCombo>();
}
