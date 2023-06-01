using ESM.Core;
using ESM.Modules.Import.ViewModels;
using ESM.Modules.Import.ViewModels.Add;
using ESM.Modules.Import.ViewModels.Edit;
using ESM.Modules.Import.Views;
using ESM.Modules.Import.Views.Add;
using ESM.Modules.Import.Views.Edit;
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

            _regionManager.RegisterViewWithRegion(RegionNames.InnerAccountManageRegion, typeof(AccountEdit));
            _regionManager.RegisterViewWithRegion(RegionNames.InnerAccountManageRegion, typeof(AccountAdd));

            _regionManager.RegisterViewWithRegion(RegionNames.InnerProviderManageRegion, typeof(ProviderEdit));
            _regionManager.RegisterViewWithRegion(RegionNames.InnerProviderManageRegion, typeof(ProviderAdd));

            _regionManager.RegisterViewWithRegion(RegionNames.InnerLaptopManageRegion, typeof(LaptopEdit));
            _regionManager.RegisterViewWithRegion(RegionNames.InnerLaptopManageRegion, typeof(LaptopAdd));

            _regionManager.RegisterViewWithRegion(RegionNames.InnerMonitorManageRegion, typeof(MonitorEdit));
            _regionManager.RegisterViewWithRegion(RegionNames.InnerMonitorManageRegion, typeof(MonitorAdd));

            _regionManager.RegisterViewWithRegion(RegionNames.InnerPCManageRegion, typeof(PCAdd));
            _regionManager.RegisterViewWithRegion(RegionNames.InnerPCManageRegion, typeof(PCEdit));

            _regionManager.RegisterViewWithRegion(RegionNames.InnerHardDiskManageRegion, typeof(HardDiskAdd));
            _regionManager.RegisterViewWithRegion(RegionNames.InnerHardDiskManageRegion, typeof(HardDiskEdit));

            _regionManager.RegisterViewWithRegion(RegionNames.InnerCPUManageRegion, typeof(CPUAdd));
            _regionManager.RegisterViewWithRegion(RegionNames.InnerCPUManageRegion, typeof(CPUEdit));

            _regionManager.RegisterViewWithRegion(RegionNames.InnerSmartphoneManageRegion, typeof(SmartphoneAdd));
            _regionManager.RegisterViewWithRegion(RegionNames.InnerSmartphoneManageRegion, typeof(SmartphoneEdit));

            _regionManager.RegisterViewWithRegion(RegionNames.InnerVGAManageRegion, typeof(VGAAdd));
            _regionManager.RegisterViewWithRegion(RegionNames.InnerVGAManageRegion, typeof(VGAEdit));

            _regionManager.RegisterViewWithRegion(RegionNames.InnerDiscountManageRegion, typeof(DiscountAdd));
            _regionManager.RegisterViewWithRegion(RegionNames.InnerDiscountManageRegion, typeof(DiscountEdit));

            _regionManager.RegisterViewWithRegion(RegionNames.InnerComboManageRegion, typeof(ComboAdd));
            _regionManager.RegisterViewWithRegion(RegionNames.InnerComboManageRegion, typeof(ComboEdit));

            _regionManager.RegisterViewWithRegion("TabRegion", typeof(HardDiskInputView));
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(LaptopInputView));
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(PCInputView));
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(MonitorInputView));
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(PhoneInputView));
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(CPUInputView));
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(VGAInputView));
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(ComboInputView));

            _regionManager.RegisterViewWithContentRegion<ProductInputView>();
            _regionManager.RegisterViewWithContentRegion<AccountManage>();
            _regionManager.RegisterViewWithContentRegion<DiscountInputView>();
            _regionManager.RegisterViewWithContentRegion<ProviderView>();

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ProductInputView, ProductInputViewModel>(ViewNames.ProductInputView);
            containerRegistry.RegisterForNavigation<AccountManage, AccountManageViewModel>(ViewNames.AccountManage);
            containerRegistry.RegisterForNavigation<DiscountInputView, DiscountInputViewModel>(ViewNames.ProductManagement);
            containerRegistry.RegisterForNavigation<ProviderView, ProviderViewModel>(ViewNames.ProviderView);

            containerRegistry.RegisterForNavigation<AccountEdit, AccountEditViewModel>(ViewNames.AccountEdit);
            containerRegistry.RegisterForNavigation<AccountAdd, AccountAddViewModel>(ViewNames.AccountAdd);

            containerRegistry.RegisterForNavigation<ProviderEdit, ProviderEditViewModel>(ViewNames.ProviderEdit);
            containerRegistry.RegisterForNavigation<ProviderAdd, ProviderAddViewModel>(ViewNames.ProviderAdd);

            containerRegistry.RegisterForNavigation<LaptopEdit, LaptopEditViewModel>(ViewNames.LaptopEdit);
            containerRegistry.RegisterForNavigation<LaptopAdd, LaptopAddViewModel>(ViewNames.LaptopAdd);

            containerRegistry.RegisterForNavigation<MonitorEdit, MonitorEditViewModel>(ViewNames.MonitorEdit);
            containerRegistry.RegisterForNavigation<MonitorAdd, MonitorAddViewModel>(ViewNames.MonitorAdd);

            containerRegistry.RegisterForNavigation<PCEdit, PCEditViewModel>(ViewNames.PCEdit);
            containerRegistry.RegisterForNavigation<PCAdd, PCAddViewModel>(ViewNames.PCAdd);

            containerRegistry.RegisterForNavigation<HardDiskEdit, HardDiskEditViewModel>(ViewNames.HardDiskEdit);
            containerRegistry.RegisterForNavigation<HardDiskAdd, HardDiskAddViewModel>(ViewNames.HardDiskAdd);

            containerRegistry.RegisterForNavigation<CPUEdit, CPUEditViewModel>(ViewNames.CPUEdit);
            containerRegistry.RegisterForNavigation<CPUAdd, CPUAddViewModel>(ViewNames.CPUAdd);

            containerRegistry.RegisterForNavigation<SmartphoneEdit, SmartphoneEditViewModel>(ViewNames.SmartphoneEdit);
            containerRegistry.RegisterForNavigation<SmartphoneAdd, SmartphoneAddViewModel>(ViewNames.SmartphoneAdd);

            containerRegistry.RegisterForNavigation<VGAEdit, VGAEditViewModel>(ViewNames.VGAEdit);
            containerRegistry.RegisterForNavigation<VGAAdd, VGAAddViewModel>(ViewNames.VGAAdd);

            containerRegistry.RegisterForNavigation<DiscountEdit, DiscountEditViewModel>(ViewNames.DiscountEdit);
            containerRegistry.RegisterForNavigation<DiscountAdd, DiscountAddViewModel>(ViewNames.DiscountAdd);

            containerRegistry.RegisterForNavigation<ComboEdit, ComboEditViewModel>(ViewNames.ComboEdit);
            containerRegistry.RegisterForNavigation<ComboAdd, ComboAddViewModel>(ViewNames.ComboAdd);

        }
    }
}