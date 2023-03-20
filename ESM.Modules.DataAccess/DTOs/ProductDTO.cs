namespace ESM.Modules.DataAccess;

public abstract class ProductDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public double? Discount { get; set; }
    public short Remain { get; set; }
    public string? DetailPath { get; set; }
    public string? ImagePath { get; set; }
    public string? AvatarPath { get; set; }
    public string Company { get; set; }
    public decimal SellPrice => Discount == null || Discount == 0 ? Price : Price * (1 - (decimal)Discount / 100);
    public bool DiscountShow => SellPrice < Price;
    public string Unit { get; set; }
}
