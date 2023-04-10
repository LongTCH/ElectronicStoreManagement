using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.Authentication;
using ESM.Modules.Authentication.Views;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Export;
using ESM.Modules.Import;
using ESM.Modules.Normal;
using ESM.ViewModels;
using ESM.Views;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using TabControl = System.Windows.Controls.TabControl;

namespace ESM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var _regionManager = Container.Resolve<IRegionManager>();
            _regionManager.RequestNavigateContentRegionWithTrace(ViewNames.HomeView);
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();

            containerRegistry.Register<IUnitOfWork, UnitOfWork>();

            containerRegistry.RegisterSingleton<MainWindowViewModel>();
            containerRegistry.RegisterSingleton<MainWindow>();
            containerRegistry.RegisterSingleton<AccountStore>();
            containerRegistry.RegisterSingleton<IModalService, ModalService>();
            containerRegistry.RegisterSingleton<IOpenDialogService, OpenDialogService>();
            containerRegistry.RegisterSingleton<IDialogCoordinator, DialogCoordinator>();
            containerRegistry.RegisterSingleton<ISendEmailService, SendEmailService>();
            containerRegistry.RegisterSingleton<IApplicationCommand, ApplicationCommand>();
            containerRegistry.RegisterSingleton<ICityListService, CityListService>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<AuthenticationModule>();
            moduleCatalog.AddModule<NormalModule>();
            moduleCatalog.AddModule<MainModule>();
            moduleCatalog.AddModule<ImportModule>();
            moduleCatalog.AddModule<ExportModule>();
        }
        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(TabControl), Container.Resolve<TabControlAdapter>());
        }
    }
}
