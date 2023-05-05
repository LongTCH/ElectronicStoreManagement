namespace ESM.Modules.DataAccess.Models;

public partial class Smartphone : ProductDTO
{

    private string? cpu;
    public string? Cpu
    {
        get => cpu?.Trim();
        set
        {
            cpu = value;
            InMemory = false;
        }
    }
    private string? ram;
    public string? Ram
    {
        get => ram?.Trim();
        set
        {
            ram = value;
            InMemory = false;
        }
    }
    private string? storage;
    public string? Storage
    {
        get => storage?.Trim();
        set
        {
            storage = value;
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
    public override void Copy(ProductDTO other)
    {
        var o = (Smartphone)other;
        Cpu = o.Cpu;
        Ram = o.Ram;
        Storage = o.Storage;
        Series = o.Series;
        RaisePropertyChanged(nameof(Cpu));
        RaisePropertyChanged(nameof(Ram));
        RaisePropertyChanged(nameof(Storage));
        RaisePropertyChanged(nameof(Series));
        base.Copy(other);
    }
}
