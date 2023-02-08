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

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel!;
    public ViewModelBase? CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
    public bool IsOpen => _modalNavigationStore.IsOpen;
    public MainViewModel(NavigationStore navigationStore, 
        ModalNavigationStore modalNavigationStore,
        ModalNavigationStore secondModalNavigationStore)
    {
        _navigationStore = navigationStore;
        _modalNavigationStore = modalNavigationStore;

        _navigationStore.CurrentStoreChanged += OnCurrentViewModelChanged;
        _modalNavigationStore.CurrentStoreChanged += OnCurrentModalViewModelChanged;
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
}
