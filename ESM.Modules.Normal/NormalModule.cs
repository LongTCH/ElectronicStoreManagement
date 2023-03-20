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
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(HomeView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(AccountView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<HomeViewModel>();
            containerRegistry.Register<AccountViewModel>();
            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>(ViewNames.HomeView);
            containerRegistry.RegisterForNavigation<AccountView, AccountViewModel>(ViewNames.AccountView);
        }
    }
}