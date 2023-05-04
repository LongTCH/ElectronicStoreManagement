using System;
using System.Collections.Generic;

namespace ESM.Modules.DataAccess.Models;

public partial class Import
{
    public int Id { get; set; }

    public string StaffId { get; set; } = null!;

    public string ProviderBillId { get; set; } = null!;

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string SubDistrict { get; set; } = null!;

    public string Street { get; set; } = null!;

    public DateTime ImportDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int ProviderId { get; set; }

    public virtual ICollection<ImportProduct> ImportProducts { get; set; } = new List<ImportProduct>();

    public virtual Provider Provider { get; set; } = null!;

    public virtual Account Staff { get; set; } = null!;
}
