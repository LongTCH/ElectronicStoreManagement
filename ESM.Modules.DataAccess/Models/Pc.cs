using System;
using System.Collections.Generic;

namespace ESM.Modules.DataAccess.Models;

public partial class Pc : ProductDTO
{

    public string Cpu { get; set; } = null!;

    public string? Ram { get; set; }


    public string? Series { get; set; }


    public string? Need { get; set; }

}
