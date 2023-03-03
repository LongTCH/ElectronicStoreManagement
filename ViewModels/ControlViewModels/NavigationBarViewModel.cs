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
    protected readonly AccountStore _accountStore;
    public ICommand NavigateLoginCommand { get; }
    public ICommand NavigateHomeCommand { get; }
    public ICommand NavigateAccountCommand { get; }
    public ICommand PopupCommand { get; }
    public ICommand LogoutCommand { get; }
    public ICommand Test { get; }
    public ICommand BanHang { get; }
    public bool IsNotLoggedIn => !IsLoggedIn;
    public ICommand LaptopNavigation { get; }
    public ICommand MonitorNavigation { get; }
    public ICommand PCNavigation { get; }
    public ICommand PCCPUNavigation { get; }
    public ICommand PCDiskNavigation { get; }
    public ICommand VGANavigation { get; }
    public ICommand SmartPhoneNavigation { get; }
    public bool IsLoggedIn => _accountStore.IsLoggedIn;
    public bool IsAdmin => _accountStore.IsAdmin;
    public bool IsSellStaff => _accountStore.IsSellStaff || IsAdmin;
    public bool IsTypingStaff => _accountStore.IsTypingStaff || IsAdmin;
    public NavigationBarViewModel(AccountStore accountStore,
            PopupListItemViewModel popupListItemViewModel,
            INavigationService loginNavigationService,
            INavigationService homeNavigationService,
            INavigationService accountNavigationService,
            INavigationService test,
            INavigationService banHang)
    {
        _accountStore = accountStore;
        NavigateLoginCommand = new RelayCommand<object>(_ => loginNavigationService.Navigate());
        NavigateHomeCommand = new RelayCommand<object>(_ => homeNavigationService.Navigate());
        NavigateAccountCommand = new RelayCommand<object>(_ => accountNavigationService.Navigate());
        LogoutCommand = new RelayCommand<object>(_ => logoutCommand());
        Test = new RelayCommand<object>(_ => test.Navigate());
        LaptopNavigation = popupListItemViewModel.LaptopNavigation;
        MonitorNavigation = popupListItemViewModel.MonitorNavigation;
        PCNavigation = popupListItemViewModel.PCNavigation;
        PCCPUNavigation = popupListItemViewModel.PCCPUNavigation;
        PCDiskNavigation = popupListItemViewModel.PCDiskNavigation;
        VGANavigation = popupListItemViewModel.VGANavigation;
        SmartPhoneNavigation = popupListItemViewModel.SmartPhoneNavigation;
        _accountStore.CurrentStoreChanged += OnCurrentAccountChanged;
        BanHang = new RelayCommand<object>(_ => banHang.Navigate()); 
    }

    private void logoutCommand()
    {
        _accountStore.Logout();
        NavigateHomeCommand.Execute(null);
    }
    private void OnCurrentAccountChanged()
    {
        OnPropertyChanged(nameof(IsLoggedIn));
        OnPropertyChanged(nameof(IsSellStaff));
        OnPropertyChanged(nameof(IsTypingStaff));
        OnPropertyChanged(nameof(IsNotLoggedIn));
    }
    public override void Dispose()
    {
        _accountStore.CurrentStoreChanged -= OnCurrentAccountChanged;
        base.Dispose();
    }
}
