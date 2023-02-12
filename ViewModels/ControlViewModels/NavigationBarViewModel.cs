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
    public ICommand PopupCommand { get; }
    public bool IsLoggedIn => _accountStore.IsLoggedIn;
    public bool IsLoginShow => !_accountStore.IsLoggedIn;
    public NavigationBarViewModel(AccountStore accountStore,
            FloatingNavigationStore floatingNavigationStore,
            INavigationService loginNavigationService,
            INavigationService popupListItemNavigationService,
            INavigationService closePopupListItemNavigationService)
    {
        _accountStore = accountStore;
        _floatingNavigationStore = floatingNavigationStore;
        NavigateLoginCommand = new RelayCommand<object>((o) => loginNavigationService.Navigate());
        _popupListItemNavigationService = popupListItemNavigationService;
        _closePopupListItemNavigationService = closePopupListItemNavigationService;
        PopupCommand = new RelayCommand<object>(o => popupCommand());
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
    }
    private void OnCurrentAccountChanged()
    {
        OnPropertyChanged(nameof(IsLoggedIn));
        OnPropertyChanged(nameof(IsLoginShow));
    }

    public override void Dispose()
    {
        _accountStore.CurrentStoreChanged -= OnCurrentAccountChanged;
        base.Dispose();
    }
}
