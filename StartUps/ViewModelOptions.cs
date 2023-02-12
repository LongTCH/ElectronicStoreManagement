using Microsoft.Extensions.DependencyInjection;
using Models;
using System;
using System.Windows;
using ViewModels.Services;
using ViewModels.Stores.Accounts;
using ViewModels;
using ViewModels.Stores;
using ViewModels.Stores.Navigations;
using ViewModels.ControlViewModels;

namespace StartUps;

public partial class App : Application
{
    private LoginViewModel CreateLoginViewModel(IServiceProvider serviceProvider)
    {
        return new LoginViewModel(
            serviceProvider.GetRequiredService<AccountStore>(),
            CreateAccountNavigationService(serviceProvider),
            CreateLoginFailNavigationService(serviceProvider),
            CreateRegisterNavigationService(serviceProvider),
            CreateForgotPasswordNavigationService(serviceProvider));
    }

    private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
    {
        return new NavigationBarViewModel(
            serviceProvider.GetRequiredService<AccountStore>(),
            serviceProvider.GetRequiredService<FloatingNavigationStore>(),
            CreateLoginNavigationService(serviceProvider),
            CreateListBoxItemNavigationService(serviceProvider),
            serviceProvider.GetRequiredService<CloseFloatNavigationService>());
    }
    public InputVerificationViewModel CreateInputEmailViewModel(IServiceProvider serviceProvider)
    {
        return new InputVerificationViewModel(serviceProvider.GetRequiredService<EmailStore>(),
            CreateEmailVerificattionService(serviceProvider));
    }

}
