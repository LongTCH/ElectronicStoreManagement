using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Linq;

namespace ESM.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private readonly IModalService _modalService;
        private readonly AccountStore _accountStore;
        private readonly IApplicationCommand _applicationCommand;
        public MainWindowViewModel(IRegionManager regionManager,
            IDialogService dialogService,
            IModalService modalService,
            AccountStore accountstore,
            IApplicationCommand applicationCommand)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            _modalService = modalService;
            _accountStore = accountstore;
            _applicationCommand = applicationCommand;
            ResetIndexCommand = new(resetIndex);
            _applicationCommand.ResetIndexCommand.RegisterCommand(ResetIndexCommand);
            _applicationCommand.ChangeModalState.RegisterCommand(new DelegateCommand(() => RaisePropertyChanged(nameof(IsModalOpen))));
            NavigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand).ObservesCanExecute(() => CanNavigate);
            HostCommand = new(ExecuteHostCommand);
            LogoutCommand = new(ExecutelogoutCommand);
            GoBackCommand = new(goBack);
            GoForwardCommand = new(goForward);

            Test = new(test);
            _accountStore.CurrentStoreChanged += () =>
            {
                RaisePropertyChanged(nameof(IsLoggedIn));
                RaisePropertyChanged(nameof(IsNotLoggedIn));
                RaisePropertyChanged(nameof(IsAdmin));
                RaisePropertyChanged(nameof(IsSellStaff));
                RaisePropertyChanged(nameof(IsTypingStaff));
                if (!_accountStore.IsLoggedIn) _regionManager.ResetTrace();
            };
        }
        private bool CanNavigate { get; set; } = true;
        private int index;
        public int Index
        {
            get => index;
            set => SetProperty(ref index, value);
        }
        public bool IsLoggedIn => _accountStore.IsLoggedIn;
        public bool IsNotLoggedIn => !_accountStore.IsLoggedIn;
        public bool IsAdmin => _accountStore.IsAdmin;
        public bool IsSellStaff => _accountStore.IsSellStaff;
        public bool IsTypingStaff => _accountStore.IsTypingStaff;
        public DelegateCommand<string> NavigateCommand { get; }
        public DelegateCommand<string> HostCommand { get; }
        public DelegateCommand LogoutCommand { get; }
        public DelegateCommand GoBackCommand { get; }
        public DelegateCommand GoForwardCommand { get; }
        public DelegateCommand ResetIndexCommand { get; }
        private void ExecuteNavigateCommand(string navigationPath)
        {
            _regionManager.RequestNavigateContentRegionWithTrace(navigationPath);
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
            Index = 0;
        }
        #region ModalHosting
        private void resetIndex()
        {
            Type view = _regionManager.Regions[RegionNames.ContentRegion].ActiveViews.First().GetType();
            var res = Utilities.StaticData.ViewTypeToHamburgerIndex.TryGetValue(view, out int temp);
            CanNavigate = false;
            if (res) Index = temp; else Index = -1;
            CanNavigate = true;
        }
        private void goBack()
        {
            CanNavigate = false;
            _regionManager.GoBack();
            resetIndex();
            CanNavigate = true;
        }
        private void goForward()
        {
            CanNavigate = false;
            _regionManager.GoForward();
            resetIndex();
            CanNavigate = true;
        }
        #endregion
        public DelegateCommand Test { get; }
        private void test()
        {
            //_modalService.ShowModal(ViewNames.ProductDetailView, null);
            //_regionManager.RequestNavigateContentRegionWithTrace(ViewNames.SellView);
            IUnitOfWork t = new UnitOfWork();
            var l = t.Pcharddisks.GetTopSoldProducts(DateTime.Now.AddYears(-2), DateTime.Now, 10);
        }

        #region DialogService
        private DelegateCommand _showDialogCommand;
        public DelegateCommand ShowDialogCommand => _showDialogCommand ??= new DelegateCommand(ShowDialog);

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
