using System.Windows.Input;
using ViewModels.Services;
using ViewModels.Stores.Accounts;
using ViewModels.Stores.Navigations;

namespace ViewModels.ControlViewModels;

public class TestNavigationBarViewModel : NavigationBarViewModel
{
    public TestNavigationBarViewModel(AccountStore accountStore, 
        FloatingNavigationStore floatingNavigationStore, 
        INavigationService loginNavigationService, 
        INavigationService popupListItemNavigationService, 
        INavigationService homeNavigationService, 
        INavigationService accountNavigationService, 
        INavigationService closePopupListItemNavigationService, 
        INavigationService test,
        PopupListItemViewModel popupListItemViewModel) : base(accountStore, floatingNavigationStore, loginNavigationService, popupListItemNavigationService, homeNavigationService, accountNavigationService, closePopupListItemNavigationService, test)
    {
        LaptopNavigation = popupListItemViewModel.LaptopNavigation;
        MonitorNavigation = popupListItemViewModel.MonitorNavigation;
        PCNavigation = popupListItemViewModel.PCNavigation;
        PCCPUNavigation = popupListItemViewModel.PCCPUNavigation;
        PCDiskNavigation = popupListItemViewModel.PCDiskNavigation;
        VGANavigation = popupListItemViewModel.VGANavigation;
        SmartPhoneNavigation = popupListItemViewModel.SmartPhoneNavigation;
        _accountStore.CurrentStoreChanged += OnCurrentAccountChanged;
    }
    public bool IsNotLoggedIn => !IsLoggedIn;
    public ICommand LaptopNavigation { get; }
    public ICommand MonitorNavigation { get; }
    public ICommand PCNavigation { get; }
    public ICommand PCCPUNavigation { get; }
    public ICommand PCDiskNavigation { get; }
    public ICommand VGANavigation { get; }
    public ICommand SmartPhoneNavigation { get; }

    private void OnCurrentAccountChanged()
    {
        OnPropertyChanged(nameof(IsSellStaff));
        OnPropertyChanged(nameof(IsTypingStaff));
    }   
}
