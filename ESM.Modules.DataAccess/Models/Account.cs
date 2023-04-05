using System;
using System.Collections.Generic;

namespace ESM.Modules.DataAccess.Models;

public partial class Account
{
    public string Id { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public bool Gender { get; set; }

    public string EmailAddress { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string SubDistrict { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? AvatarPath { get; set; }

    public virtual ICollection<BillCombo> BillCombos { get; } = new List<BillCombo>();

    public virtual ICollection<Bill> Bills { get; } = new List<Bill>();

    public virtual ICollection<Import> Imports { get; } = new List<Import>();
}
