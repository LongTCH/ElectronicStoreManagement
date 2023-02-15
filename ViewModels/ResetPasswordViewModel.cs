using Models;
using Scrypt;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.MyMessageBox;
using ViewModels.Services;
using ViewModels.Stores.Accounts;

namespace ViewModels;

public class ResetPasswordViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;
    private readonly DataProvider _dataProvider;
    private readonly INavigationService _loginNavigate;
    public ResetPasswordViewModel(DataProvider dataProvider, AccountStore accountStore, INavigationService loginNavigate)
    {
        _dataProvider = dataProvider;
        _accountStore = accountStore;
        _loginNavigate = loginNavigate;
        if (accountStore.CurrentAccount != null) IsOldPasswordType = true;
        ResetCommand = new RelayCommand<object>(_ => reset());
    }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? ReTypeNewPassword { get; set; }
    public bool IsOldPasswordType { get; set; } = false;
    public ICommand ResetCommand { get; }
    private void reset()
    {
        if (IsOldPasswordType)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            if (encoder.Compare(OldPassword, _accountStore.CurrentAccount.PasswordHash) == false)
            {
                ErrorNotifyViewModel.Instance!.Show("Wrong Password", "Failed");
                return;
            }
            if (NewPassword != ReTypeNewPassword || NewPassword == null)
            {
                ErrorNotifyViewModel.Instance!.Show("New Password Invalid", "Failed");
                return;
            }
            // code hera
        }
        else
        {
            if (NewPassword != ReTypeNewPassword || NewPassword == null)
            {
                ErrorNotifyViewModel.Instance!.Show("New Password Invalid", "Failed");
                return;
            }
            // code hera
        }
    }
}
