using Models;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores.Accounts;

namespace ViewModels;

public class NavigationBarViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;
    private readonly ESMDbContext _eSMDbContext;
    public ICommand NavigateLoginCommand { get; }
    public bool IsLoggedIn => _accountStore.IsLoggedIn;
    public NavigationBarViewModel(ESMDbContext eSMDbContext, AccountStore accountStore,
            INavigationService loginNavigationService)
    {
        _eSMDbContext = eSMDbContext;
        _accountStore = accountStore;
        NavigateLoginCommand = new NavigateCommand(loginNavigationService);
        _accountStore.CurrentStoreChanged += OnCurrentAccountChanged;
    }
    private void OnCurrentAccountChanged()
    {
        OnPropertyChanged(nameof(IsLoggedIn));
    }

    public override void Dispose()
    {
        _accountStore.CurrentStoreChanged -= OnCurrentAccountChanged;

        base.Dispose();
    }
}
