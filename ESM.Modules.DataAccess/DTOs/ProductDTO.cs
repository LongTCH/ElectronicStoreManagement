using System.ComponentModel.DataAnnotations.Schema;

namespace ESM.Modules.DataAccess;

public abstract class ProductDTO
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    [NotMapped]
    public double? Discount { get; set; }
    public int Remain { get; set; }
    public string? DetailPath { get; set; }
    public string? ImagePath { get; set; }
    public string? AvatarPath { get; set; }
    public string? Company { get; set; }
    public decimal SellPrice => Discount == null || Discount == 0 ? Price : Price * (1 - (decimal)Discount / 100);
    public bool DiscountShow => SellPrice < Price;
    public bool IsDefault => AvatarPath == null;
    public string? Unit { get; set; }

}
