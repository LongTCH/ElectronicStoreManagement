namespace ESM.Modules.DataAccess.DTOs;

public class PcDTO : ProductDTO
{
    public string Cpu { get; set; } = null!;
    public string? Ram { get; set; }
    public string? Series { get; set; }
    public string? Need { get; set; }
}
