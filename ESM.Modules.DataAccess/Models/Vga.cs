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
            InMemory = false;
        }
    }
    private string? chipset;
    public string? Chipset
    {
        get => chipset?.Trim();
        set
        {
            chipset = value;
            InMemory = false;
        }
    }
    private string? vram;
    public string? Vram
    {
        get => vram?.Trim();
        set
        {
            vram = value;
            InMemory = false;
        }
    }
    private string? gen;
    public string? Gen
    {
        get => gen?.Trim();
        set
        {
            gen = value;
            InMemory = false;
        }
    }
    private string? serires;
    public string? Series
    {
        get => serires?.Trim();
        set
        {
            serires = value;
            InMemory = false;
        }
    }
    public override void Copy(ProductDTO o)
    {
        var other = (Vga)o;
        Series = other.Series;
        Gen = other.Gen;
        Chip = other.Chip;
        Chipset = other.Chipset;
        Vram = other.Vram;
        RaisePropertyChanged(nameof(Chip));
        RaisePropertyChanged(nameof(Chipset));
        RaisePropertyChanged(nameof(Vram));
        RaisePropertyChanged(nameof(Gen));
        RaisePropertyChanged(nameof(Series));

        base.Copy(o);
    }
}
