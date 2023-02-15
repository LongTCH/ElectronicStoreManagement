using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ProductViewModels;

public class LaptopViewModel : ViewModelBase
{
    private readonly DataProvider _dataProvider;
    public ObservableCollection<LaptopDTO>? LaptopList { get; set; }
    public LaptopViewModel(DataProvider dataProvider)
    {
        _dataProvider = dataProvider;
        LaptopList = new ObservableCollection<LaptopDTO>(_dataProvider.GetLaptopList());
    }
}
