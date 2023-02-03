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

public class NavigationBarViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;

    public ICommand NavigateLoginCommand { get; }
    public bool IsLoggedIn => _accountStore.IsLoggedIn;
    public NavigationBarViewModel(AccountStore accountStore,
            INavigationService loginNavigationService)
    {
        _accountStore = accountStore;
        NavigateLoginCommand = new NavigateCommand(loginNavigationService);

        _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
    }
    private void OnCurrentAccountChanged()
    {
        OnPropertyChanged(nameof(IsLoggedIn));
    }

    public override void Dispose()
    {
        _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;

        base.Dispose();
    }
}
