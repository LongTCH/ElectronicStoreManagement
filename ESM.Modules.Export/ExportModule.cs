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
            _regionManager.RegisterViewWithContentRegion<TabChartView>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SellView, SellViewModel>(ViewNames.SellView);
            containerRegistry.RegisterForNavigation<TabChartView, TabChartViewModel>(ViewNames.TabChartView);
        }
    }
}