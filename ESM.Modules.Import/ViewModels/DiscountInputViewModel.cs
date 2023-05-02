using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Prism.Mvvm;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ESM.Modules.Import.Utilities;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using ESM.Core;
using System.ComponentModel.DataAnnotations;
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
            ProductType = new[] { "LAPTOP", "PC", "HARD DISK", "CPU", "MONITOR", "SMARTPHONE", "VGA" };
            AddCommand = new(addToDiscountListCommand);
            AddToDiscountCommand = new(addToDiscountCommand);
            RemoveFromDiscountDetailCommand = new(executeRemove);
            AddToDiscountListCommand = new(addToDiscountListCommand);
            CancelCommand = new(clearCommand);
            DeleteCommand = new(deleteCommand);
            FindCommand = new(findCommand);
            SaveCommand = new(async () => await saveCommand());
        }
        async Task Init()
        {
            Laptops = await _unitOfWork.Laptops.GetAll();
            Monitors = await _unitOfWork.Monitors.GetAll();
            Pcs = await _unitOfWork.Pcs.GetAll();
            HardDisks = await _unitOfWork.Pcharddisks.GetAll();
            Smartphones = await _unitOfWork.Smartphones.GetAll();
            Vgas = await _unitOfWork.Vgas.GetAll();
            CPUs = await _unitOfWork.Pccpus.GetAll();
            DiscountDetail = new();
            ProductList = Array.Empty<SelectableViewModel>();
            DiscountList = new();
        }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand AddToDiscountCommand { get; }
        public DelegateCommand AddToDiscountListCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand<Discount> FindCommand { get; }
        public DelegateCommand<SelectableViewModel> RemoveFromDiscountDetailCommand { get; }
        public IEnumerable<string> ProductType { get; }
        public Discount CurrentDiscount { get; set; }
        private IEnumerable<Laptop> Laptops;
        private IEnumerable<Monitor> Monitors;
        private IEnumerable<Pc> Pcs;
        private IEnumerable<Pcharddisk> HardDisks;
        private IEnumerable<Vga> Vgas;
        private IEnumerable<Smartphone> Smartphones;
        private IEnumerable<Pccpu> CPUs;
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
        private double? discount;
        [Range(0, 100)]
        public double? Discount
        {
            get => discount;
            set => SetProperty(ref discount, value, () => this.ValidateProperty(value, nameof(Discount)));
        }
        private string discountName;
        public string DiscountName
        {
            get => discountName?.Trim();
            set => SetProperty(ref discountName, value);
        }
        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;
            set => SetProperty(ref startDate, value);
        }
        private DateTime endDate;
        public DateTime EndDate
        {
            get => endDate;
            set => SetProperty(ref endDate, value);
        }
        private void SetProductList()
        {
            switch (SelectedProductType)
            {
                case "LAPTOP":
                    ProductList = Laptops.Select(x => new SelectableViewModel()
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
                    ProductList = Pcs.Select(x => new SelectableViewModel()
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
                    ProductList = HardDisks.Select(x => new SelectableViewModel()
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
                    ProductList = CPUs.Select(x => new SelectableViewModel()
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
                    ProductList = Monitors.Select(x => new SelectableViewModel()
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
                    ProductList = Smartphones.Select(x => new SelectableViewModel()
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
                    ProductList = Vgas.Select(x => new SelectableViewModel()
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
        private async void deleteCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (CurrentDiscount.InMemory) await _unitOfWork.Discounts.Delete(CurrentDiscount.Id.ToString());
                DiscountList.Remove(CurrentDiscount);
                Empty();
            }
        }
        private void addToDiscountCommand()
        {
            foreach (var item in ProductList)
            {
                if (item.IsSelected && !DiscountDetail.Any(x => x.Id == item.Id)) DiscountDetail.Add(item);
            }
        }
        private async Task saveCommand()
        {
            if (CurrentDiscount == null) return;
            if (DiscountName == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tên khuyến mãi", "Cảnh báo");
                return;
            }
            if (StartDate >= EndDate)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập thời gian khuyến mãi hợp lệ", "Cảnh báo");
                return;
            }
            if (Discount < 0 || Discount > 100)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập giá trị khuyến mãi từ 0 đến 100", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                var p = GetDiscount(CurrentDiscount.Id);
                if (CurrentDiscount.InMemory)
                {
                    var res = await _unitOfWork.Discounts.Update(p);
                    if ((bool)res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");

                        var index = DiscountList.IndexOf(CurrentDiscount);
                        DiscountList.RemoveAt(index);
                        DiscountList.Insert(index, p);
                        // Clear
                        Empty();
                    }
                    else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    var res = (int)await _unitOfWork.Discounts.Add(p);
                    if (res > 0)
                    {
                        _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                        // Clear
                        Empty();
                        var index = DiscountList.IndexOf(DiscountList.Where(x => x.Id == p.Id).First());
                        DiscountList.RemoveAt(index);
                        DiscountList.Insert(index, p);
                    }
                    else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
                    await Task.CompletedTask;
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
            findCommand(discount);
        }
        private void clearCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn hủy bỏ thông tin?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                clear();
            }
        }
        private async void clear()
        {
            Discount Discount = null;
            if (CurrentDiscount != null)
            {
                Discount = await _unitOfWork.Discounts.GetById(CurrentDiscount.Id.ToString());
            }

            Empty();
            if (Discount != null) findCommand(Discount);
        }
        private void Empty()
        {
            CurrentDiscount = null;
            Discount = 0;
            DiscountDetail = new();
            DiscountName = null;
            Discount = 0;
            StartDate = DateTime.Now;
            EndDate = StartDate.AddMonths(1);
        }
        private async void findCommand(Discount discount)
        {
            if (discount == null) return;
            CurrentDiscount = discount;
            DiscountDetail.Clear();
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
            DiscountName = discount.Name;
            Discount = discount.Discount1 == null ? 0 : discount.Discount1;
        }
        private Discount GetDiscount(int id)
        {
            Discount discount = new();
            List<string> ids = new();
            foreach (var item in DiscountDetail)
            {
                ids.Add(item.Id);
            }
            discount.Id = id;
            discount.Discount1 = Discount;
            discount.Name = DiscountName;
            discount.StartDate = StartDate;
            discount.EndDate = EndDate;
            discount.ProductIdlist = string.Join(' ', ids);
            return discount;
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
            clear();
            await Init();
            DiscountList = new(await _unitOfWork.Discounts.GetAll());
            Discount = 0;
        }
    }
}
