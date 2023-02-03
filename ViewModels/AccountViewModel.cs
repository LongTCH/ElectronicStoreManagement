using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores.Accounts;

namespace ViewModels;

public class AccountViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;

    public string Username => _accountStore.CurrentAccount?.Username;
    //public string Email => _accountStore.CurrentAccount?.Email;

    public ICommand NavigateHomeCommand { get; }

    public AccountViewModel(AccountStore accountStore, INavigationService homeNavigationService)
    {
        _accountStore = accountStore;

        NavigateHomeCommand = new NavigateCommand(homeNavigationService);

        _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
    }

    private void OnCurrentAccountChanged()
    {
        //OnPropertyChanged(nameof(Email));
        OnPropertyChanged(nameof(Username));
    }

    public override void Dispose()
    {
        _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;

        base.Dispose();
    }
}
