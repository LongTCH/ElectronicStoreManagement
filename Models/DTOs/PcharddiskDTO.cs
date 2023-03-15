namespace Models.DTOs;

public class PcharddiskDTO : ProductDTO
{
    public string Storage { get; set; } = null!;
    public string Connect { get; set; } = null!;
    public string? Series { get; set; }
    public string? Type { get; set; }
}
