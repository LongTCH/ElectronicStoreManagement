using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using ViewModels.Services;
using ViewModels.Stores.Navigations;
using ViewModels;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace StartUps;

public partial class App : Application
{
    private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<HomeViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            () => serviceProvider.GetRequiredService<HomeViewModel>(),
            () => serviceProvider.GetRequiredService<NavigationBarViewModel>(),
            () => serviceProvider.GetRequiredService<ControlBarViewModel>());
    }

    private INavigationService CreateLoginNavigationService(IServiceProvider serviceProvider)
    {
        //return new ModalNavigationService<LoginViewModel>(
        //    serviceProvider.GetRequiredService<ModalNavigationStore>(),
        //    () => serviceProvider.GetRequiredService<LoginViewModel>());
        return new LayoutNavigationService<LoginViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            () => serviceProvider.GetRequiredService<LoginViewModel>(),
            () => serviceProvider.GetRequiredService<NavigationBarViewModel>(),
            () => serviceProvider.GetRequiredService<ControlBarViewModel>());
    }

    private INavigationService CreateAccountNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<AccountViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            () => serviceProvider.GetRequiredService<AccountViewModel>(),
            () => serviceProvider.GetRequiredService<NavigationBarViewModel>(),
            () => serviceProvider.GetRequiredService<ControlBarViewModel>());
    }
    private INavigationService CreateLoginFailNavigationService(IServiceProvider serviceProvider)
    {
        return new ModalNavigationService<ErrorNotifyViewModel>(
            serviceProvider.GetRequiredService<ModalNavigationStore>(),
            () => serviceProvider.GetRequiredService<ErrorNotifyViewModel>());
    }
    private INavigationService CreateRegisterNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<RegisterViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            ()=>serviceProvider.GetRequiredService<RegisterViewModel>(),
            ()=>serviceProvider.GetRequiredService<NavigationBarViewModel>(),
            ()=>serviceProvider.GetRequiredService<ControlBarViewModel>());
    }
    private INavigationService CreateForgotPasswordNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<ForgotPasswordViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            () => serviceProvider.GetRequiredService<ForgotPasswordViewModel>(),
            () => serviceProvider.GetRequiredService<NavigationBarViewModel>(),
            () => serviceProvider.GetRequiredService<ControlBarViewModel>());
    }
}
