using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs;

public class PcDTO : ProductDTO
{
    public string Cpu { get; set; } = null!;
    public string? Ram { get; set; }
    public string? Series { get; set; }
    public string? Need { get; set; }
}
