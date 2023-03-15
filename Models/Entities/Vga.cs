using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("VGA")]
public partial class Vga
{
    [Key]
    [StringLength(9)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Chip { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Chipset { get; set; } = null!;

    [Column("VRAM")]
    [StringLength(50)]
    [Unicode(false)]
    public string Vram { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string Gen { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public double? Discount { get; set; }

    public short Remain { get; set; }

    [Column("Detail_Path")]
    [StringLength(200)]
    public string? DetailPath { get; set; }

    [Column("Image_Path")]
    [StringLength(200)]
    public string? ImagePath { get; set; }

    [StringLength(50)]
    public string? Series { get; set; }

    [StringLength(50)]
    public string Company { get; set; } = null!;

    [StringLength(50)]
    public string? Need { get; set; }

    [Column("Avatar_Path")]
    [StringLength(200)]
    public string? AvatarPath { get; set; }

    [StringLength(50)]
    public string Unit { get; set; } = null!;
}
