using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Stores.Accounts;

public class AccountStore : IStore
{
    private Account? _currentAccount;
    public Account? CurrentAccount
    {
        get => _currentAccount;
        set
        {
            _currentAccount = value;
            CurrentStoreChanged?.Invoke();
        }
    }

    public bool IsLoggedIn => CurrentAccount != null;

    public event Action? CurrentStoreChanged;
    public void Logout()
    {
        CurrentAccount = null;
    }
}
