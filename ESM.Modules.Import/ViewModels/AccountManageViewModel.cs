using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using ESM.Core.ShareStores;
using System.Linq;
using ESM.Modules.Import.Utilities;

namespace ESM.Modules.Import.ViewModels
{
    public class AccountManageViewModel : BindableBase, INavigationAware, IParentViewModel, ISearchBar
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        private readonly IRegionManager _regionManager;
        private readonly ViewModelStore _accountViewModelStore;
        public AccountManageViewModel(IUnitOfWork unitOfWork,
            IModalService modalService,
            IRegionManager regionManager,
            ViewModelStore accountViewModelStore)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            _regionManager = regionManager;
            _accountViewModelStore = accountViewModelStore;
            SaveCommand = new(async () => await _accountViewModelStore.CurrentViewModel?.save());
            EditCommand = new(editCommand);
            ClearCommand = new(clearCommand);
            AddCommand = new(addCommand);
            DeleteCommand = new(deleteCommand);
            SearchCommand = new(async (s) => await searchCommand(s));
            GetAllCommand = new(getAccountList);
            MapRoleCommand = new(mapRoleCommand);
        }
        private bool isEditMode;
        public bool IsEditMode
        {
            get => isEditMode;
            set => SetProperty(ref isEditMode, value);
        }
        private Account selectedAccount;
        public Account SelectedAccount
        {
            get => selectedAccount;
            set => SetProperty(ref selectedAccount, value);
        }
        private ObservableCollection<Account> accountList;
        public ObservableCollection<Account> AccountList
        {
            get => accountList;
            set => SetProperty(ref accountList, value);
        }

        private string? role;
        public string? Role
        {
            get => role;
            set => SetProperty(ref role, value);
        }
        private void mapRoleCommand()
        {
            if (SelectedAccount != null)
            {
                Role = ListRole.ElementAt(SelectedAccount.Id[0] - '0');
            }
            else Role = null;
        }
        private void editCommand()
        {
            if (SelectedAccount != null)
            {
                IsEditMode = true;
                NavigationParameters keyValuePairs = new()
                {
                    { "accountId" , SelectedAccount.Id },
                };
                _regionManager.RequestNavigate(RegionNames.InnerAccountManageRegion, ViewNames.AccountEdit, keyValuePairs);
            }
            else _modalService.ShowModal(ModalType.Error, "Bạn chưa chọn tài khoản", "Thông báo");
        }
        private void clearCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn hủy bỏ thông tin?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                SelectedAccount = null;
                IsEditMode = false;
                getAccountList();
            }

        }
        private void addCommand()
        {
            IsEditMode = true;
            _regionManager.RequestNavigate(RegionNames.InnerAccountManageRegion, ViewNames.AccountAdd);
        }
        private async void deleteCommand()
        {
            if (SelectedAccount is null) return;
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                await _unitOfWork.Accounts.Delete(SelectedAccount.Id);
                _modalService.ShowModal(ModalType.Information, "Đã xóa", "Thông báo");
                getAccountList();
            }
        }
        private async Task searchCommand(string keyword)
        {
            AccountList = new(await _unitOfWork.Accounts.SearchAccounts(keyword));
        }
        private async void getAccountList()
        {
            AccountList = new(await _unitOfWork.Accounts.GetAll());
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            IsEditMode = false;
            SelectedAccount = null;
            getAccountList();
            _accountViewModelStore.ParentViewModal = this;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnChildViewNotify()
        {
            IsEditMode = false;
            getAccountList();
        }

        public DelegateCommand AddAvatarCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand MapRoleCommand { get; }
        public DelegateCommand ClearCommand { get; }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand EditCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand GetAllCommand { get; }
        public DelegateCommand<string> SearchCommand { get; }
        public IEnumerable<string> Gender { get; } = Core.StaticData.GenderList;
        public IEnumerable<string> ListRole { get; } = Utilities.StaticData.RoleList;
    }
}
