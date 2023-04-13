using System;
using System.Collections.Generic;

namespace ESM.Modules.DataAccess.Models;

public partial class Vga : ProductDTO
{

    public string Chip { get; set; } = null!;

    public string Chipset { get; set; } = null!;

    public string Vram { get; set; } = null!;

    public string Gen { get; set; } = null!;

    public string? Series { get; set; }

}
