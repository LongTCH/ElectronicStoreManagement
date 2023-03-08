using System;

namespace Models.DTOs;

public class ProductBillDTO
{
    public string ProductID { get; set; }
    public int Number { get; set; }
    public decimal SellPrice { get; set; }
    public string Unit { get; set; }
    public decimal Amount => SellPrice * Convert.ToInt32(Number);
    public string? Warranty { get; set; }
}
