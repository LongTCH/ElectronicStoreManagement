using Models;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores.Accounts;
using ViewModels.Stores.Navigations;

namespace ViewModels.ControlViewModels;

public class NavigationBarViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;
    private readonly FloatingNavigationStore _floatingNavigationStore;
    INavigationService _popupListItemNavigationService;
    INavigationService _closePopupListItemNavigationService;
    public ICommand NavigateLoginCommand { get; }
    public ICommand NavigateHomeCommand { get; } 
    public ICommand NavigateAccountCommand { get; }
    public ICommand PopupCommand { get; }
    public bool IsLoggedIn => _accountStore.IsLoggedIn;
    public bool IsLoginShow => !_accountStore.IsLoggedIn;
    public bool IsMenuOpen => !_floatingNavigationStore.IsOpen;
    public bool IsMenuBackOpen => _floatingNavigationStore.IsOpen;
    public NavigationBarViewModel(AccountStore accountStore,
            FloatingNavigationStore floatingNavigationStore,
            INavigationService loginNavigationService,
            INavigationService popupListItemNavigationService,
            INavigationService homeNavigationService,
            INavigationService accountNavigationService,
            INavigationService closePopupListItemNavigationService)
    {
        _accountStore = accountStore;
        _floatingNavigationStore = floatingNavigationStore;
        NavigateLoginCommand = new RelayCommand<object>(_ => loginNavigationService.Navigate());
        NavigateHomeCommand = new RelayCommand<object>(_ => homeNavigationService.Navigate());
        _popupListItemNavigationService = popupListItemNavigationService;
        NavigateAccountCommand = new RelayCommand<object>(_ => accountNavigationService.Navigate());
        _closePopupListItemNavigationService = closePopupListItemNavigationService;
        PopupCommand = new RelayCommand<object>(_ => popupCommand());
        _accountStore.CurrentStoreChanged += OnCurrentAccountChanged;
    }
    private void popupCommand()
    {
        if (!_floatingNavigationStore.IsOpen)
        {
            _popupListItemNavigationService.Navigate();
        }
        else
        {
            _closePopupListItemNavigationService.Navigate();
        }
        OnPropertyChanged(nameof(IsMenuBackOpen));
        OnPropertyChanged(nameof(IsMenuOpen));
    }
    private void OnCurrentAccountChanged()
    {
        OnPropertyChanged(nameof(IsLoggedIn));
        OnPropertyChanged(nameof(IsLoginShow));
        OnPropertyChanged(nameof(IsMenuBackOpen));
        OnPropertyChanged(nameof(IsMenuOpen));
    }

    public override void Dispose()
    {
        _accountStore.CurrentStoreChanged -= OnCurrentAccountChanged;
        base.Dispose();
    }
}
