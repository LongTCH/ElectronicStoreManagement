using ESM.Core;
using ESM.Modules.Import.ViewModels;
using ESM.Modules.Import.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Linq;

namespace ESM.Modules.Import
{
    public class ImportModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ImportModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(HardDiskInputView));
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(LaptopInputView));
            RegisterViewWithContentRegion(typeof(ProductInputView));
            RegisterViewWithContentRegion(typeof(RegisterView));
            RegisterViewWithContentRegion(typeof(ChangeAccountInfoView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ProductInputView, ProductInputViewModel>(ViewNames.ProductInputView);
            containerRegistry.RegisterForNavigation<RegisterView, RegisterViewModel>(ViewNames.RegisterView);
            containerRegistry.RegisterForNavigation<ChangeAccountInfoView, ChangeAccountInfoViewModel>(ViewNames.ChangeAccountInfoView);
        }
        private void RegisterViewWithContentRegion(Type type)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, type);
            var view = _regionManager.Regions[RegionNames.ContentRegion].Views.First(v => v.GetType().Equals(type));
            _regionManager.Regions[RegionNames.ContentRegion].Deactivate(view);
        }
    }
}