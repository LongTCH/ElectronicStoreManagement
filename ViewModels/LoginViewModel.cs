using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTOs;
using Models.Entities;
using Models.Interfaces;
using System;
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
    public ICommand ForgotPasswordNavigationCommand { get; }
    public LoginViewModel(IUnitOfWork unitOfWork,
        AccountStore accountStore,
        INavigationService accountNavigationService,
        INavigationService forgotPasswordNavigationService)
    {
        _unitOfWork = unitOfWork;
        _accountStore = accountStore;
        _navigationService = accountNavigationService;
        LoginCommand = new RelayCommand<object>(async(_) => await loginCommandAsync());
        PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => Password = p.Password);
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
        try
        {
            Task<AccountDTO?> t = new(() => _unitOfWork.Accounts.GetAccountWithIdAndPassword(Id, Password));
            t.Start();
            AccountDTO? account = await t;
            if (account == null)
            {
                ErrorNotifyViewModel.Instance!.Show("Can not find you account", "Login failed");
                return;
            }
            _accountStore.CurrentAccount = account;
            _navigationService.Navigate();
        }
        catch (Exception ex)
        {
            ErrorNotifyViewModel.Instance!.Show(ex.Message, "Error");
        }
    }
}
