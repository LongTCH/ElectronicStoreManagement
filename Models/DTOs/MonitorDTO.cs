using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs;

public class MonitorDTO : ProductDTO
{
    public string Size { get; set; } = null!;
    public string Panel { get; set; } = null!;
    public short RefreshRate { get; set; }
    public string? Series { get; set; }
    public string? Need { get; set; }

}
