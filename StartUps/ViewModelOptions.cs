using Microsoft.Extensions.DependencyInjection;
using Models;
using System;
using System.Windows;
using ViewModels.Services;
using ViewModels.Stores.Accounts;
using ViewModels;

namespace StartUps;

public partial class App : Application
{
    private LoginViewModel CreateLoginViewModel(IServiceProvider serviceProvider)
    {
        CompositeNavigationService navigationService = new CompositeNavigationService(
            serviceProvider.GetRequiredService<CloseModalNavigationService>(),
            CreateAccountNavigationService(serviceProvider));

        return new LoginViewModel(
            serviceProvider.GetRequiredService<ESMDbContext>(),
            serviceProvider.GetRequiredService<AccountStore>(),
            navigationService,
            CreateLoginFailNavigationService(serviceProvider),
            CreateRegisterNavigationService(serviceProvider),
            CreateForgotPasswordNavigationService(serviceProvider));
    }

    private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
    {
        return new NavigationBarViewModel(
            serviceProvider.GetRequiredService<ESMDbContext>(),
            serviceProvider.GetRequiredService<AccountStore>(),
            CreateLoginNavigationService(serviceProvider));
    }
}
