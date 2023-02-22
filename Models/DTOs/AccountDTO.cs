using System;

namespace Models.DTOs;

public class AccountDTO
{
    public string Id { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public bool Sex { get; set; }
    public string EmailAddress { get; set; } = null!;
    public DateTime Birthday { get; set; }
    public string City { get; set; } = null!;
    public string District { get; set; } = null!;
    public string SubDistrict { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? AvatarPath { get; set; }
}
