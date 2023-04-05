using System;
using System.Collections.Generic;

namespace ESM.Modules.DataAccess.Models;

public partial class Bill
{
    public int Id { get; set; }

    public string StaffId { get; set; } = null!;

    public string? CustomerName { get; set; }

    public string? Phone { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public string? SubDistrict { get; set; }

    public string? Street { get; set; }

    public DateTime PurchasedTime { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual ICollection<BillProduct> BillProducts { get; set; } = new List<BillProduct>();

    public virtual Account Staff { get; set; } = null!;
}
