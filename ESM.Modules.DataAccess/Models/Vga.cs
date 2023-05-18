using System.Numerics;

namespace ESM.Modules.DataAccess.Models;

public partial class Vga : ProductDTO
{
    private string? chip;
    public string? Chip
    {
        get => chip?.Trim();
        set
        {
            chip = value;
        }
    }
    private string? chipset;
    public string? Chipset
    {
        get => chipset?.Trim();
        set
        {
            chipset = value;
        }
    }
    private string? vram;
    public string? Vram
    {
        get => vram?.Trim();
        set
        {
            vram = value;
        }
    }
    private string? gen;
    public string? Gen
    {
        get => gen?.Trim();
        set
        {
            gen = value;
        }
    }
    private string? serires;
    public string? Series
    {
        get => serires?.Trim();
        set
        {
            serires = value;
        }
    }
}
