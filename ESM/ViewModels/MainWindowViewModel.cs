using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Linq;

namespace ESM.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private readonly IModalService _modalService;
        private readonly AccountStore _accountStore;
        public MainWindowViewModel(IRegionManager regionManager,
            IDialogService dialogService,
            IModalService modalService,
            AccountStore accountstore)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            _modalService = modalService;
            _accountStore = accountstore;
            _modalService.Action += () => RaisePropertyChanged(nameof(IsModalOpen));
            NavigateCommand = new(ExecuteNavigateCommand);
            HostCommand = new(ExecuteHostCommand);
            LogoutCommand = new(ExecutelogoutCommand);
            Test = new(test);
            _accountStore.CurrentStoreChanged += () => { RaisePropertyChanged(nameof(IsLoggedIn)); RaisePropertyChanged(nameof(IsNotLoggedIn)); };
        }
        public bool IsLoggedIn => _accountStore.IsLoggedIn;
        public bool IsNotLoggedIn => !_accountStore.IsLoggedIn;
        public DelegateCommand<string> NavigateCommand { get; }
        public DelegateCommand<string> HostCommand { get; }
        public DelegateCommand LogoutCommand { get; }
        private void ExecuteNavigateCommand(string navigationPath)
        {
            if (!string.IsNullOrWhiteSpace(navigationPath))
                _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath);
        }
        public bool IsModalOpen => _regionManager.Regions.ContainsRegionWithName(RegionNames.HostRegion) 
                                    && _regionManager.Regions[RegionNames.HostRegion].ActiveViews.Any();
        private void ExecuteHostCommand(string navigationPath)
        {
            _modalService.ShowModal(ModalType.Error, "Test", "Host");
        }
        private void ExecutelogoutCommand()
        {
            _accountStore.Logout();
            _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.HomeView);
        }
        public DelegateCommand Test { get; }
        private void test()
        { }

        #region DialogService
        private DelegateCommand _showDialogCommand;
        public DelegateCommand ShowDialogCommand =>_showDialogCommand ??= new DelegateCommand(ShowDialog);

        private void ShowDialog()
        {
            var message = "Got you";
            var title = "hiyou";
            //using the dialog service as-is
            _dialogService.ShowDialog("NotificationDialog", new DialogParameters($"message={message}&title={title}"), r =>
            {

            }, "DialogWindow");
        }
        #endregion
    }
}
