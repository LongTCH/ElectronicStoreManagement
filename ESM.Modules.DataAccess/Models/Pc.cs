namespace ESM.Modules.DataAccess.Models;

public partial class Pc : ProductDTO
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
}
