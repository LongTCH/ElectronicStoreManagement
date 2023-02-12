using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ControlViewModels;

public class LayoutViewModel : ViewModelBase
{
    public LayoutViewModel(ControlBarViewModel controlBarViewModel, 
        NavigationBarViewModel navigationBarViewModel, 
        ViewModelBase contentViewModel)
    {
        ControlBarViewModel = controlBarViewModel;
        NavigationBarViewModel = navigationBarViewModel;
        ContentViewModel = contentViewModel;
    }

    public ControlBarViewModel ControlBarViewModel { get; }
    public NavigationBarViewModel NavigationBarViewModel { get; }
    public ViewModelBase ContentViewModel { get; }


    public override void Dispose()
    {
        NavigationBarViewModel.Dispose();
        ContentViewModel.Dispose();
        ControlBarViewModel.Dispose();
        base.Dispose();
    }
}
