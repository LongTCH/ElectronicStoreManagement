using System;
using System.Collections.Generic;

namespace Models;

public partial class Attribute : DomainObject
{
    //public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductAttribute> ProductAttributes { get; } = new List<ProductAttribute>();
}
