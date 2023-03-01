using System.ComponentModel.DataAnnotations;

namespace ViewModels.Validators;

public class PriceValidate : ValidationAttribute
{
    public PriceValidate()
    {
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        decimal price;
        if (decimal.TryParse(value?.ToString(), out price) == false || price < 0) return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        return ValidationResult.Success;
    }
}
