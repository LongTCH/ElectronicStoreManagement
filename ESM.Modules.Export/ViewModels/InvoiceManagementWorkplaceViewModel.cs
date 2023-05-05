using ESM.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ESM.Modules.Export.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class InvoiceManagementWorkplaceViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;

        public InvoiceManagementWorkplaceViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new(navigate);
        }
        public DelegateCommand<string> NavigateCommand { get; }
        private void navigate(string path)
        {
            _regionManager.RequestNavigateContentRegionWithTrace(path);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
    }
}
