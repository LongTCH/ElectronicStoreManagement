using System;
using ViewModels.ControlViewModels;
using ViewModels.Stores.Navigations;

namespace ViewModels.Services;

public class LayoutNavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly Func<TViewModel> _createViewModel;
    private readonly Func<NavigationBarViewModel> _createNavigationBarViewModel;
    private readonly Func<ControlBarViewModel> _createControlBarViewModel;

    public LayoutNavigationService(NavigationStore navigationStore, 
        Func<TViewModel> createViewModel, 
        Func<NavigationBarViewModel> createNavigationBarViewModel, 
        Func<ControlBarViewModel> createControlBarViewModel)
    {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
        _createNavigationBarViewModel = createNavigationBarViewModel;
        _createControlBarViewModel = createControlBarViewModel;
    }

    public void Navigate()
    {
        _navigationStore.CurrentViewModel = new LayoutViewModel(_createControlBarViewModel(), _createNavigationBarViewModel(), _createViewModel());
    }
}
