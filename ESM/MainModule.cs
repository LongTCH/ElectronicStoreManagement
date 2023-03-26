using ESM.Core;
using ESM.Core.ShareServices;
using ESM.ViewModels;
using ESM.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
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
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ErrorNotifyView, ErrorNotifyViewModel>(ViewNames.ErrorModal);
            containerRegistry.RegisterForNavigation<InformationView, InformationViewModel>(ViewNames.InformationModal);
        }
    }
}
