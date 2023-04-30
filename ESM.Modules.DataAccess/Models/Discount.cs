using System;
using System.Collections.Generic;

namespace ESM.Modules.DataAccess.Models;

public partial class Discount
{
    public int Id { get; set; }

    public string? ProductIdlist { get; set; }

    public double? Discount1 { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
