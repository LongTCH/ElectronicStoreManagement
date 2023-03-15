using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.MyMessageBox;
using ViewModels.Stores.Address;

namespace ViewModels.Commands;

public class GetCitiesCommand
{
    private static string json = Directory.GetParent(
            Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent
            + "\\ViewModels\\Utilities\\dvhcvn.json";
    public GetCitiesCommand()
    {
    }
    public IEnumerable<City>? GetCitiesList()
    {
        try { 
            return JsonConvert.DeserializeObject<IEnumerable<City>>(File.ReadAllText(json))!; }
        catch
        {
            ErrorNotifyViewModel.Instance!.Show("Can not load address list", "Error");
            return null;
        }
    }
}
