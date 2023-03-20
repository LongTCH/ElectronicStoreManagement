namespace ESM.Modules.DataAccess.DTOs;

public class SmartphoneDTO : ProductDTO
{
    public string Cpu { get; set; } = null!;
    public string Ram { get; set; } = null!;
    public string Storage { get; set; } = null!;
    public string? Series { get; set; }
}
