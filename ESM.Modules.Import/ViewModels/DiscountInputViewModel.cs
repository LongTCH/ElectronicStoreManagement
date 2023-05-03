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
using ESM.Core;
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
            CancelCommand = new(clearCommand);
            DeleteCommand = new(async (p) => await deleteCommand(p));
            FindCommand = new(findCommand);
            SaveCommand = new(async (p) => await saveCommand(p));
        }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand AddToDiscountCommand { get; }
        public DelegateCommand AddToDiscountListCommand { get; }
        public DelegateCommand<Discount> CancelCommand { get; }
        public DelegateCommand<Discount> SaveCommand { get; }
        public DelegateCommand<Discount> DeleteCommand { get; }
        public DelegateCommand<Discount> FindCommand { get; }
        public DelegateCommand<SelectableViewModel> RemoveFromDiscountDetailCommand { get; }
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
                selectedProductType = value;
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
                default:
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
        private async Task deleteCommand(Discount discount)
        {
            if (discount == null) return;
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (discount.InMemory) await _unitOfWork.Discounts.Delete(discount.Id.ToString());
                DiscountList.Remove(discount);
                DiscountDetail.Clear();
                DiscountList.Refresh();
            }
        }
        private void addToDiscountCommand()
        {
            if (ProductList == null) return;
            foreach (var item in ProductList)
            {
                if (item.IsSelected && !DiscountDetail.Any(x => x.Id == item.Id)) DiscountDetail.Add(item);
            }
        }
        private async Task saveCommand(Discount discount)
        {
            if (discount == null) return;
            if (discount.Name == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tên khuyến mãi", "Cảnh báo");
                return;
            }
            if (discount.StartDate == null || discount.EndDate == null || discount.StartDate >= discount.EndDate)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập thời gian khuyến mãi hợp lệ", "Cảnh báo");
                return;
            }
            if (discount.Discount1 < 0 || discount.Discount1 > 100)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập giá trị khuyến mãi từ 0 đến 100", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                discount.ProductIdlist = GetDiscountDetail();
                bool res;
                if (discount.InMemory)
                {
                    res = (bool)await _unitOfWork.Discounts.Update(discount);
                    if (res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                    }
                    else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    res = (bool)await _unitOfWork.Discounts.Add(discount);
                    if (res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                    }
                    else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
                    await Task.CompletedTask;
                }
                if (res)
                {
                    discount.InMemory = true;
                    DiscountList.Refresh();
                }
            }
        }
        private void addToDiscountListCommand()
        {
            Discount discount = new()
            {
                Id = GetNewID(),
                InMemory = false
            };
            DiscountList.Add(discount);
        }
        private async void clearCommand(Discount discount)
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn hủy bỏ thông tin?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (discount.InMemory) discount = await _unitOfWork.Discounts.GetById(discount.Id.ToString());
                else discount = new()
                {
                    Id = discount.Id,
                    InMemory = false
                };
                DiscountList[DiscountList.IndexOf(discount)] = discount;
                DiscountDetail?.Clear();
                DiscountList.Refresh();
            }
        }
        private async void findCommand(Discount discount)
        {
            if (discount == null) return;
            DiscountDetail?.Clear();
            var list = await _unitOfWork.Discounts.GetListProduct(discount);
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
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            DiscountList = new(await _unitOfWork.Discounts.GetAll());
        }
    }
}
