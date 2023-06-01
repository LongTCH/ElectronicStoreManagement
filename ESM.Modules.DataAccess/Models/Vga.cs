namespace ESM.Modules.DataAccess.Models;

public partial class Vga : ProductDTO
{
    public string? Chip { get; set; }
    public string? Chipset
    {
        get; set;
    }
    public string? Vram
    {
        get; set;
    }
    public string? Gen
    {
        get; set;
    }
    public string? Series
    {
        get; set;
    }
}
