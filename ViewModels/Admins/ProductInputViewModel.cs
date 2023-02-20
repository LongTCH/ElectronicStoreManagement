using System;
using System.ComponentModel.DataAnnotations;
using ViewModels.Validators;

namespace ViewModels.Admins;

public abstract class ProductInputViewModel : ViewModelBase
{
    public string Id { get; set; }
    public string Name { get; set; }
    protected decimal price;
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
    public string Company { get; set; }
}
