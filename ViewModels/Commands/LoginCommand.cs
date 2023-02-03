using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Services;
using ViewModels.Stores.Accounts;

namespace ViewModels.Commands;

public class LoginCommand : CommandBase
{
    private readonly LoginViewModel _viewModel;
    private readonly AccountStore _accountStore;
    private readonly INavigationService _navigationService;

    public LoginCommand(LoginViewModel viewModel, AccountStore accountStore, INavigationService navigationService)
    {
        _viewModel = viewModel;
        _accountStore = accountStore;
        _navigationService = navigationService;
    }
    public override void Execute(object parameter)
    {
        Account account = new Account()
        {
           
        };

        _accountStore.CurrentAccount = account;

        _navigationService.Navigate();
    }
}