using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Table("ACCOUNT")]
public partial class Account
{
    [Key]
    public int Id { get; set; }

    [StringLength(128)]
    public string Username { get; set; } = null!;

    [StringLength(128)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(10)]
    public string? Sex { get; set; }

    [StringLength(256)]
    public string EmailAddress { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    [StringLength(50)]
    public string? District { get; set; }

    [Column("Sub-district")]
    [StringLength(50)]
    public string? SubDistrict { get; set; }

    [StringLength(100)]
    public string? Street { get; set; }

    [StringLength(30)]
    public string? Phone { get; set; }

    [StringLength(50)]
    public string Role { get; set; } = null!;

    [Column("Avatar_Path")]
    [StringLength(200)]
    public string? AvatarPath { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Accounts")]
    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
