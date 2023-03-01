using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("COMBO")]
public partial class Combo
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "money")]
    public decimal SellPrice { get; set; }

    [InverseProperty("Combo")]
    public virtual ComboProduct? ComboProduct { get; set; }
}
