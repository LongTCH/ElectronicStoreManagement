using Prism.Mvvm;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESM.Modules.DataAccess.Models;

public partial class Discount : BindableBase, IEquatable<Discount>
{
    public int Id { get; set; }

    public string? ProductIdlist { get; set; }
    private double? discount;
    [Range(0, 100, ErrorMessage = "Nhập giá trị từ 0 đến 100")]
    public double? Discount1
    {
        get => discount;
        set
        {
            discount = value;
            ValidateProperty(value, nameof(Discount1));
        }
    }
    private DateTime? startDate;
    public DateTime? StartDate
    {
        get => startDate?.Date;
        set => startDate = value;
    }
    private DateTime? endDate;
    public DateTime? EndDate
    {
        get => endDate?.Date;
        set => endDate = value;
    }
    public string? Name { get; set; }
    [NotMapped]
    public IEnumerable<ProductDTO>? ListProducts { get; set; }

    public bool Equals(Discount? other)
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
