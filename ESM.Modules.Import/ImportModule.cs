using ESM.Core;
using ESM.Modules.Import.ViewModels;
using ESM.Modules.Import.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

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
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(PCInputView));
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(MonitorInputView));
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(PhoneInputView));
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(CPUInputView));
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(VGAInputView));

            _regionManager.RegisterViewWithRegion("TabRegion", typeof(ComboInputView));

            _regionManager.RegisterViewWithContentRegion<ProductInputView>();
            _regionManager.RegisterViewWithContentRegion<RegisterView>();
            _regionManager.RegisterViewWithContentRegion<ChangeAccountInfoView>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ProductInputView, ProductInputViewModel>(ViewNames.ProductInputView);
            containerRegistry.RegisterForNavigation<RegisterView, RegisterViewModel>(ViewNames.RegisterView);
            containerRegistry.RegisterForNavigation<ChangeAccountInfoView, ChangeAccountInfoViewModel>(ViewNames.ChangeAccountInfoView);
        }
    }
}