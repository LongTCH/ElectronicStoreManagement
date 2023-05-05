namespace ESM.Modules.DataAccess.Models;

public partial class Monitor : ProductDTO
{
    private string? size;
    public string? Size
    {
        get => size?.Trim();
        set
        {
            size = value;
            InMemory = false;
        }
    }
    private string? panel;
    public string? Panel
    {
        get => panel?.Trim();
        set
        {
            panel = value;
            InMemory = false;
        }
    }
    private string? refreshRate;
    public string? RefreshRate
    {
        get => refreshRate?.Trim();
        set
        {
            refreshRate = value;
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
    private string? need;
    public string? Need
    {
        get => need?.Trim();
        set
        {
            need = value;
            InMemory = false;
        }
    }
    public override void Copy(ProductDTO other)
    {
        var o = (Monitor)other;
        Size = o.Size;
        Panel = o.Panel;
        RefreshRate = o.RefreshRate;
        Need = o.Need;
        Series = o.Series;
        RaisePropertyChanged(nameof(Size));
        RaisePropertyChanged(nameof(Panel));
        RaisePropertyChanged(nameof(RefreshRate));
        RaisePropertyChanged(nameof(Need));
        RaisePropertyChanged(nameof(Series));
        base.Copy(other);
    }
}
