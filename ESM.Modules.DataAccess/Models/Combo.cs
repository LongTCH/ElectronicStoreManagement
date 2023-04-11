using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESM.Modules.DataAccess.Models;

public partial class Combo
{
    public string Id { get; set; } = null!;

    public double Discount { get; set; }

    public string ProductIdlist { get; set; } = null!; 
    public string Name { get; set; } = null!;
    public string Unit { get; set; } = null!;

    public virtual ICollection<BillCombo> BillCombos { get; } = new List<BillCombo>();
}
public partial class Combo
{
    [NotMapped]
    public decimal SellPrice { get; set; }
    [NotMapped]
    public int Remain { get; set; }
}