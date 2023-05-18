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
    private string? detailPath;
    public string? DetailPath
    {
        get => detailPath;
        set => SetProperty(ref detailPath, value);
    }
    private string? imagePath;
    public string? ImagePath
    {
        get => imagePath;
        set => SetProperty(ref imagePath, value);
    }
    private string? avatarPath;
    public string? AvatarPath
    {
        get => avatarPath;
        set
        {
            SetProperty(ref avatarPath, value);
            RaisePropertyChanged(nameof(IsDefault));
        }
    }
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
