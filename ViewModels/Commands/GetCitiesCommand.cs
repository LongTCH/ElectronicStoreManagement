using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Stores.Address;

namespace ViewModels.Commands;

public class GetCitiesCommand
{
    private static string json = Directory.GetParent(
            Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent
            + "\\ViewModels\\JsonConfig\\dvhcvn.json";
    public GetCitiesCommand()
    {
    }
    public IEnumerable<City> GetCitiesList() => JsonConvert.DeserializeObject<IEnumerable<City>>(File.ReadAllText(json));
}
