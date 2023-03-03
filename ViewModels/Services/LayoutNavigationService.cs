using System;
using ViewModels.ControlViewModels;
using ViewModels.Stores.Navigations;

namespace ViewModels.Services;

public class LayoutNavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly Func<TViewModel> _createViewModel;

    public LayoutNavigationService(NavigationStore navigationStore,
        Func<TViewModel> createViewModel)
    {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
    }

    public void Navigate()
    {
        _navigationStore.CurrentViewModel =  _createViewModel();
    }
}
