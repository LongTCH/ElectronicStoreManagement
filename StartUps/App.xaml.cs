using Microsoft.Extensions.DependencyInjection;
using Models;
using System;
using System.Windows;
using ViewModels;
using ViewModels.Services;
using ViewModels.Stores.Navigations;
using ViewModels.Stores.Accounts;
using Microsoft.Extensions.Configuration;
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
        services.AddDbContext<ESMDbContext>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<AccountStore>();
        services.AddSingleton<NavigationStore>();
        services.AddSingleton<ModalNavigationStore>();


        services.AddTransient<AccountViewModel>(s => new AccountViewModel(
            s.GetRequiredService<AccountStore>(),
            CreateHomeNavigationService(s)));
        services.AddTransient<LoginViewModel>(CreateLoginViewModel);
        services.AddTransient<ControlBarViewModel>();
        services.AddTransient<NavigationBarViewModel>(CreateNavigationBarViewModel);
        services.AddTransient<HomeViewModel>(s => new HomeViewModel(CreateLoginNavigationService(s)));
        services.AddSingleton<ErrorNotifyViewModel>();
        services.AddSingleton<RegisterViewModel>();
        services.AddSingleton<ForgotPasswordViewModel>();

        services.AddSingleton<INavigationService>(s => CreateHomeNavigationService(s));
        services.AddSingleton<CloseModalNavigationService>();



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

