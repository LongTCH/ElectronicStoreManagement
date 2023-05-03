namespace ESM.Modules.DataAccess.Models;

public partial class Smartphone : ProductDTO
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
