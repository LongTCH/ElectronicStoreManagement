using System.ComponentModel.DataAnnotations.Schema;

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
    [NotMapped]
    public bool InMemory { get; set; } = true;
    [NotMapped]
    public bool EditMode { get; set; } = false;
}