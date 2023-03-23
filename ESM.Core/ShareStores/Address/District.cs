using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESM.Core.ShareStores.Address;

public class District
{
    public string level2_id { get; set; } 
    public string name { get; set; }
    public string type { get; set; }
    public IEnumerable<Sub_district> level3s { get; set; }
    public override string ToString() => this.name;
}
