﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Models;

[Table("PC")]
public partial class Pc
{
    [Key]
    [StringLength(9)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("CPU")]
    [StringLength(50)]
    public string Cpu { get; set; } = null!;

    [Column("RAM")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Ram { get; set; }

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