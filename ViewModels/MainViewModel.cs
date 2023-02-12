using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Stores;
using ViewModels.Stores.Navigations;

namespace ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly FloatingNavigationStore _floatingNavigationStore;

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel!;
    public ViewModelBase? CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
    public ViewModelBase? FloatViewModel => _floatingNavigationStore.CurrentViewModel;
    public bool IsOpen => _modalNavigationStore.IsOpen;
    public bool IsFloatOpen => _floatingNavigationStore.IsOpen;
    public MainViewModel(NavigationStore navigationStore, 
        ModalNavigationStore modalNavigationStore,
        FloatingNavigationStore floatingNavigationStore)
    {
        _navigationStore = navigationStore;
        _modalNavigationStore = modalNavigationStore;
        _floatingNavigationStore = floatingNavigationStore;
        _navigationStore.CurrentStoreChanged += OnCurrentViewModelChanged;
        _modalNavigationStore.CurrentStoreChanged += OnCurrentModalViewModelChanged;
        _floatingNavigationStore.CurrentStoreChanged += OnFloatViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void OnCurrentModalViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
        OnPropertyChanged(nameof(IsOpen));
    }
    private void OnFloatViewModelChanged()
    {
        OnPropertyChanged(nameof(FloatViewModel));
        OnPropertyChanged(nameof(IsFloatOpen));
    }
}
