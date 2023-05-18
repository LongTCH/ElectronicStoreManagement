namespace ESM.Modules.DataAccess.Models;

public partial class Pccpu : ProductDTO
{
    private string? socket;
    public string? Socket
    {
        get => socket?.Trim();
        set
        {
            socket = value;
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
