using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModels.Services;
using ViewModels.Stores.Navigations;
using ViewModels;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace StartUps;

public partial class App : Application
{
    private ConfigurationBuilder CreateConfigurationBuilder(string filename)
    {
        ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        try
        {
            configurationBuilder.SetBasePath(Directory.GetParent(
            Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent
            + "\\ViewModels\\JsonConfig");
        }
        catch { }
        configurationBuilder.AddJsonFile(filename);
        return configurationBuilder;
        //}
        //catch { }
        //configurationBuilder.AddJsonFile(filename);

    }
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
        return new ModalNavigationService<ErrorViewModel>(
            serviceProvider.GetRequiredService<ModalNavigationStore>(),
            () => serviceProvider.GetRequiredService<ErrorViewModel>());
    }

}
