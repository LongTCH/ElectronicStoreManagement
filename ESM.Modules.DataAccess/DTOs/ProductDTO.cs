using Prism.Mvvm;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESM.Modules.DataAccess;

public abstract class ProductDTO : BindableBase, IEquatable<ProductDTO>
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public decimal price;
    public decimal Price
    {
        get => price;
        set
        {
            price = value;
            ValidateProperty(value, nameof(Price));
        }
    }
    [NotMapped]
    public double? Discount { get; set; }
    public int Remain { get; set; }
    public string? DetailPath { get; set; }
    public string? ImagePath { get; set; }
    public string? AvatarPath { get; set; }
    private string? company;
    public string? Company
    {
        get => company?.Trim();
        set
        {
            company = value;
        }
    }
    public decimal SellPrice => Discount == null || Discount == 0 ? Price : Price * (1 - (decimal)Discount / 100);
    public bool DiscountShow => SellPrice < Price;
    public bool IsDefault => AvatarPath == null;
    private string? unit;
    public string? Unit
    {
        get => unit?.Trim();
        set
        {
            unit = value;
        }
    }
    [NotMapped]
    public bool IdExist { get; set; } = true;
    public override bool Equals(object? obj)
    {
        return Id == (obj as ProductDTO)?.Id;
    }

    public bool Equals(ProductDTO? other)
    {
        if (other == null) return false;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    void ValidateProperty<TProp>(TProp value, string name)
    {
        Validator.ValidateProperty(value, new ValidationContext(this, null, null)
        {
            MemberName = name
        });
    }
}
