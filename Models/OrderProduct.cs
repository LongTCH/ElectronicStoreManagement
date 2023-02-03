using System;
using System.Collections.Generic;

namespace Models;

public partial class OrderProduct : DomainObject
{
    //public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Number { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
