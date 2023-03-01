using Models;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Stores.Accounts;

public class AccountStore : IStore
{
    private AccountDTO? _currentAccount;
    public AccountDTO? CurrentAccount
    {
        get => _currentAccount;
        set
        {
            _currentAccount = value;
            CurrentStoreChanged?.Invoke();
        }
    }

    public bool IsLoggedIn => CurrentAccount != null;
    public bool IsAdmin => CurrentAccount != null && CurrentAccount.Id.StartsWith("0");
    public bool IsSellStaff => CurrentAccount != null && CurrentAccount.Id.StartsWith("1");
    public bool IsTypingStaff => CurrentAccount != null && CurrentAccount.Id.StartsWith("2");

    public event Action? CurrentStoreChanged;
    public void Logout()
    {
        CurrentAccount = null;
    }
}
