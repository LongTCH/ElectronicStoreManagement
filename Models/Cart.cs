using System;
using System.Collections.Generic;

namespace Models;

public partial class Cart : DomainObject
{
    //public int Id { get; set; }

    public string AuthUserId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public virtual Product Id1 { get; set; } = null!;

    public virtual Account IdNavigation { get; set; } = null!;
}
