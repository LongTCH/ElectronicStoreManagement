using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("ACCOUNT")]
[Index("Id", Name = "IX_ACCOUNT_Id")]
public partial class Account
{
    [Key]
    [StringLength(9)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(128)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    public bool Gender { get; set; }

    [StringLength(256)]
    public string EmailAddress { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime Birthday { get; set; }

    [StringLength(50)]
    public string City { get; set; } = null!;

    [StringLength(50)]
    public string District { get; set; } = null!;

    [Column("Sub-district")]
    [StringLength(50)]
    public string SubDistrict { get; set; } = null!;

    [StringLength(100)]
    public string Street { get; set; } = null!;

    [StringLength(30)]
    public string Phone { get; set; } = null!;

    [Column("Avatar_Path")]
    [StringLength(200)]
    public string? AvatarPath { get; set; }

    [InverseProperty("Staff")]
    public virtual ICollection<Bill> Bills { get; } = new List<Bill>();

    [InverseProperty("Staff")]
    public virtual ICollection<Import> Imports { get; } = new List<Import>();
}
