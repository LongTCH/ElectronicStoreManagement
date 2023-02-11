using System.Collections.Generic;

namespace ViewModels.Stores.Address;

public class City
{
    public string level1_id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public IEnumerable<District> level2s;
    public override string ToString()
    {
        if (this.name.StartsWith("Tỉnh"))
        {
            return this.name[string.Format("Tỉnh ").Length..];
        }
        else return this.name[string.Format("Thành phố ").Length..];
    }
}
