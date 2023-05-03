using Prism.Mvvm;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESM.Modules.DataAccess;

public abstract class ProductDTO : BindableBase, IEquatable<ProductDTO>
{
    public string Id { get; set; }
    public string? name;
    public string? Name
    {
        get => name;
        set
        {
            name = value;
            InMemory = false;
        }
    }
    public decimal price;
    public decimal Price
    {
        get => price;
        set
        {
            price = value;
            InMemory = false;
        }
    }
    [NotMapped]
    public double? Discount { get; set; }
    public int Remain { get; set; }
    public string? detailPath;
    public string? DetailPath
    {
        get => detailPath;
        set
        {
            SetProperty(ref detailPath, value);
            InMemory = false;
        }
    }
    public string? imagePath;
    public string? ImagePath
    {
        get => imagePath;
        set
        {
            SetProperty(ref imagePath, value);
            InMemory = false;
        }
    }
    private string? _avatarPath;
    public string? AvatarPath
    {
        get => _avatarPath;
        set
        {
            SetProperty(ref _avatarPath, value);
            RaisePropertyChanged(nameof(IsDefault));
            InMemory = false;
        }
    }
    private string? company;
    public string? Company
    {
        get => company;
        set
        {
            company = value;
            InMemory = false;
        }
    }
    public decimal SellPrice => Discount == null || Discount == 0 ? Price : Price * (1 - (decimal)Discount / 100);
    public bool DiscountShow => SellPrice < Price;
    public bool IsDefault => AvatarPath == null;
    private string? unit;
    public string? Unit
    {
        get => unit;
        set
        {
            unit = value;
            InMemory = false;
        }
    }
    private bool inMemory;
    [NotMapped]
    public bool InMemory
    {
        get => inMemory && IdExist;
        set
        {
            SetProperty(ref inMemory, value);
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
