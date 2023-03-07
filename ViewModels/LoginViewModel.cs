using Models.DTOs;
using Models.Interfaces;
using Scrypt;
using System;
using System.Threading.Tasks;
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
        Task task = new(() => loginCommandAsync());
        LoginCommand = new RelayCommand<object>(_ => task.Start());
        PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => Password = p.Password);
        ForgotPasswordNavigationCommand = new RelayCommand<object>(_ => forgotPasswordNavigationService.Navigate());
    }
    private void loginCommandAsync()
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            ErrorNotifyViewModel.Instance!.Show("Enter ID", "Warning");
            return;
        }
        if (string.IsNullOrWhiteSpace(Password))
        {
            ErrorNotifyViewModel.Instance!.Show("Enter Password", "Warning");
            return;
        }
        try
        {
            ScryptEncoder encoder = new ScryptEncoder();
            AccountDTO? account = _unitOfWork.Accounts.Get(Id);
            if (account == null || !encoder.Compare(Password, account.PasswordHash))
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
