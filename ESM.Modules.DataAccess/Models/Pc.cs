namespace ESM.Modules.DataAccess.Models;

public partial class Pc : ProductDTO
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
