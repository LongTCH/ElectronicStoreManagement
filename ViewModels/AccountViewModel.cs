using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores.Accounts;

namespace ViewModels;

public class AccountViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;

    public string? Username => _accountStore.CurrentAccount?.Username;
    public string? FirstName => _accountStore.CurrentAccount?.FirstName;
    public string? LastName => _accountStore.CurrentAccount?.LastName;

    public ICommand NavigateHomeCommand { get; }

    public AccountViewModel(AccountStore accountStore, INavigationService homeNavigationService)
    {
        _accountStore = accountStore;

        NavigateHomeCommand = new RelayCommand<object>((_) => homeNavigationService.Navigate());

        _accountStore.CurrentStoreChanged += OnCurrentAccountChanged;
    }

    private void OnCurrentAccountChanged()
    {
        
        OnPropertyChanged(nameof(Username));
    }

    public override void Dispose()
    {
        _accountStore.CurrentStoreChanged -= OnCurrentAccountChanged;

        base.Dispose();
    }
}
