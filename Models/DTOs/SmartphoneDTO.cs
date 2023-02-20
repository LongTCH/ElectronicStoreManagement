using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs;

public class SmartphoneDTO : ProductDTO
{
    public string Cpu { get; set; } = null!;
    public string Ram { get; set; } = null!;
    public string Storage { get; set; } = null!;
    public string? Series { get; set; }
}
