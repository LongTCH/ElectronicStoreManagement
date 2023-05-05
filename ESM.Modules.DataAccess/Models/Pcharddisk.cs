namespace ESM.Modules.DataAccess.Models;

public partial class Pcharddisk : ProductDTO
{

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
    private string? connect;
    public string? Connect
    {
        get => connect?.Trim();
        set
        {
            connect = value;
            InMemory = false;
        }
    }
    private string? type;
    public string? Type
    {
        get => type?.Trim();
        set
        {
            type = value;
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
        var o =(Pcharddisk)other;
        Storage = o.Storage;
        Connect = o.Connect;
        Type = o.Type;
        Series = o.Series;
        RaisePropertyChanged(nameof(Storage));
        RaisePropertyChanged(nameof(Connect));
        RaisePropertyChanged(nameof(Type));
        RaisePropertyChanged(nameof(Series));
        base.Copy(other);
    }
}
