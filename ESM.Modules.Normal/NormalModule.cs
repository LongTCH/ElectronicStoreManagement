using ESM.Core;
using ESM.Modules.Normal.ViewModels;
using ESM.Modules.Normal.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ESM.Modules.Normal
{
    public class NormalModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public NormalModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithContentRegion<HomeView>();
            _regionManager.RegisterViewWithContentRegion<AccountView>();
            _regionManager.RegisterViewWithContentRegion<LaptopView>();
            _regionManager.RegisterViewWithContentRegion<MonitorView>();
            _regionManager.RegisterViewWithContentRegion<PCCPUView>();
            _regionManager.RegisterViewWithContentRegion<PCView>();
            _regionManager.RegisterViewWithContentRegion<PCHardDiskView>();
            _regionManager.RegisterViewWithContentRegion<SmartPhoneView>();
            _regionManager.RegisterViewWithContentRegion<VGAView>();
            _regionManager.RegisterViewWithContentRegion<ComboView>();
            _regionManager.RegisterViewWithContentRegion<ManagementView>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>(ViewNames.HomeView);
            containerRegistry.RegisterForNavigation<AccountView, AccountViewModel>(ViewNames.AccountView);
            containerRegistry.RegisterForNavigation<ManagementView, ManagementViewModel>(ViewNames.ManagementView);

            containerRegistry.RegisterForNavigation<ProductDetailView, ProductDetailViewModel>(ViewNames.ProductDetailView);
            containerRegistry.RegisterForNavigation<LaptopView, LaptopViewModel>(ViewNames.LaptopView);
            containerRegistry.RegisterForNavigation<MonitorView, MonitorViewModel>(ViewNames.MonitorView);
            containerRegistry.RegisterForNavigation<PCView, PCViewModel>(ViewNames.PCView);
            containerRegistry.RegisterForNavigation<PCCPUView, PCCPUViewModel>(ViewNames.PCCPUView);
            containerRegistry.RegisterForNavigation<SmartPhoneView, SmartPhoneViewModel>(ViewNames.SmartPhoneView);
            containerRegistry.RegisterForNavigation<PCHardDiskView, PCHardDiskViewModel>(ViewNames.PCHardDiskView);
            containerRegistry.RegisterForNavigation<VGAView, VGAViewModel>(ViewNames.VGAView);
            containerRegistry.RegisterForNavigation<ComboView, ComboViewModel>(ViewNames.ComboView);
        }
    }
}