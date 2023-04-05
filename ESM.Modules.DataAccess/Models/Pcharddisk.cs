using System;
using System.Collections.Generic;

namespace ESM.Modules.DataAccess.Models;

public partial class Pcharddisk : ProductDTO
{

    public string Storage { get; set; } = null!;

    public string Connect { get; set; } = null!;

    public string? Series { get; set; }


    public string? Type { get; set; }
}
