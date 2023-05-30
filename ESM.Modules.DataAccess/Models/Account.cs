using Prism.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace ESM.Modules.DataAccess.Models;

public partial class Account : BindableBase
{
    private string id;
    public string Id
    {
        get => id;
        set => SetProperty(ref id, value);
    }

    public string PasswordHash { get; set; } = null!;
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;

    public bool Gender { get; set; }

    public string EmailAddress { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string SubDistrict { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Phone { get; set; } = null!;
    private string? avatarPath;
    public string? AvatarPath
    {
        get => avatarPath;
        set => SetProperty(ref avatarPath, value);
    }
    public override string ToString() => $"{Id} {LastName} {FirstName} {Gender} {EmailAddress} {Phone} {Birthday.ToShortDateString()} {Address}";
    public string Address => $"{City}, {District}, {SubDistrict}, {Street}";

    public virtual ICollection<BillCombo> BillCombos { get; } = new List<BillCombo>();

    public virtual ICollection<Bill> Bills { get; } = new List<Bill>();

    public virtual ICollection<Import> Imports { get; } = new List<Import>();
}
