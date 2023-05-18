using Prism.Mvvm;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESM.Modules.DataAccess.Models;

public partial class Combo: BindableBase, IEquatable<Combo>
{
    public string Id { get; set; } = null!;
    private double discount;
    [Range(0, 100, ErrorMessage = "Nhập giá trị từ 0 đến 100")]
    public double Discount
    {
        get => discount;
        set
        {
            discount = value;
            ValidateProperty(value, nameof(Discount));
        }
    }

    public string ProductIdlist { get; set; } = null!; 
    public string Name { get; set; } = null!;
    public string Unit { get; set; } = null!;
    public bool? Status { get; set; } = true;
    public bool DiscountShow => Discount > 0;
    public decimal SellPrice => Price * (1 - (decimal)Discount / 100);
    public virtual ICollection<BillCombo> BillCombos { get; } = new List<BillCombo>();
    [NotMapped]
    public decimal Price { get; set; }
    [NotMapped]
    public int Remain { get; set; }
    [NotMapped]
    public IEnumerable<ProductDTO>? ListProducts { get; set; }

    public bool Equals(Combo? other)
    {
        if (other is null) return false;
        return Id == other.Id;
    }
    void ValidateProperty<TProp>(TProp value, string name)
    {
        Validator.ValidateProperty(value, new ValidationContext(this, null, null)
        {
            MemberName = name
        });
    }
}