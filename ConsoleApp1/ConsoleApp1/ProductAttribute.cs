using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[PrimaryKey("ProductId", "AttributeId")]
[Table("PRODUCT_ATTRIBUTE")]
public partial class ProductAttribute
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [Key]
    [Column("AttributeID")]
    public int AttributeId { get; set; }

    [StringLength(50)]
    public string Value { get; set; } = null!;

    [ForeignKey("AttributeId")]
    [InverseProperty("ProductAttributes")]
    public virtual Attribute Attribute { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ProductAttributes")]
    public virtual Product Product { get; set; } = null!;
}
