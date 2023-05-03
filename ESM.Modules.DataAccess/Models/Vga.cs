namespace ESM.Modules.DataAccess.Models;

public partial class Vga : ProductDTO
{
    private string? chip;
    public string? Chip
    {
        get => chip;
        set
        {
            chip = value;
            InMemory = false;
        }
    }
    private string? chipset;
    public string? Chipset
    {
        get => chipset;
        set
        {
            chipset = value;
            InMemory = false;
        }
    }
    private string? vram;
    public string? Vram
    {
        get => vram;
        set
        {
            vram = value;
            InMemory = false;
        }
    }
    private string? gen;
    public string? Gen
    {
        get => gen;
        set
        {
            gen = value;
            InMemory = false;
        }
    }
    private string? serires;
    public string? Series
    {
        get => serires;
        set
        {
            serires = value;
            InMemory = false;
        }
    }

}
