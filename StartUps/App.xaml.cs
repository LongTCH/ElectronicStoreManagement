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

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<AccountStore>();
        services.AddSingleton<NavigationStore>();
        services.AddSingleton<ModalNavigationStore>();
        services.AddSingleton<FloatingNavigationStore>();
        services.AddSingleton<EmailStore>();


        services.AddTransient<AccountViewModel>(s => new AccountViewModel(
            s.GetRequiredService<AccountStore>(),
            CreateHomeNavigationService(s)));
        services.AddTransient<LoginViewModel>(CreateLoginViewModel);
        services.AddSingleton<ControlBarViewModel>();
        services.AddTransient<NavigationBarViewModel>(CreateNavigationBarViewModel);
        services.AddSingleton<HomeViewModel>(s => new HomeViewModel(CreateLoginNavigationService(s)));
        services.AddSingleton<ErrorNotifyViewModel>();
        services.AddTransient<RegisterViewModel>();
        services.AddTransient<InputVerificationViewModel>(CreateInputEmailViewModel);
        services.AddTransient<VerifyEmailViewModel>();
        services.AddSingleton<PopupListItem>();

        services.AddSingleton<INavigationService>(s => CreateHomeNavigationService(s));
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

