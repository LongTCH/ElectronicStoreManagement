using Models;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores.Accounts;

namespace ViewModels;

public class LoginViewModel : ViewModelBase
{
    private string _username;
    public string Username
    {
        get
        {
            return _username;
        }
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    private string _password;
    public string Password
    {
        get
        {
            return _password;
        }
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public ICommand LoginCommand { get; }

    public LoginViewModel(ESMDbContext eSMDbContext, AccountStore accountStore, INavigationService loginNavigationService, INavigationService openNotifyView)
    {
        LoginCommand = new LoginCommand(eSMDbContext, this, accountStore, loginNavigationService, openNotifyView);
    }
}
