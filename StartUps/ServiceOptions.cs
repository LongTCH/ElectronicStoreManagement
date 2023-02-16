using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using ViewModels.Services;
using ViewModels.Stores.Navigations;
using ViewModels;
using Microsoft.Extensions.Configuration;
using System.IO;
using ViewModels.Stores.Accounts;
using ViewModels.Stores;
using ViewModels.ControlViewModels;
using ViewModels.NotifyControlViewModels;
using Views.Products;
using ViewModels.ProductViewModels;
using Models;
using ViewModels.Admins;

namespace StartUps;

public partial class App : Application
{
    private INavigationService CreateChangeAccountInfoNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<ChangeAccountInfoViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<ChangeAccountInfoViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>,
            serviceProvider.GetRequiredService<ControlBarViewModel>);
    }
    private INavigationService CreateResetPasswordNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<ResetPasswordViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<ResetPasswordViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>,
            serviceProvider.GetRequiredService<ControlBarViewModel>);
    }
    private INavigationService CreateListBoxItemNavigationService(IServiceProvider serviceProvider)
    {
        return new FloatingNavigationService<PopupListItemViewModel>(
            serviceProvider.GetRequiredService<FloatingNavigationStore>(),
            serviceProvider.GetRequiredService<PopupListItemViewModel>);
    }
    private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<HomeViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<HomeViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>,
            serviceProvider.GetRequiredService<ControlBarViewModel>);
    }

    private INavigationService CreateLoginNavigationService(IServiceProvider serviceProvider)
    {
        //return new FloatingNavigationService<LoginViewModel>(
        //    serviceProvider.GetRequiredService<FloatingNavigationStore>(),
        //    serviceProvider.GetRequiredService<LoginViewModel>);
        //return new ModalNavigationService<LoginViewModel>(
        //    serviceProvider.GetRequiredService<ModalNavigationStore>(),
        //    () => serviceProvider.GetRequiredService<LoginViewModel>());
        return new LayoutNavigationService<LoginViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<LoginViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>,
            serviceProvider.GetRequiredService<ControlBarViewModel>);
    }

    private INavigationService CreateAccountNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<AccountViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<AccountViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>,
            serviceProvider.GetRequiredService<ControlBarViewModel>);
    }
    private INavigationService CreateRegisterNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<RegisterViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<RegisterViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>,
            serviceProvider.GetRequiredService<ControlBarViewModel>);
    }
    private INavigationService CreateForgotPasswordNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<InputVerificationViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<InputVerificationViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>,
            serviceProvider.GetRequiredService<ControlBarViewModel>);
    }
    private INavigationService CreateEmailVerificattionService(IServiceProvider serviceProvider)
    {
        return new EmailVerificationService(
            serviceProvider.GetRequiredService<VerificationStore>(),
            new ModalNavigationService<VerifyEmailViewModel>(
            serviceProvider.GetRequiredService<ModalNavigationStore>(),
            serviceProvider.GetRequiredService<VerifyEmailViewModel>),
            serviceProvider.GetRequiredService<DataProvider>());
    }
    private INavigationService CreateLaptopNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<LaptopViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<LaptopViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>,
            serviceProvider.GetRequiredService<ControlBarViewModel>);
    }
    private INavigationService CreateMonitorNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<MonitorViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<MonitorViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>,
            serviceProvider.GetRequiredService<ControlBarViewModel>);
    }
    private INavigationService CreatePCNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<PCViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<PCViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>,
            serviceProvider.GetRequiredService<ControlBarViewModel>);
    }
    private INavigationService CreatePCCPUNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<PCCPUViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<PCCPUViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>,
            serviceProvider.GetRequiredService<ControlBarViewModel>);
    }
    private INavigationService CreatePCHardDiskNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<PCHarddiskViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<PCHarddiskViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>,
            serviceProvider.GetRequiredService<ControlBarViewModel>);
    }
    private INavigationService CreateVGANavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<VGAViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<VGAViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>,
            serviceProvider.GetRequiredService<ControlBarViewModel>);
    }
    private INavigationService CreateSmartPhoneNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<SmartPhoneViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<SmartPhoneViewModel>,
            serviceProvider.GetRequiredService<NavigationBarViewModel>,
            serviceProvider.GetRequiredService<ControlBarViewModel>);
    }
}
