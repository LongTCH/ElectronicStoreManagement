using System.ComponentModel.DataAnnotations.Schema;

namespace ESM.Modules.DataAccess.Models;

public partial class Discount
{
    public int Id { get; set; }

    public string? ProductIdlist { get; set; }

    public double? Discount1 { get; set; }
    private DateTime? startDate;
    public DateTime? StartDate
    {
        get => startDate?.Date;
        set => startDate = value;
    }
    private DateTime? endDate;
    public DateTime? EndDate
    {
        get => endDate?.Date;
        set => endDate = value;
    }
    public string? Name { get; set; }
    [NotMapped]
    public IEnumerable<ProductDTO>? ListProducts { get; set; }
    [NotMapped]
    public bool InMemory { get; set; } = true;
    [NotMapped]
    public bool EditMode { get; set; } = false;
}
