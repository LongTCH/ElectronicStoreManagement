using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using ViewModels;
using ViewModels.Services;
using ViewModels.Stores.Navigations;
using ViewModels.Stores.Accounts;
using ViewModels.Stores;
using ViewModels.ControlViewModels;
using ViewModels.NotifyControlViewModels;
using Models;
using ViewModels.ProductViewModels;
using Models.Entities;
using ViewModels.Admins;
using Models.Interfaces;

namespace StartUps;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;
    public App()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddSingleton<ESMDbContext>();
        services.AddSingleton<IUnitOfWork, UnitOfWork>(s=> new UnitOfWork(s.GetRequiredService<ESMDbContext>()));
        
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<AccountStore>();
        services.AddSingleton<NavigationStore>();
        services.AddSingleton<ModalNavigationStore>();
        services.AddSingleton<FloatingNavigationStore>();
        services.AddSingleton<VerificationStore>(s => new VerificationStore(s.GetRequiredService<AccountStore>()));
        services.AddSingleton<ProductDetailStore>();

        services.AddSingleton<MainViewModel>();
        services.AddTransient<AccountViewModel>(CreateAccountViewModel);
        services.AddTransient<LoginViewModel>(CreateLoginViewModel);
        services.AddSingleton<ControlBarViewModel>();
        services.AddTransient<NavigationBarViewModel>(CreateNavigationBarViewModel);
        services.AddSingleton<HomeViewModel>(CreateHomeViewModel);
        services.AddTransient<RegisterViewModel>(CreateRegisterViewModel);
        services.AddTransient<InputVerificationViewModel>(CreateInputEmailViewModel);
        services.AddTransient<VerifyEmailViewModel>(CreateVerifyEmailViewModel);
        services.AddSingleton<PopupListItemViewModel>(CreatePopupListItemViewModel);
        services.AddTransient<ResetPasswordViewModel>(CreateResetPasswordViewModel);
        services.AddTransient<ChangeAccountInfoViewModel>(CreateChangeAccountInfoViewModel);
        services.AddTransient<ProductDetailViewModel>(CreateProductDetailViewModel);

        services.AddTransient<LaptopViewModel>(CreateLaptopViewModel);
        services.AddTransient<PCViewModel>(CreatePCViewModel);
        services.AddTransient<PCCPUViewModel>(CreatePCCPUViewModel);
        services.AddTransient<PCHarddiskViewModel>(CreatePCHardDiskViewModel);
        services.AddTransient<MonitorViewModel>(CreateMonitorViewModel);
        services.AddTransient<VGAViewModel>(CreateVGAViewModel);
        services.AddTransient<SmartPhoneViewModel>(CreateSmartPhoneViewModel);
        services.AddSingleton<HardDiskNhapLieuViewModel>();
        services.AddSingleton<MonitorNhapLieuViewModel>();
        services.AddSingleton<ProductInputViewModel>(CreateProductInputViewModel);

        services.AddSingleton<INavigationService>(CreateHomeNavigationService);
        services.AddSingleton<CloseModalNavigationService>(); 
        services.AddSingleton<CloseFloatNavigationService>();

        services.AddSingleton<MainWindow>(s => new MainWindow()
        {
            DataContext = s.GetRequiredService<MainViewModel>()
        });

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
        initialNavigationService.Navigate();
        MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        MainWindow.Show();
        base.OnStartup(e);
    }



}

