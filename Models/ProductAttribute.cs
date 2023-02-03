using System;
using System.Collections.Generic;

namespace Models;

public partial class ProductAttribute : DomainObject
{
    //public int Id { get; set; }

    public int ProductId { get; set; }

    public int AttributeId { get; set; }

    public string Value { get; set; } = null!;

    public virtual Attribute Attribute { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
