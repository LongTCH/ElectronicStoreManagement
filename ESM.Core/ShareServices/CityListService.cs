using ESM.Core.ShareStores.Address;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ESM.Core.ShareServices
{
    public interface ICityListService
    {
        IEnumerable<City>? GetList();
    }
    public class CityListService : ICityListService
    {
        private static string json = Directory.GetParent(
            Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent
            + "\\ESM.Core\\Utilities\\dvhcvn.json";

        public IEnumerable<City>? GetList()
        {
            var list = JsonConvert.DeserializeObject<IEnumerable<City>>(File.ReadAllText(json));
            if (list != null) list = list.OrderBy(c => c.name);
            return list;
        }
    }
}
