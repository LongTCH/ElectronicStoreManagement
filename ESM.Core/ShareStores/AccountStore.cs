using ESM.Modules.DataAccess.DTOs;
using System;

namespace ESM.Core.ShareStores
{
    public class AccountStore
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
}
