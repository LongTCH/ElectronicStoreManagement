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
            _regionManager.RegisterViewWithRegion("ReportTabRegion", typeof(ReportView)); 
            _regionManager.RegisterViewWithRegion("ReportTabRegion", typeof(TopSellingReport));
            _regionManager.RegisterViewWithRegion("ReportTabRegion", typeof(Revenue));

            _regionManager.RegisterViewWithContentRegion<SellView>();
            _regionManager.RegisterViewWithContentRegion<ComboSellView>();
            _regionManager.RegisterViewWithContentRegion<TabChartView>();
            _regionManager.RegisterViewWithContentRegion<ImportBillView>();
            
            _regionManager.RegisterViewWithContentRegion<Invoicemanagement>();
            _regionManager.RegisterViewWithContentRegion<ImportInvoice>();
            _regionManager.RegisterViewWithContentRegion<InvoiceComboMangament>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
            containerRegistry.RegisterForNavigation<TabChartView, TabChartViewModel>(ViewNames.TabChartView);
            containerRegistry.RegisterForNavigation<ImportBillView, ImportBillViewModel>(ViewNames.ImportBillView);
            containerRegistry.RegisterForNavigation<SellView, SellViewModel>(ViewNames.SellView);
            containerRegistry.RegisterForNavigation<ComboSellView, ComboSellViewModel>(ViewNames.ComboSellView);

            containerRegistry.RegisterForNavigation<Invoicemanagement, InvoiceManagementViewModel>(ViewNames.Invoicemanagement);
            containerRegistry.RegisterForNavigation<ImportInvoice, ImportInvoiceViewModel>(ViewNames.ImportInvoice);
            containerRegistry.RegisterForNavigation<InvoiceComboMangament, InvoiceComboMangamentViewModel>(ViewNames.InvoiceComboMangament);
        }
    }
}