namespace ESM.Modules.DataAccess.Models;

public partial class Provider
{
    public int Id { get; set; }

    public string? ProviderName { get; set; }
    public string? Phone { get; set; }
    public string? Website { get; set; }

    public string? Note { get; set; }
    public virtual ICollection<Import> Imports { get; } = new List<Import>();
    public override string ToString()
    {
        return $"{Id} {ProviderName} {Phone} {Website} {Note}";
    }
}
