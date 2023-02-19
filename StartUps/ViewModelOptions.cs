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
using ViewModels.Admins;
using Models.Interfaces;

namespace StartUps;

public partial class App : Application
{
    private ProductDetailViewModel CreateProductDetailViewModel(IServiceProvider serviceProvider)
    {
        return new ProductDetailViewModel(
            serviceProvider.GetRequiredService<ProductDetailStore>(),
            serviceProvider.GetRequiredService<CloseModalNavigationService>());
    }
    private ChangeAccountInfoViewModel CreateChangeAccountInfoViewModel(IServiceProvider serviceProvider)
    {
        return new ChangeAccountInfoViewModel(
            serviceProvider.GetRequiredService<IUnitOfWork>(),
            CreateChangeAccountInfoNavigationService(serviceProvider));
    }
    private RegisterViewModel CreateRegisterViewModel(IServiceProvider serviceProvider)
    {
        return new RegisterViewModel(serviceProvider.GetRequiredService<IUnitOfWork>());
    }
    private AccountViewModel CreateAccountViewModel(IServiceProvider serviceProvider)
    {
        return new AccountViewModel(
            serviceProvider.GetRequiredService<IUnitOfWork>(),
            serviceProvider.GetRequiredService<AccountStore>(),
            CreateResetPasswordNavigationService(serviceProvider),
            CreateRegisterNavigationService(serviceProvider),
            CreateChangeAccountInfoNavigationService(serviceProvider));
    }
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
            serviceProvider.GetRequiredService<IUnitOfWork>(),
            serviceProvider.GetRequiredService<AccountStore>(),
            serviceProvider.GetRequiredService<VerificationStore>(),
            CreateLoginNavigationService(serviceProvider));
    }
    private LaptopViewModel CreateLaptopViewModel(IServiceProvider serviceProvider)
    {
        return new LaptopViewModel(
            serviceProvider.GetRequiredService<IUnitOfWork>(),
            serviceProvider.GetRequiredService<ProductDetailStore>(),
            CreateProductDetailNavigationService(serviceProvider));
    }
    private MonitorViewModel CreateMonitorViewModel(IServiceProvider serviceProvider)
    {
        return new MonitorViewModel(
            serviceProvider.GetRequiredService<IUnitOfWork>(),
            serviceProvider.GetRequiredService<ProductDetailStore>(),
            CreateProductDetailNavigationService(serviceProvider));
    }
    private PCViewModel CreatePCViewModel(IServiceProvider serviceProvider)
    {
        return new PCViewModel(
            serviceProvider.GetRequiredService<IUnitOfWork>(),
            serviceProvider.GetRequiredService<ProductDetailStore>(),
            CreateProductDetailNavigationService(serviceProvider));
    }
    private PCCPUViewModel CreatePCCPUViewModel(IServiceProvider serviceProvider)
    {
        return new PCCPUViewModel(
            serviceProvider.GetRequiredService<IUnitOfWork>(),
            serviceProvider.GetRequiredService<ProductDetailStore>(),
            CreateProductDetailNavigationService(serviceProvider));
    }
    private PCHarddiskViewModel CreatePCHardDiskViewModel(IServiceProvider serviceProvider)
    {
        return new PCHarddiskViewModel(
            serviceProvider.GetRequiredService<IUnitOfWork>(),
            serviceProvider.GetRequiredService<ProductDetailStore>(),
            CreateProductDetailNavigationService(serviceProvider));
    }
    private SmartPhoneViewModel CreateSmartPhoneViewModel(IServiceProvider serviceProvider)
    {
        return new SmartPhoneViewModel(
            serviceProvider.GetRequiredService<IUnitOfWork>(),
            serviceProvider.GetRequiredService<ProductDetailStore>(),
            CreateProductDetailNavigationService(serviceProvider));
    }
    private VGAViewModel CreateVGAViewModel(IServiceProvider serviceProvider)
    {
        return new VGAViewModel(
            serviceProvider.GetRequiredService<IUnitOfWork>(),
            serviceProvider.GetRequiredService<ProductDetailStore>(),
            CreateProductDetailNavigationService(serviceProvider));
    }
    private HomeViewModel CreateHomeViewModel(IServiceProvider serviceProvider)
    {
        return new HomeViewModel(CreateLoginNavigationService(serviceProvider));
    }
    private LoginViewModel CreateLoginViewModel(IServiceProvider serviceProvider)
    {
        return new LoginViewModel(
            serviceProvider.GetRequiredService<IUnitOfWork>(),
            serviceProvider.GetRequiredService<AccountStore>(),
            CreateAccountNavigationService(serviceProvider),
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
            serviceProvider.GetRequiredService<CloseFloatNavigationService>(),
            CreateChangeAccountInfoNavigationService(serviceProvider));
    }
    private InputVerificationViewModel CreateInputEmailViewModel(IServiceProvider serviceProvider)
    {
        return new InputVerificationViewModel(serviceProvider.GetRequiredService<VerificationStore>(),
            CreateEmailVerificattionService(serviceProvider));
    }
    private PopupListItemViewModel CreatePopupListItemViewModel(IServiceProvider serviceProvider)
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
