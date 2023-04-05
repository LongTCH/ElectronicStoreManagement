using System;
using System.Collections.Generic;

namespace ESM.Modules.DataAccess.Models;

public partial class Combo
{
    public string Id { get; set; } = null!;

    public double Discount { get; set; }

    public string ProductIdlist { get; set; } = null!;

    public virtual ICollection<BillCombo> BillCombos { get; } = new List<BillCombo>();
}
