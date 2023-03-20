namespace ESM.Modules.DataAccess.DTOs;

public class PccpuDTO : ProductDTO
{
    public string Socket { get; set; } = null!;
    public string? Series { get; set; }
    public string? Need { get; set; }
}
