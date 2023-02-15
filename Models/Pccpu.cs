using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Table("PCCPU")]
public partial class Pccpu
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Socket { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [Column(TypeName = "money")]
    public decimal? Discount { get; set; }

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
}
