namespace ESM.Modules.DataAccess.Models;

public partial class Laptop : ProductDTO
{
    private string? cpu;
    public string? Cpu
    {
        get => cpu;
        set
        {
            cpu = value;
            InMemory = false;
        }
    }
    private string? ram;
    public string? Ram
    {
        get => ram;
        set
        {
            ram = value;
            InMemory = false;
        }
    }
    private string? storage;
    public string? Storage
    {
        get => storage;
        set
        {
            storage = value;
            InMemory = false;
        }
    }
    private string? graphic;
    public string? Graphic
    {
        get => graphic;
        set
        {
            graphic = value;
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
    private string? need;
    public string? Need
    {
        get => need;
        set
        {
            need = value;
            InMemory = false;
        }
    }
    public override void Copy(ProductDTO other)
    {
        var o = (Laptop)other;
        Cpu = o.Cpu;
        Ram = o.Ram;
        Storage = o.Storage;
        Graphic = o.Graphic;
        Series = o.Series;
        Need = o.Need;
        RaisePropertyChanged(nameof(Cpu));
        RaisePropertyChanged(nameof(Ram));
        RaisePropertyChanged(nameof(Storage));
        RaisePropertyChanged(nameof(Graphic));
        RaisePropertyChanged(nameof(Series));
        RaisePropertyChanged(nameof(Need));
        base.Copy(other);
    }
}
