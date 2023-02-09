using Models;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores.Accounts;

namespace ViewModels;

public class RegisterViewModel : ViewModelBase
{
    private readonly ESMDbContext _esmDbContext;
    private readonly AccountStore? _accountStore;
    private readonly INavigationService _navigationService;
    private readonly INavigationService _openNotifyView;
    private string? _username;
    public string? Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
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
    public bool isWrongRetype;
    public bool IsWrongRetype
    {
        get => isWrongRetype;
        set
        {
            isWrongRetype = value;
            OnPropertyChanged(nameof(IsWrongRetype));
        }
    }
    private string? _retypePassword;
    public string? RetypePassword
    {
        get => _retypePassword;
        set
        {
            _retypePassword = value;
            OnPropertyChanged(nameof(RetypePassword));
        }
    }
    public ICommand PasswordChangedCommand { get; } = null!;
    public ICommand RetypePasswordChangedCommand { get; } = null!;
    public ICommand SignUpCommand { get; } = null!;
    public RegisterViewModel(ESMDbContext esmDbContext,
        AccountStore? accountStore,
        INavigationService navigationService,
        INavigationService openNotifyView)
    {
        _esmDbContext = esmDbContext;
        _accountStore = accountStore;
        _navigationService = navigationService;
        _openNotifyView = openNotifyView;

        PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => Password = p.Password);
        RetypePasswordChangedCommand = new RelayCommand<PasswordBox>(retypePasswordChangedCommand);
    }
    private void retypePasswordChangedCommand(PasswordBox p)
    {
        RetypePassword = p.Password;
        if (RetypePassword == null) return;
        if (Password == null || RetypePassword!.Length >= Password!.Length && !RetypePassword.Equals(Password))
        {
            IsWrongRetype = true;
        }
        else IsWrongRetype = false;
    }
}
