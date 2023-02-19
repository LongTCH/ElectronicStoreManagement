using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTOs;
using Models.Entities;
using Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.MyMessageBox;
using ViewModels.Services;
using ViewModels.Stores.Accounts;

namespace ViewModels;

public class LoginViewModel : ViewModelBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AccountStore _accountStore;
    private readonly INavigationService _navigationService;
    private readonly INavigationService _registerNavigationService;
    private readonly INavigationService _forgotPasswordNavigationService;
    private string? _id;
    public string? Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    private string? _password;
    public string? Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }
    public ICommand PasswordChangedCommand { get; }
    public ICommand LoginCommand { get; }
    public ICommand RegisterNavigationCommand { get; }
    public ICommand ForgotPasswordNavigationCommand { get; }
    public LoginViewModel(IUnitOfWork unitOfWork,
        AccountStore accountStore,
        INavigationService loginNavigationService,
        INavigationService registerNavigationService,
        INavigationService forgotPasswordNavigationService)
    {
        _unitOfWork = unitOfWork;
        _accountStore = accountStore;
        _navigationService = loginNavigationService;
        _registerNavigationService = registerNavigationService;
        _forgotPasswordNavigationService = forgotPasswordNavigationService;

        LoginCommand = new RelayCommand<object>(async(_) => await loginCommandAsync());
        PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => Password = p.Password);
        RegisterNavigationCommand = new RelayCommand<object>(_ => registerNavigationService.Navigate());
        ForgotPasswordNavigationCommand = new RelayCommand<object>(_ => forgotPasswordNavigationService.Navigate());
    }
    private async Task loginCommandAsync()
    {
        if (Id.IsNullOrEmpty())
        {
            ErrorNotifyViewModel.Instance!.Show("Enter ID", "Warning");
            return;
        }
        if (Password.IsNullOrEmpty())
        {
            ErrorNotifyViewModel.Instance!.Show("Enter Password", "Warning");
            return;
        }
        Task<AccountDTO?> t = new(() => _unitOfWork.Accounts.GetAccountWithIdAndPassword(Id, Password));
        t.Start();
        AccountDTO? account = await t;
        if (account == null)
        {
            ErrorNotifyViewModel.Instance!.Show("Can not find you account", "Login failed");
            return;
        }
        _navigationService.Navigate();
        _accountStore.CurrentAccount = account;

    }
}
