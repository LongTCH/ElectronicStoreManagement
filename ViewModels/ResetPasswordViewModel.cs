using Models.Interfaces;
using Scrypt;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.MyMessageBox;
using ViewModels.Services;
using ViewModels.Stores;
using ViewModels.Stores.Accounts;

namespace ViewModels;

public class ResetPasswordViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;
    private readonly VerificationStore _verificationStore;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INavigationService _loginNavigate;
    public ResetPasswordViewModel(IUnitOfWork unitOfWork,
        AccountStore accountStore,
        VerificationStore verificationStore,
        INavigationService loginNavigate)
    {
        _unitOfWork = unitOfWork;
        _accountStore = accountStore;
        _verificationStore = verificationStore;
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
            if (encoder.Compare(OldPassword, _accountStore.CurrentAccount!.PasswordHash) == false)
            {
                ErrorNotifyViewModel.Instance!.Show("Wrong Password", "Failed");
                return;
            }
            if (NewPassword != ReTypeNewPassword || NewPassword == null)
            {
                ErrorNotifyViewModel.Instance!.Show("New Password Invalid", "Failed");
                return;
            }
            _verificationStore.Id = _accountStore.CurrentAccount?.Id;
        }
        else
        {
            if (NewPassword != ReTypeNewPassword || NewPassword == null)
            {
                ErrorNotifyViewModel.Instance!.Show("New Password Invalid", "Failed");
                return;
            }
        }
        
        _accountStore.Logout();
        _loginNavigate.Navigate();
        Task.Run(resetAsync);
    }
    private Task resetAsync()
    {
        try
        {
            ScryptEncoder encoder = new ScryptEncoder();
            _unitOfWork.Accounts.ResetPassword(_verificationStore.Id!, encoder.Encode(NewPassword!));
            _unitOfWork.Complete();
            InformationViewModel.Instance!.Show("Please log in your account", "Success");
        }
        catch (Exception)
        {
            ErrorNotifyViewModel.Instance!.Show("Reset password failed", "Error");
        }
        return Task.CompletedTask;
    }
}
