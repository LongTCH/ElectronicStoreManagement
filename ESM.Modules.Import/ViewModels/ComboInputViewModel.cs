using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Import.Utilities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ESM.Modules.Import.ViewModels
{
    public class ComboInputViewModel : BindableBase, INavigationAware
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IModalService modalService;

        public ComboInputViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            this.unitOfWork = unitOfWork;
            this.modalService = modalService;
            ComboDetail = new();
            ProductType = new[] { "LAPTOP", "PC", "HARD DISK", "CPU", "MONITOR", "SMARTPHONE", "VGA" };
            AddCommand = new(addToComboListCommand);
            AddToComboCommand = new(addToComboCommand);
            RemoveFromComboDetailCommand = new(executeRemove);
            AddToComboListCommand = new(addToComboListCommand);
            CancelCommand = new(clearCommand);
            EditCommand = new(editCommand);
            DeleteCommand = new(async () => await deleteCommand());
            FindCommand = new(findCommand);
            SaveCommand = new(async () => await saveCommand());
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

        private string selectedProductType;
        public string SelectedProductType
        {
            get => selectedProductType;
            set
            {
                SetProperty(ref selectedProductType, value);
                SetProductList();
            }
        }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand AddToComboCommand { get; }
        public DelegateCommand AddToComboListCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand FindCommand { get; }
        public DelegateCommand EditCommand { get; }
        public DelegateCommand<string> SearchCommand { get; }
        public DelegateCommand GetAllCommand { get; }
        public DelegateCommand<SelectableViewModel> RemoveFromComboDetailCommand { get; }
        private async Task searchCommand(string keyword)
        {
            ComboList = new(await unitOfWork.Combos.SearchProduct(keyword));
        }
        private void editCommand()
        {
            if (SelectedCombo != null)
                IsEditMode = true;
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
        private void addToComboCommand()
        {
            if (ProductList == null) return;
            foreach (var item in ProductList)
            {
                if (item.IsSelected && !ComboDetail.Any(x => x.Id == item.Id)) ComboDetail.Add(item);
            }
        }
        private async Task saveCommand()
        {
            if (SelectedCombo == null) return;
            if (SelectedCombo.Name == null || SelectedCombo.Unit == null)
            {
                modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                FillCombo();
                bool res;
                if (await unitOfWork.Combos.IsIdExist(SelectedCombo.Id))
                {
                    res = (bool)await unitOfWork.Combos.Update(SelectedCombo);
                    if (res)
                    {
                        modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                    }
                    else modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    res = (bool)await unitOfWork.Combos.Add(SelectedCombo);
                    if (res)
                    {
                        modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                    }
                    else modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
                }
                if (res)
                {
                    reset();
                }
            }
        }
        private async void addToComboListCommand()
        {
            ComboDetail.Clear();
            IsEditMode = true;
            var p = new Combo();
            p.Id = await GetNewID();
            SelectedCombo = p;
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
            SelectedProductType = null;
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
        private async void FillCombo()
        {
            List<string> ids = new();
            foreach (var item in ComboDetail)
            {
                ids.Add(item.Id);
            }
            SelectedCombo.ProductIdlist = string.Join(' ', ids);
            SelectedCombo.Price = await unitOfWork.Combos.GetComboPrice(SelectedCombo);
        }
        protected async Task<string> GetNewID()
        {
            var previousID = ComboList.OrderBy(x => x.Id).LastOrDefault()?.Id;
            if (previousID == null) return DAStaticData.IdPrefix[DataAccess.ProductType.COMBO] + "0000000";
            int counter = Convert.ToInt32(previousID[2..]);
            string cs = await unitOfWork.Combos.GetSuggestID();
            counter = Math.Max(counter, int.Parse(cs[2..]));
            ++counter;
            return DAStaticData.IdPrefix[DataAccess.ProductType.COMBO] + counter.ToString().PadLeft(7, '0');
        }
        private async void SetProductList()
        {
            switch (SelectedProductType)
            {
                case "LAPTOP":
                    ProductList = (await unitOfWork.Laptops.GetAll()).Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        Remain = x.Remain,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "PC":
                    ProductList = (await unitOfWork.Pcs.GetAll()).Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        Remain = x.Remain,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "HARD DISK":
                    ProductList = (await unitOfWork.Pcharddisks.GetAll()).Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        Remain = x.Remain,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "CPU":
                    ProductList = (await unitOfWork.Pccpus.GetAll()).Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Remain = x.Remain,
                        Unit = x.Unit,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "MONITOR":
                    ProductList = (await unitOfWork.Monitors.GetAll()).Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        Remain = x.Remain,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "SMARTPHONE":
                    ProductList = (await unitOfWork.Smartphones.GetAll()).Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        Remain = x.Remain,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "VGA":
                    ProductList = (await unitOfWork.Vgas.GetAll()).Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        Remain = x.Remain,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
            }
        }
        private void executeRemove(SelectableViewModel obj)
        {
            ComboDetail.Remove(obj);
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
            reset();
        }
    }
}
