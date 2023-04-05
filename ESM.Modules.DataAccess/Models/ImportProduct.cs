using System;
using System.Collections.Generic;

namespace ESM.Modules.DataAccess.Models;

public partial class ImportProduct
{
    public int ImportId { get; set; }

    public string ProductId { get; set; } = null!;

    public int Number { get; set; }

    public string? Warranty { get; set; }

    public string Unit { get; set; } = null!;

    public decimal SellPrice { get; set; }

    public decimal Amount { get; set; }

    public virtual Import Import { get; set; } = null!;
}
