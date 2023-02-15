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
        services.AddSingleton<DataProvider>(s=> new DataProvider(s.GetRequiredService<ESMDbContext>()));

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<AccountStore>();
        services.AddSingleton<NavigationStore>();
        services.AddSingleton<ModalNavigationStore>();
        services.AddSingleton<FloatingNavigationStore>();
        services.AddSingleton<VerificationStore>();


        services.AddTransient<AccountViewModel>(s => new AccountViewModel(
            s.GetRequiredService<AccountStore>(),
            CreateHomeNavigationService(s)));
        services.AddTransient<LoginViewModel>(CreateLoginViewModel);
        services.AddSingleton<ControlBarViewModel>();
        services.AddTransient<NavigationBarViewModel>(CreateNavigationBarViewModel);
        services.AddSingleton<HomeViewModel>(CreateHomeViewModel);
        services.AddTransient<RegisterViewModel>();
        services.AddTransient<InputVerificationViewModel>(CreateInputEmailViewModel);
        services.AddTransient<VerifyEmailViewModel>(CreateVerifyEmailViewModel);
        services.AddSingleton<PopupListItemViewModel>(CreatePopupListItemViewModel);
        services.AddTransient<ResetPasswordViewModel>(CreateResetPasswordViewModel);

        services.AddTransient<LaptopViewModel>(CreateLaptopViewModel);
        services.AddTransient<PCViewModel>();
        services.AddTransient<PCCPUViewModel>();
        services.AddTransient<PCHarddiskViewModel>();
        services.AddTransient<MonitorViewModel>();
        services.AddTransient<VGAViewModel>();
        services.AddTransient<SmartPhoneViewModel>();

        services.AddSingleton<INavigationService>(CreateHomeNavigationService);
        services.AddSingleton<CloseModalNavigationService>(); 
        services.AddSingleton<CloseFloatNavigationService>();


        services.AddSingleton<MainViewModel>();

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

