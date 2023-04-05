using System;
using System.Collections.Generic;

namespace ESM.Modules.DataAccess.Models;

public partial class Pccpu : ProductDTO
{

    public string Socket { get; set; } = null!;

    public string? Series { get; set; }

    public string? Need { get; set; }

}
