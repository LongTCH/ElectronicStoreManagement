using ESM.Core;
using ESM.Modules.Export.ViewModels;
using ESM.Modules.Export.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ESM.Modules.Export
{
    public class ExportModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ExportModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithContentRegion<SellView>();

            _regionManager.RegisterViewWithRegion("ReportTabRegion", typeof(ReportView)); 
            _regionManager.RegisterViewWithRegion("ReportTabRegion", typeof(TopSellingReport));
            _regionManager.RegisterViewWithRegion("ReportTabRegion", typeof(Revenue));
            _regionManager.RegisterViewWithRegion("SellTabRegion", typeof(SellView));
            _regionManager.RegisterViewWithRegion("SellTabRegion", typeof(ComboSellView));
            _regionManager.RegisterViewWithContentRegion<InputSellView>();
            _regionManager.RegisterViewWithContentRegion<TabChartView>();
            _regionManager.RegisterViewWithContentRegion<ImportBillView>();
            
            _regionManager.RegisterViewWithContentRegion<Invoicemanagement>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<InputSellView, InputSellViewModel>(ViewNames.InputSellView);
            containerRegistry.RegisterForNavigation<TabChartView, TabChartViewModel>(ViewNames.TabChartView);
            containerRegistry.RegisterForNavigation<ImportBillView, ImportBillViewModel>(ViewNames.ImportBillView);

            containerRegistry.RegisterForNavigation<Invoicemanagement, InvoiceManagementViewModel>(ViewNames.Invoicemanagement);
        }
    }
}