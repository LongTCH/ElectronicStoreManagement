using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("COMBO_PRODUCT")]
public partial class ComboProduct
{
    [Key]
    public int ComboId { get; set; }

    [Column("ProductID")]
    [StringLength(9)]
    [Unicode(false)]
    public string ProductId { get; set; } = null!;

    public int Number { get; set; }

    [StringLength(50)]
    public string Unit { get; set; } = null!;

    [ForeignKey("ComboId")]
    [InverseProperty("ComboProduct")]
    public virtual Combo Combo { get; set; } = null!;
}
