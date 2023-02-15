using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores.Accounts;

namespace ViewModels;

public class AccountViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;

    public string? FirstName => _accountStore.CurrentAccount?.FirstName;
    public string? LastName => _accountStore.CurrentAccount?.LastName;
    public string? Email => _accountStore.CurrentAccount?.EmailAddress;
    public string? Phone => _accountStore.CurrentAccount?.Phone;
    public string? City => _accountStore.CurrentAccount?.City;
    public string? District => _accountStore.CurrentAccount?.District;
    public string? SubDistrict => _accountStore.CurrentAccount?.SubDistrict;
    public string? Street => _accountStore.CurrentAccount?.Street;

    public ICommand NavigateHomeCommand { get; }

    public AccountViewModel(AccountStore accountStore, INavigationService homeNavigationService)
    {
        _accountStore = accountStore;

        NavigateHomeCommand = new RelayCommand<object>((_) => homeNavigationService.Navigate());

        _accountStore.CurrentStoreChanged += OnCurrentAccountChanged;
    }

    private void OnCurrentAccountChanged()
    {
        
        
    }

    public override void Dispose()
    {
        _accountStore.CurrentStoreChanged -= OnCurrentAccountChanged;

        base.Dispose();
    }
}
