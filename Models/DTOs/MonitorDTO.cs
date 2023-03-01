namespace Models.DTOs;

public class MonitorDTO : ProductDTO
{
    public string Size { get; set; } = null!;
    public string Panel { get; set; } = null!;
    public short RefreshRate { get; set; }
    public string? Series { get; set; }
    public string? Need { get; set; }

}
