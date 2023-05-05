using ESM.Core;
using ESM.Core.ShareStores;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ESM.Modules.Import.ViewModels
{
    public class ProductManagementViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly AccountStore _accountStores;
        public ProductManagementViewModel(IRegionManager regionManager, AccountStore accountStore)
        {
            _regionManager = regionManager;
            _accountStores = accountStore;
            NavigateCommand = new(navigate);
        }
        public bool IsAdmin => _accountStores.IsAdmin;
        public DelegateCommand<string> NavigateCommand { get; }
        private void navigate(string path)
        {
            _regionManager.RequestNavigateContentRegionWithTrace(path);
        }
    }
}
