namespace Models.DTOs;

public class VgaDTO : ProductDTO
{
    public string Chip { get; set; } = null!;
    public string Chipset { get; set; } = null!;
    public string Vram { get; set; } = null!;
    public string Gen { get; set; } = null!;
    public string? Series { get; set; }
    public string? Need { get; set; }
}
