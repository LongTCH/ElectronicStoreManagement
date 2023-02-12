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

namespace StartUps;

public partial class App : Application
{
    private INavigationService CreateListBoxItemNavigationService(IServiceProvider serviceProvider)
    {
        return new FloatingNavigationService<PopupListItem>(
            serviceProvider.GetRequiredService<FloatingNavigationStore>(),
            serviceProvider.GetRequiredService<PopupListItem>);
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
    private INavigationService CreateLoginFailNavigationService(IServiceProvider serviceProvider)
    {
        return new ModalNavigationService<ErrorNotifyViewModel>(
            serviceProvider.GetRequiredService<ModalNavigationStore>(),
            serviceProvider.GetRequiredService<ErrorNotifyViewModel>);
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
            serviceProvider.GetRequiredService<EmailStore>(),
            new ModalNavigationService<VerifyEmailViewModel>(
                serviceProvider.GetRequiredService<ModalNavigationStore>(),
                serviceProvider.GetRequiredService<VerifyEmailViewModel>));
    }
}
