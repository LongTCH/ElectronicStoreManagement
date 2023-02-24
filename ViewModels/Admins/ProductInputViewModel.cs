using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Admins;

public class ProductInputViewModel : ViewModelBase
{
    public HardDiskNhapLieuViewModel HardDisk { get; }
    public MonitorNhapLieuViewModel Monitor { get; }
    public ProductInputViewModel(
        HardDiskNhapLieuViewModel hardDisk,
        MonitorNhapLieuViewModel monitor)
    {
        _unitOfWork = unitOfWork;
    }

    protected string? id;
    [Required]
    [RegularExpression(@"\d{9}", ErrorMessage = "ID must contain 9 digital characters")]
    public string? Id
    {
        get => id; set
        {
            id= value;
            ValidateProperty(value, nameof(Id));
        }
    }
    public string? Name { get; set; }
    protected decimal price;
    [Required]
    [PriceValidate(ErrorMessage = "Invalid Input")]
    public decimal Price
    {
        get => price;
        set
        {
            price = value;
            ValidateProperty(value, nameof(Price));
        }
    }
    protected double? discount;
    [Range(0, 100)]
    public double? Discount
    {
        get => discount;
        set
        {
            discount = value;
            ValidateProperty(value, nameof(Discount));
        }
    }
    public string? DetailPath { get; set; }
    public string? ImagePath { get; set; }
    public string? AvatarPath { get; set; }
    public string? Company { get; set; }
    protected virtual void saveCommand()
    {
        if (Id == null || Company == null)
        {
            ErrorNotifyViewModel.Instance!.Show("Enter all required value", "Warning");
            return;
        }
    }
}
