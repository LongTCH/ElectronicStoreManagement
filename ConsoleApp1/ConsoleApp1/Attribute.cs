using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Table("ATTRIBUTE")]
public partial class Attribute
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("Attribute")]
    public virtual ICollection<ProductAttribute> ProductAttributes { get; } = new List<ProductAttribute>();
}
