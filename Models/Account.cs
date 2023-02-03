using System;
using System.Collections.Generic;

namespace Models;

public partial class Account : DomainObject
{
    //public int Id { get; set; }

    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Sex { get; set; }

    public string EmailAddress { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public string? SubDistrict { get; set; }

    public string? Street { get; set; }

    public string? Phone { get; set; }

    public string Role { get; set; } = null!;

    public string? AvatarPath { get; set; }

    public virtual Cart? Cart { get; set; }
}
