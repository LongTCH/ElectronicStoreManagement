using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESM.Core.ShareStores.Address;

public class Sub_district
{
    public string level3_id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public override string ToString() => this.name;
}
