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

        public MainModule(IRegionManager regionManager, IModalService modalService)
        {
            _regionManager = regionManager;
            modalService.Register<ErrorNotifyView>(ViewNames.ErrorModal);
            modalService.Register<InformationView>(ViewNames.InformationModal);
            ViewModelLocationProvider.Register<ErrorNotifyView, ErrorNotifyViewModel>();
            ViewModelLocationProvider.Register<InformationView, InformationViewModel>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ErrorNotifyView>(ViewNames.ErrorModal);
            containerRegistry.RegisterForNavigation<InformationView>(ViewNames.InformationModal);
        }
    }
}
