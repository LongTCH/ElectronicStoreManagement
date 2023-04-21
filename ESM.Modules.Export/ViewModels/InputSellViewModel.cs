using Prism.Mvvm;
using Prism.Regions;

namespace ESM.Modules.Export.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class InputSellViewModel : BindableBase, INavigationAware
    {
        public InputSellViewModel()
        {

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
