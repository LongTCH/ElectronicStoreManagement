using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs;

public class LaptopDTO
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Cpu { get; set; } = null!;
    public string Ram { get; set; } = null!;
    public string Storage { get; set; } = null!;
    public string Graphic { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal? Discount { get; set; }
    public short Remain { get; set; }
    public string? DetailPath { get; set; }
    public string? ImagePath { get; set; }
    public string? Series { get; set; }
    public string Company { get; set; } = null!;
    public string? Need { get; set; }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    public override bool Equals(object? obj)
    {
        return Id == (obj as LaptopDTO)?.Id;
    }
}
