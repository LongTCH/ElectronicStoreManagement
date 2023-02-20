using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs;

public class PcharddiskDTO :ProductDTO
{
    public string Storage { get; set; } = null!;
    public string Connect { get; set; } = null!;
    public string? Series { get; set; }
    public string? Type { get; set; }
}
