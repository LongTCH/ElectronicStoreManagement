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
        }
    }
    private string? panel;
    public string? Panel
    {
        get => panel?.Trim();
        set
        {
            panel = value;
        }
    }
    private string? refreshRate;
    public string? RefreshRate
    {
        get => refreshRate?.Trim();
        set
        {
            refreshRate = value;
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
    private string? need;
    public string? Need
    {
        get => need?.Trim();
        set
        {
            need = value;
        }
    }

}
