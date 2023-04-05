using System;
using System.Collections.Generic;

namespace ESM.Modules.DataAccess.Models;

public partial class Smartphone : ProductDTO
{

    public string Cpu { get; set; } = null!;

    public string Ram { get; set; } = null!;

    public string Storage { get; set; } = null!;

    public string? Series { get; set; }

}
