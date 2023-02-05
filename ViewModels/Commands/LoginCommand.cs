using Models;
using System.Linq;
using ViewModels.Services;
using ViewModels.Stores.Accounts;

namespace ViewModels.Commands;

public class LoginCommand : CommandBase
{
    private readonly ESMDbContext _esmDbContext;
    private readonly LoginViewModel _viewModel;
    private readonly AccountStore _accountStore;
    private readonly INavigationService _navigationService;
    private readonly INavigationService _openNotifyView;

    public LoginCommand(ESMDbContext esmDbContext, LoginViewModel viewModel, AccountStore accountStore, INavigationService navigationService, INavigationService openNotifyView)
    {
        _esmDbContext = esmDbContext;
        _viewModel = viewModel;
        _accountStore = accountStore;
        _navigationService = navigationService;
        _openNotifyView = openNotifyView;
    }

    public override void Execute(object? parameter)
    {
        Account? account = _esmDbContext.Accounts
            .Where(s => s.Username == _viewModel.Username && s.PasswordHash == _viewModel.Password)
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