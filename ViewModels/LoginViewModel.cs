using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores.Accounts;

namespace ViewModels;

public class LoginViewModel : ViewModelBase
{
    private readonly ESMDbContext _esmDbContext;
    private readonly AccountStore _accountStore;
    private readonly INavigationService _navigationService;
    private readonly INavigationService _openNotifyView;
    private readonly INavigationService _registerNavigationService;
    private readonly INavigationService _forgotPasswordNavigationService;
    private string _username;
    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    private string _password;
    public string Password
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
    public LoginViewModel(ESMDbContext eSMDbContext,
        AccountStore accountStore,
        INavigationService loginNavigationService,
        INavigationService openNotifyView,
        INavigationService registerNavigationService,
        INavigationService forgotPasswordNavigationService)
    {
        _esmDbContext = eSMDbContext;
        _accountStore = accountStore;
        _navigationService = loginNavigationService;
        _openNotifyView = openNotifyView;
        _registerNavigationService = registerNavigationService;
        _forgotPasswordNavigationService= forgotPasswordNavigationService;

        LoginCommand = new AsyncRelayCommand(loginCommand);
        PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => Password = p.Password);
        RegisterNavigationCommand = new RelayCommand<object>((o)=>registerNavigationService.Navigate());
        ForgotPasswordNavigationCommand = new RelayCommand<object>((o) => forgotPasswordNavigationService.Navigate());
    }
    private async Task loginCommand()
    {
        Account? account = await _esmDbContext.Accounts
            .Where(s => s.Username == Username && s.PasswordHash == Password)
            .FirstOrDefaultAsync();
        if (account == null)
        {
            _openNotifyView.Navigate();
            
            return;
        }
        _accountStore.CurrentAccount = account;
        _navigationService.Navigate();
    }
}
