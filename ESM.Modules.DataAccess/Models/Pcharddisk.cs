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
        }
    }
    private string? connect;
    public string? Connect
    {
        get => connect?.Trim();
        set
        {
            connect = value;
        }
    }
    private string? type;
    public string? Type
    {
        get => type?.Trim();
        set
        {
            type = value;
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
