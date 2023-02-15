using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using ViewModels.Services;
using ViewModels.Stores.Accounts;
using ViewModels;
using ViewModels.Stores;
using ViewModels.Stores.Navigations;
using ViewModels.ControlViewModels;
using Models;
using ViewModels.ProductViewModels;
using ViewModels.NotifyControlViewModels;

namespace StartUps;

public partial class App : Application
{
    private VerifyEmailViewModel CreateVerifyEmailViewModel(IServiceProvider serviceProvider)
    {
        return new VerifyEmailViewModel(
            serviceProvider.GetRequiredService<VerificationStore>(),
            serviceProvider.GetRequiredService<CloseModalNavigationService>(),
            CreateResetPasswordNavigationService(serviceProvider));
    }
    private ResetPasswordViewModel CreateResetPasswordViewModel(IServiceProvider serviceProvider)
    {
        return new ResetPasswordViewModel(
            serviceProvider.GetRequiredService<DataProvider>(),
            serviceProvider.GetRequiredService<AccountStore>(),
            serviceProvider.GetRequiredService<VerificationStore>(),
            CreateLoginNavigationService(serviceProvider));
    }
    private LaptopViewModel CreateLaptopViewModel(IServiceProvider serviceProvider)
    {
        return new LaptopViewModel(serviceProvider.GetRequiredService<DataProvider>());
    }
    private HomeViewModel CreateHomeViewModel(IServiceProvider serviceProvider)
    {
        return new HomeViewModel(CreateLoginNavigationService(serviceProvider));
    }
    private LoginViewModel CreateLoginViewModel(IServiceProvider serviceProvider)
    {
        return new LoginViewModel(
            serviceProvider.GetRequiredService<DataProvider>(),
            serviceProvider.GetRequiredService<AccountStore>(),
            CreateAccountNavigationService(serviceProvider),
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
            CreateHomeNavigationService(serviceProvider),
            CreateAccountNavigationService(serviceProvider),
            serviceProvider.GetRequiredService<CloseFloatNavigationService>());
    }
    public InputVerificationViewModel CreateInputEmailViewModel(IServiceProvider serviceProvider)
    {
        return new InputVerificationViewModel(serviceProvider.GetRequiredService<VerificationStore>(),
            CreateEmailVerificattionService(serviceProvider));
    }
    public PopupListItemViewModel CreatePopupListItemViewModel(IServiceProvider serviceProvider)
    {
        return new PopupListItemViewModel(
            serviceProvider.GetRequiredService<CloseFloatNavigationService>(),
            CreateLaptopNavigationService(serviceProvider),
            CreateMonitorNavigationService(serviceProvider),
            CreatePCNavigationService(serviceProvider),
            CreatePCCPUNavigationService(serviceProvider),
            CreatePCHardDiskNavigationService(serviceProvider),
            CreateVGANavigationService(serviceProvider),
            CreateSmartPhoneNavigationService(serviceProvider));
    }
}
