using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Stores.Address;

namespace ViewModels.Commands;

public class GetDistrictsCommand
{
    private readonly City _city;
    public GetDistrictsCommand(City selectedCity)
    {
        _city = selectedCity;
    }
    public IEnumerable<District> GetDistrictsList() => _city.level2s;
}
