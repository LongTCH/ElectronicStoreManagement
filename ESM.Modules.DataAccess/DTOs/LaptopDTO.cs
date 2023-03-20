namespace ESM.Modules.DataAccess.DTOs;
public class LaptopDTO : ProductDTO
{
    public string Cpu { get; set; } = null!;
    public string Ram { get; set; } = null!;
    public string Storage { get; set; } = null!;
    public string Graphic { get; set; } = null!;
    public string? Series { get; set; }
    public string? Need { get; set; }
}
