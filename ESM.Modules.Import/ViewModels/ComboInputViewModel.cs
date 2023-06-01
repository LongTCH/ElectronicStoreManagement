using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Import.Utilities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ESM.Modules.Import.ViewModels
{
    [RegionMemberLifetime(KeepAlive = true)]
    public class ComboInputViewModel : BindableBase, INavigationAware, IParentViewModel, ISearchBar
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IModalService modalService;
        private readonly ViewModelStore _viewModelStore;
        private readonly IRegionManager _regionManager;
        public ComboInputViewModel(IUnitOfWork unitOfWork, IModalService modalService, ViewModelStore viewModelStore, IRegionManager regionManager)
        {
            this.unitOfWork = unitOfWork;
            this.modalService = modalService;
            this._viewModelStore = viewModelStore;
            _regionManager = regionManager;
            ComboDetail = new();

            AddCommand = new(addToComboListCommand);
            CancelCommand = new(clearCommand);
            EditCommand = new(editCommand);
            DeleteCommand = new(async () => await deleteCommand());
            FindCommand = new(findCommand);
            SaveCommand = new(async () => await _viewModelStore.CurrentViewModel.save());
            SearchCommand = new(async (s) => await searchCommand(s));
            GetAllCommand = new(async () => await getAll());
        }
        public string Header => "COMBO";
        private Combo selectedCombo;
        public Combo SelectedCombo
        {
            get => selectedCombo;
            set => SetProperty(ref selectedCombo, value);
        }
        private bool isEditMode;
        public bool IsEditMode
        {
            get => isEditMode;
            set => SetProperty(ref isEditMode, value);
        }
        public IEnumerable<string> ProductType { get; }
        public ICollection<SelectableViewModel> productList;
        public ICollection<SelectableViewModel> ProductList
        {
            get => productList;
            set => SetProperty(ref productList, value);
        }
        private ObservableCollection<SelectableViewModel> comboDetail;
        public ObservableCollection<SelectableViewModel> ComboDetail
        {
            get => comboDetail;
            set => SetProperty(ref comboDetail, value);
        }
        private ObservableCollection<Combo> comboList;
        public ObservableCollection<Combo> ComboList
        {
            get => comboList;
            set => SetProperty(ref comboList, value);
        }

        
        public DelegateCommand AddCommand { get; }

        public DelegateCommand AddToComboListCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand FindCommand { get; }
        public DelegateCommand EditCommand { get; }
        public DelegateCommand<string> SearchCommand { get; }
        public DelegateCommand GetAllCommand { get; }
        private async Task searchCommand(string keyword)
        {
            ComboList = new(await unitOfWork.Combos.SearchProduct(keyword));
        }
        private void editCommand()
        {
            if (SelectedCombo != null)
            {
                IsEditMode = true;
                _regionManager.RequestNavigate(RegionNames.InnerComboManageRegion, ViewNames.ComboEdit, new NavigationParameters()
                {
                    {"comboId", SelectedCombo.Id}
                });
            }
            else modalService.ShowModal(ModalType.Error, "Bạn chưa chọn Combo", "Thông báo");
        }
        private async Task deleteCommand()
        {
            if (SelectedCombo == null) return;
            bool res = false;
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                res = (bool)await unitOfWork.Combos.Delete(SelectedCombo.Id);
            }
            if (res)
            {
                modalService.ShowModal(ModalType.Information, "Đã xóa", "Thông báo");
                reset();
            }
            else modalService.ShowModal(ModalType.Information, "Xóa không thành công", "Thông báo");
        }
        
        private void addToComboListCommand()
        {
            IsEditMode = true;
            _regionManager.RequestNavigate(RegionNames.InnerComboManageRegion, ViewNames.ComboAdd);
        }
        private void clearCommand()
        {
            if (SelectedCombo != null)
            {
                MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
                if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn hủy bỏ thông tin?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
                {
                    reset();
                }
            }
        }

        private async void reset()
        {
            SelectedCombo = null;
            ComboDetail.Clear();
            ProductList = null;
            IsEditMode = false;
            await getAll();
        }
        private async Task getAll()
        {
            ComboList = new(await unitOfWork.Combos.GetAll());
        }
        private async void findCommand()
        {
            if (SelectedCombo == null) return;
            ComboDetail.Clear();
            var list = await unitOfWork.Combos.GetListProduct(SelectedCombo);
            foreach (var item in list)
            {
                ComboDetail.Add(new()
                {
                    Id = item.Id,
                    Discount = item.Discount,
                    IsSelected = true,
                    Name = item.Name,
                    Price = item.Price,
                    Unit = item.Unit,
                    Remain = item.Remain,
                });
            }
        }
       
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _viewModelStore.ParentViewModal = this;
            reset();
        }

        public void OnChildViewNotify()
        {
            IsEditMode = false;
            reset();
        }
    }
}
