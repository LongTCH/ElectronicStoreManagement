using ESM.Core;
using ESM.Modules.Normal.ViewModels;
using ESM.Modules.Normal.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Linq;
using System;
using ESM.Core.ShareServices;
using Prism.Mvvm;

namespace ESM.Modules.Normal
{
    public class NormalModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public NormalModule(IRegionManager regionManager, IModalService modalService)
        {
            _regionManager = regionManager;
            ViewModelLocationProvider.Register<ProductDetailView, ProductDetailViewModel>();
            modalService.Register<ProductDetailView>(ViewNames.ProductDetailView);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            RegisterViewWithContentRegion(typeof(HomeView));
            RegisterViewWithContentRegion(typeof(AccountView));
            RegisterViewWithContentRegion(typeof(LaptopView));
            RegisterViewWithContentRegion(typeof(MonitorView));
            RegisterViewWithContentRegion(typeof(PCCPUView));
            RegisterViewWithContentRegion(typeof(PCView));
            RegisterViewWithContentRegion(typeof(PCHardDiskView));
            RegisterViewWithContentRegion(typeof(SmartPhoneView));
            RegisterViewWithContentRegion(typeof(VGAView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<HomeViewModel>();
            containerRegistry.Register<AccountViewModel>();

            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>(ViewNames.HomeView);
            containerRegistry.RegisterForNavigation<AccountView, AccountViewModel>(ViewNames.AccountView);

            containerRegistry.RegisterForNavigation<LaptopView, LaptopViewModel>(ViewNames.LaptopView);
            containerRegistry.RegisterForNavigation<MonitorView, MonitorViewModel>(ViewNames.MonitorView);
            containerRegistry.RegisterForNavigation<PCView, PCViewModel>(ViewNames.PCView);
            containerRegistry.RegisterForNavigation<PCCPUView, PCCPUViewModel>(ViewNames.PCCPUView);
            containerRegistry.RegisterForNavigation<SmartPhoneView, SmartPhoneViewModel>(ViewNames.SmartPhoneView);
            containerRegistry.RegisterForNavigation<PCHardDiskView, PCHardDiskViewModel>(ViewNames.PCHardDiskView);
            containerRegistry.RegisterForNavigation<VGAView, VGAViewModel>(ViewNames.VGAView);
        }
        private void RegisterViewWithContentRegion(Type type)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, type);
            var view = _regionManager.Regions[RegionNames.ContentRegion].Views.First(v => v.GetType().Equals(type));
            _regionManager.Regions[RegionNames.ContentRegion].Deactivate(view);
        }
    }
}