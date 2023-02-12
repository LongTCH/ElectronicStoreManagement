using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Stores.Navigations;

namespace ViewModels.Services;

public class FloatingNavigationService<TViewModel> : INavigationService
    where TViewModel : ViewModelBase
{
    private readonly FloatingNavigationStore _floatNavigationStore;
    private readonly Func<TViewModel> _createViewModel;

    public FloatingNavigationService(FloatingNavigationStore navigationStore, Func<TViewModel> createViewModel)
    {
        _floatNavigationStore = navigationStore;
        _createViewModel = createViewModel;
    }

    public void Navigate()
    {
        _floatNavigationStore.CurrentViewModel = _createViewModel();
    }
}
