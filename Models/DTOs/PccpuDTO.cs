using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs;

public class PccpuDTO : IProductDTO
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Socket { get; set; } = null!;
    public decimal Price { get; set; }
    public double? Discount { get; set; }
    public short Remain { get; set; }
    public string? DetailPath { get; set; }
    public string? ImagePath { get; set; }
    public string? Series { get; set; }
    public string Company { get; set; } = null!;
    public string? Need { get; set; }
}
