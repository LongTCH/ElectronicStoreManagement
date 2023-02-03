using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels;

public class LayoutViewModel : ViewModelBase
{
    public NavigationBarViewModel NavigationBarViewModel { get; }
    public ViewModelBase ContentViewModel { get; }

    public LayoutViewModel(NavigationBarViewModel navigationBarViewModel, ViewModelBase contentViewModel)
    {
        NavigationBarViewModel = navigationBarViewModel;
        ContentViewModel = contentViewModel;
    }

    public override void Dispose()
    {
        NavigationBarViewModel.Dispose();
        ContentViewModel.Dispose();

        base.Dispose();
    }
}
