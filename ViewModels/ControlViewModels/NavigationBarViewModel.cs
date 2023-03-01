using Models;
using System.Threading;
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
    private readonly PopupListItemViewModel _popup;
    INavigationService _popupListItemNavigationService;
    INavigationService _closePopupListItemNavigationService;
    public ICommand NavigateLoginCommand { get; }
    public ICommand NavigateHomeCommand { get; }
    public ICommand NavigateAccountCommand { get; }
    public ICommand PopupCommand { get; }
    public ICommand LogoutCommand { get; }
    public ICommand Test { get; }
    public bool IsLoggedIn => _accountStore.IsLoggedIn;
    public bool IsMenuOpen => _floatingNavigationStore.IsOpen;
    public bool IsAdmin => _accountStore.IsAdmin;
    public bool IsSellStaff => _accountStore.IsSellStaff || IsAdmin;
    public bool IsTypingStaff => _accountStore.IsTypingStaff || IsAdmin;
    public NavigationBarViewModel(AccountStore accountStore,
            FloatingNavigationStore floatingNavigationStore,
            INavigationService loginNavigationService,
            INavigationService popupListItemNavigationService,
            INavigationService homeNavigationService,
            INavigationService accountNavigationService,
            INavigationService closePopupListItemNavigationService,
            INavigationService test)
    {
        _accountStore = accountStore;
        _floatingNavigationStore = floatingNavigationStore;
        NavigateLoginCommand = new RelayCommand<object>(_ => loginNavigationService.Navigate());
        NavigateHomeCommand = new RelayCommand<object>(_ => homeNavigationService.Navigate());
        _popupListItemNavigationService = popupListItemNavigationService;
        NavigateAccountCommand = new RelayCommand<object>(_ => accountNavigationService.Navigate());
        PopupCommand = new RelayCommand<object>(_ => popupCommand());
        LogoutCommand = new RelayCommand<object>(_ => logoutCommand());
        Test = new RelayCommand<object>(_ => test.Navigate());

            _closePopupListItemNavigationService = closePopupListItemNavigationService;
        _accountStore.CurrentStoreChanged += OnCurrentAccountChanged;
        _floatingNavigationStore.CurrentStoreChanged += OnCurrentFloatChanged;
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
        OnPropertyChanged(nameof(IsMenuOpen));
    }
    private void logoutCommand()
    {
        _accountStore.Logout();
        NavigateHomeCommand.Execute(null);
    }
    private void OnCurrentAccountChanged()
    {
        OnPropertyChanged(nameof(IsLoggedIn));
        OnPropertyChanged(nameof(IsMenuOpen));
    }
    private void OnCurrentFloatChanged()
    {
        OnPropertyChanged(nameof(IsMenuOpen));
    }

    public override void Dispose()
    {
        _accountStore.CurrentStoreChanged -= OnCurrentAccountChanged;
        base.Dispose();
    }
}
