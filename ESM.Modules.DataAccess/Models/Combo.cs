using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static Azure.Core.HttpHeader;

namespace ESM.Modules.DataAccess.Models;

public partial class Combo
{
    public string Id { get; set; } = null!;

    public double Discount { get; set; }

    public string ProductIdlist { get; set; } = null!; 
    public string Name { get; set; } = null!;
    public string Unit { get; set; } = null!;
    public bool DiscountShow => Discount > 0;
    public decimal SellPrice => Price * (1 - (decimal)Discount / 100);
    public virtual ICollection<BillCombo> BillCombos { get; } = new List<BillCombo>();
    [NotMapped]
    public decimal Price { get; set; }
    [NotMapped]
    public int Remain { get; set; }
    [NotMapped]
    public IEnumerable<ProductDTO>? ListProducts { get; set; }
}
//public partial class Combo
//{
//    [NotMapped]
//    public decimal SellPrice { get; set; }
//    [NotMapped]
//    public int Remain { get; set; }
//}