using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Prism.Mvvm;
using System.Threading.Tasks;
using System.Collections.Generic;
using ESM.Modules.Import.Utilities;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Windows;
using Prism.Regions;

namespace ESM.Modules.Import.ViewModels
{
    public class DiscountInputViewModel : BindableBase, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;

        public DiscountInputViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            DiscountDetail = new();
            ProductType = new[] { "LAPTOP", "PC", "HARD DISK", "CPU", "MONITOR", "SMARTPHONE", "VGA" };
            AddCommand = new(addToDiscountListCommand);
            AddToDiscountCommand = new(addToDiscountCommand);
            RemoveFromDiscountDetailCommand = new(executeRemove);
            AddToDiscountListCommand = new(addToDiscountListCommand); 
            EditCommand = new(editCommand);
            CancelCommand = new(clearCommand);
            DeleteCommand = new(async () => await deleteCommand());
            FindCommand = new(findCommand);
            SaveCommand = new(async () => await saveCommand());
        }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand EditCommand { get; }
        public DelegateCommand AddToDiscountCommand { get; }
        public DelegateCommand AddToDiscountListCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand FindCommand { get; }
        public DelegateCommand<SelectableViewModel> RemoveFromDiscountDetailCommand { get; }
        private Discount selectedDiscount;
        public Discount SelectedDiscount
        {
            get => selectedDiscount;
            set => SetProperty(ref selectedDiscount, value);
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
        private ObservableCollection<SelectableViewModel> discountDetail;
        public ObservableCollection<SelectableViewModel> DiscountDetail
        {
            get => discountDetail;
            set => SetProperty(ref discountDetail, value);
        }
        private ObservableCollection<Discount> discountList;
        public ObservableCollection<Discount> DiscountList
        {
            get => discountList;
            set => SetProperty(ref discountList, value);
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
        private async void SetProductList()
        {
            switch (SelectedProductType)
            {
                case "LAPTOP":
                    ProductList = (await _unitOfWork.Laptops.GetAll()).Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        Remain = x.Remain,
                        IsSelected = DiscountDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "PC":
                    ProductList = (await _unitOfWork.Pcs.GetAll()).Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        Remain = x.Remain,
                        IsSelected = DiscountDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "HARD DISK":
                    ProductList = (await _unitOfWork.Pcharddisks.GetAll()).Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        Remain = x.Remain,
                        IsSelected = DiscountDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "CPU":
                    ProductList = (await _unitOfWork.Pccpus.GetAll()).Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Remain = x.Remain,
                        Unit = x.Unit,
                        IsSelected = DiscountDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "MONITOR":
                    ProductList = (await _unitOfWork.Monitors.GetAll()).Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        Remain = x.Remain,
                        IsSelected = DiscountDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "SMARTPHONE":
                    ProductList = (await _unitOfWork.Smartphones.GetAll()).Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        Remain = x.Remain,
                        IsSelected = DiscountDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "VGA":
                    ProductList = (await _unitOfWork.Vgas.GetAll()).Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        Remain = x.Remain,
                        IsSelected = DiscountDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
            }
        }
        private void editCommand()
        {
            if (SelectedDiscount != null)
                IsEditMode = true;
            else _modalService.ShowModal(ModalType.Error, "Bạn chưa chọn Khuyến mãi", "Thông báo");
        }
        private async Task deleteCommand()
        {
            if (SelectedDiscount == null) return;
            bool res = false;
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                res = (bool)await _unitOfWork.Discounts.Delete(SelectedDiscount.Id + "");
            }
            if (res)
            {
                _modalService.ShowModal(ModalType.Information, "Đã xóa", "Thông báo");
                reset();
            }
            else _modalService.ShowModal(ModalType.Information, "Xóa không thành công", "Thông báo");
        }
        private void addToDiscountCommand()
        {
            if (ProductList == null) return;
            foreach (var item in ProductList)
            {
                if (item.IsSelected && !DiscountDetail.Any(x => x.Id == item.Id)) DiscountDetail.Add(item);
            }
        }
        private async Task saveCommand()
        {
            if (SelectedDiscount == null) return;
            if (SelectedDiscount.Name == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tên khuyến mãi", "Cảnh báo");
                return;
            }
            if (SelectedDiscount.StartDate == null || SelectedDiscount.EndDate == null || SelectedDiscount.StartDate >= SelectedDiscount.EndDate)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập thời gian khuyến mãi hợp lệ", "Cảnh báo");
                return;
            }
            if (SelectedDiscount.Discount1 < 0 || SelectedDiscount.Discount1 > 100)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập giá trị khuyến mãi từ 0 đến 100", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                SelectedDiscount.ProductIdlist = GetDiscountDetail();
                bool res;
                if (await _unitOfWork.Discounts.IsIdExist(SelectedDiscount.Id + ""))
                {
                    res = (bool)await _unitOfWork.Discounts.Update(SelectedDiscount);
                    if (res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                    }
                    else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    res = (bool)await _unitOfWork.Discounts.Add(SelectedDiscount);
                    if (res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                    }
                    else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
                    await Task.CompletedTask;
                }
                if (res)
                {
                    reset();
                }
            }
        }
        private void addToDiscountListCommand()
        {
            DiscountDetail.Clear();
            IsEditMode = true;
            var p = new Discount();
            p.Id = GetNewID();
            SelectedDiscount = p;
        }
        private void clearCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn hủy bỏ thông tin?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                reset();
            }
        }
        private async void findCommand()
        {
            if (SelectedDiscount == null) return;
            DiscountDetail?.Clear();
            var list = await _unitOfWork.Discounts.GetListProduct(SelectedDiscount);
            foreach (var item in list)
            {
                DiscountDetail.Add(new()
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
        private string GetDiscountDetail()
        {
            List<string> ids = new();
            foreach (var item in DiscountDetail)
            {
                ids.Add(item.Id);
            }
            return string.Join(' ', ids);
        }
        private int GetNewID()
        {
            var p = DiscountList.OrderBy(x => x.Id).LastOrDefault();
            if (p == null) return 1;
            return p.Id + 1;
        }
        private void executeRemove(SelectableViewModel obj)
        {
            DiscountDetail.Remove(obj);
        }
        private async void reset()
        {
            SelectedDiscount = null;
            SelectedProductType = null;
            DiscountDetail.Clear();
            ProductList = null;
            IsEditMode = false;
            DiscountList = new(await _unitOfWork.Discounts.GetAll());
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
