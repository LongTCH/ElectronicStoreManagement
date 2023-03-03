using ViewModels.ControlViewModels;
using ViewModels.Stores.Navigations;

namespace ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly ModalNavigationStore _modalNavigationStore;

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel!;
    public ViewModelBase? CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
    public ViewModelBase? ControlBarVM { get; }
    public ViewModelBase? NavigationBarVM { get; }
    public ViewModelBase? CurrentTopLevelViewModel => TopLevelStore.Instance!.CurrentViewModel;
    public bool IsOpen => _modalNavigationStore.IsOpen;
    public bool IsTopLevelOpen => TopLevelStore.Instance!.IsOpen;
    public MainViewModel(NavigationStore navigationStore, 
        ModalNavigationStore modalNavigationStore,
        NavigationBarViewModel navigationBarVM,
        ControlBarViewModel controlBarViewModel)
    {
        _navigationStore = navigationStore;
        _modalNavigationStore = modalNavigationStore;
        _navigationStore.CurrentStoreChanged += OnCurrentViewModelChanged;
        _modalNavigationStore.CurrentStoreChanged += OnCurrentModalViewModelChanged;
        TopLevelStore.Instance!.CurrentStoreChanged += OnTopLevelViewModelChanged;
        NavigationBarVM = navigationBarVM;
        ControlBarVM = controlBarViewModel;
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
    private void OnTopLevelViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentTopLevelViewModel));
        OnPropertyChanged(nameof(IsTopLevelOpen));
    }
}
