using ESM.Core;
using ESM.ViewModels;
using ESM.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Linq;

namespace ESM
{
    public class MainModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MainModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.HostRegion, typeof(ErrorNotifyView));
            var errorview = _regionManager.Regions[RegionNames.HostRegion].Views.First(v => v.GetType().Equals(typeof(ErrorNotifyView)));
            _regionManager.Regions[RegionNames.HostRegion].Deactivate(errorview);

            _regionManager.RegisterViewWithRegion(RegionNames.HostRegion, typeof(InformationView));
            var informationrview = _regionManager.Regions[RegionNames.HostRegion].Views.First(v => v.GetType().Equals(typeof(InformationView)));
            _regionManager.Regions[RegionNames.HostRegion].Deactivate(informationrview);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ErrorNotifyView, ErrorNotifyViewModel>(HostNames.ErrorModal);
            containerRegistry.RegisterForNavigation<InformationView, InformationViewModel>(HostNames.InformationModal);
        }
    }
}
