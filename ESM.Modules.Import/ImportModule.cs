using ESM.Core;
using ESM.Modules.Import.ViewModels;
using ESM.Modules.Import.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

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

            _regionManager.RegisterViewWithRegion(RegionNames.AccountRegion, typeof(RegisterView));
            _regionManager.RegisterViewWithRegion(RegionNames.AccountRegion, typeof(ChangeAccountInfoView));

            _regionManager.RegisterViewWithContentRegion<ProductInputView>();
            _regionManager.RegisterViewWithContentRegion<AccountManage>();
            _regionManager.RegisterViewWithContentRegion<DiscountInputView>();
            _regionManager.RegisterViewWithContentRegion<DistributorView>();
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ProductInputView, ProductInputViewModel>(ViewNames.ProductInputView);
            containerRegistry.RegisterForNavigation<AccountManage, AccountManageViewModel>(ViewNames.AccountManage);
            containerRegistry.RegisterForNavigation<ChangeAccountInfoView, ChangeAccountInfoViewModel>(ViewNames.ChangeAccountInfoView);
            containerRegistry.RegisterForNavigation<DiscountInputView, DiscountInputViewModel>(ViewNames.ProductManagement);
            containerRegistry.RegisterForNavigation<DistributorView, DistributorViewModel>(ViewNames.DistributorView); 
            containerRegistry.RegisterForNavigation<RegisterView, RegisterViewModel>(ViewNames.RegisterView);
        }
    }
}