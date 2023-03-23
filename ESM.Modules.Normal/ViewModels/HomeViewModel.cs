using ESM.Core;
using ESM.Core.ShareServices;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ESM.Modules.Normal.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IApplicationCommand _applicationCommand;
        public HomeViewModel(IRegionManager regionManager,
            IApplicationCommand applicationCommand)
        {
            _regionManager = regionManager;
            _applicationCommand = applicationCommand;
            NavigateCommand = new(ExecuteNavigateCommand);
        }
        public DelegateCommand<string> NavigateCommand { get; }
        private void ExecuteNavigateCommand(string navigationPath)
        {
            _regionManager.RequestNavigateContentRegionWithTrace(navigationPath);
            _applicationCommand.ResetIndexCommand.Execute(true);
        }
    }
}
