using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using ViewModels.Services;
using ViewModels.Stores.Navigations;
using ViewModels;
using ViewModels.Stores;
using ViewModels.ControlViewModels;
using ViewModels.NotifyControlViewModels;
using ViewModels.ProductViewModels;
using ViewModels.Admins;
using Models.Interfaces;

namespace StartUps;

public partial class App : Application
{
    private INavigationService CreateProductInputNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<ProductInputViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<ProductInputViewModel>);
    }
    private INavigationService CreateProductDetailNavigationService(IServiceProvider serviceProvider)
    {
        return new ModalNavigationService<ProductDetailViewModel>(
            serviceProvider.GetRequiredService<ModalNavigationStore>(),
            serviceProvider.GetRequiredService<ProductDetailViewModel>);
    }
    private INavigationService CreateChangeAccountInfoNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<ChangeAccountInfoViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            serviceProvider.GetRequiredService<ChangeAccountInfoViewModel>);
    }
    private INavigationService CreateResetPasswordNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<ResetPasswordViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(), 
            serviceProvider.GetRequiredService<ResetPasswordViewModel>);
    }
    private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<HomeViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(), 
            serviceProvider.GetRequiredService<HomeViewModel>);
    }

    private INavigationService CreateLoginNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<LoginViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(), 
            serviceProvider.GetRequiredService<LoginViewModel>);
    }

    private INavigationService CreateAccountNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<AccountViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(), 
            serviceProvider.GetRequiredService<AccountViewModel>);
    }
    private INavigationService CreateRegisterNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<RegisterViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(), 
            serviceProvider.GetRequiredService<RegisterViewModel>);
    }
    private INavigationService CreateForgotPasswordNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<InputVerificationViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(), 
            serviceProvider.GetRequiredService<InputVerificationViewModel>);
    }
    private INavigationService CreateEmailVerificattionService(IServiceProvider serviceProvider)
    {
        return new EmailVerificationService(
            serviceProvider.GetRequiredService<VerificationStore>(),
            new ModalNavigationService<VerifyEmailViewModel>(
                serviceProvider.GetRequiredService<ModalNavigationStore>(),
                serviceProvider.GetRequiredService<VerifyEmailViewModel>),
            serviceProvider.GetRequiredService<IUnitOfWork>());
    }
    private INavigationService CreateLaptopNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<LaptopViewModel>(serviceProvider.GetRequiredService<NavigationStore>(), serviceProvider.GetRequiredService<LaptopViewModel>);
    }
    private INavigationService CreateMonitorNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<MonitorViewModel>(serviceProvider.GetRequiredService<NavigationStore>(), serviceProvider.GetRequiredService<MonitorViewModel>);
    }
    private INavigationService CreatePCNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<PCViewModel>(serviceProvider.GetRequiredService<NavigationStore>(), serviceProvider.GetRequiredService<PCViewModel>);
    }
    private INavigationService CreatePCCPUNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<PCCPUViewModel>(serviceProvider.GetRequiredService<NavigationStore>(), serviceProvider.GetRequiredService<PCCPUViewModel>);
    }
    private INavigationService CreatePCHardDiskNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<PCHarddiskViewModel>(serviceProvider.GetRequiredService<NavigationStore>(), serviceProvider.GetRequiredService<PCHarddiskViewModel>);
    }
    private INavigationService CreateVGANavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<VGAViewModel>(serviceProvider.GetRequiredService<NavigationStore>(), serviceProvider.GetRequiredService<VGAViewModel>);
    }
    private INavigationService CreateSmartPhoneNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<SmartPhoneViewModel>(serviceProvider.GetRequiredService<NavigationStore>(), serviceProvider.GetRequiredService<SmartPhoneViewModel>);
    }
}
