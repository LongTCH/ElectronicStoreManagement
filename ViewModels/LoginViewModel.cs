using Models;
using System.Linq;
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
    public LoginViewModel(ESMDbContext eSMDbContext, AccountStore accountStore, INavigationService loginNavigationService, INavigationService openNotifyView)
    {
        _esmDbContext = eSMDbContext;
        _accountStore = accountStore;
        _navigationService = loginNavigationService;
        _openNotifyView = openNotifyView;
        //LoginCommand = new LoginCommand(eSMDbContext, this, accountStore, loginNavigationService, openNotifyView);
        LoginCommand = new RelayCommand<UserControl>(loginCommand);
        PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => Password = p.Password);
    }
    private void loginCommand(UserControl userControl)
    {
        Account? account = _esmDbContext.Accounts
            .Where(s => s.Username == Username && s.PasswordHash == Password)
            .FirstOrDefault();
        if (account == null)
        {
            _openNotifyView.Navigate();
            return;
        }
        _accountStore.CurrentAccount = account;

        _navigationService.Navigate();
    }
}
