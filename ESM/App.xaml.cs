using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.Authentication;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.Normal;
using ESM.ViewModels;
using ESM.Views;
using MahApps.Metro.Controls.Dialogs;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace ESM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<MainWindowViewModel>();
            containerRegistry.RegisterSingleton<AccountStore>();
            containerRegistry.RegisterSingleton<IUnitOfWork,UnitOfWork>();
            containerRegistry.RegisterSingleton<IModalService, ModalService>();
            containerRegistry.RegisterSingleton<IOpenDialogService, OpenDialogService>();
            containerRegistry.RegisterSingleton<IDialogCoordinator, DialogCoordinator>();
            containerRegistry.RegisterSingleton<ISendEmailService, SendEmailService>();


            containerRegistry.RegisterDialogWindow<DialogWindow>();
            containerRegistry.RegisterDialog<NotificationDialog, NotificationDialogViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<AuthenticationModule>();
            moduleCatalog.AddModule<NormalModule>();
            moduleCatalog.AddModule<MainModule>();
        }

    }
}
