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
        }
    }
    private string? ram;
    public string? Ram
    {
        get => ram?.Trim();
        set
        {
            ram = value;
        }
    }
    private string? storage;
    public string? Storage
    {
        get => storage?.Trim();
        set
        {
            storage = value;
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
