using System;
using System.Collections.Generic;

namespace Models;

public partial class Product : DomainObject
{
    //public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal? Discount { get; set; }

    public short Remain { get; set; }

    public string? DetailPath { get; set; }

    public string? ImagePath { get; set; }

    public string? Series { get; set; }

    public string Company { get; set; } = null!;

    public string? Need { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; } = new List<OrderProduct>();

    public virtual ICollection<ProductAttribute> ProductAttributes { get; } = new List<ProductAttribute>();
}
