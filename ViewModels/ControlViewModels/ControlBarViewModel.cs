using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using ViewModels.Commands;
using ViewModels.Stores.Accounts;

namespace ViewModels.ControlViewModels;

public class ControlBarViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;
    public ControlBarViewModel(AccountStore accountStore)
    {
        _accountStore = accountStore;
    }

    public string Message => "Xin chào " + _accountStore.CurrentAccount?.FirstName;
    public bool CanShow => _accountStore.IsLoggedIn;

}
