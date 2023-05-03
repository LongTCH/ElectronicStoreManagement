namespace ESM.Modules.DataAccess.Models;

public partial class Pccpu : ProductDTO
{
    private string? socket;
    public string? Socket
    {
        get => socket;
        set
        {
            socket = value;
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
