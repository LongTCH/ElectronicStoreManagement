using System;
using System.Collections.Generic;

namespace Models;

public partial class Order : DomainObject
{
    //public int Id { get; set; }

    public string CustomerName { get; set; } = null!;

    public string PhoneNvarchar30 { get; set; } = null!;

    public string ReceivedCity { get; set; } = null!;

    public string ReceivedDistrict { get; set; } = null!;

    public string ReceivedSubDistrict { get; set; } = null!;

    public DateTime ReceivedDate { get; set; }

    public decimal Cash { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; } = new List<OrderProduct>();
}
