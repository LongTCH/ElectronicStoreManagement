﻿using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Core;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Import.Utilities;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ESM.Modules.Import.ViewModels.Add
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class DiscountAddViewModel : BindableBase, IViewModel, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        private readonly ViewModelStore _viewModelStore;

        public DiscountAddViewModel(IUnitOfWork unitOfWork, IModalService modalService, ViewModelStore viewModelStore)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            _viewModelStore = viewModelStore;
            DiscountDetail = new();
            ProductType = new[] { "LAPTOP", "PC", "HARD DISK", "CPU", "MONITOR", "SMARTPHONE", "VGA" };
            AddToDiscountCommand = new(addToDiscountCommand);
            RemoveFromDiscountDetailCommand = new(executeRemove);
        }
        private void executeRemove(SelectableViewModel obj)
        {
            DiscountDetail.Remove(obj);
        }
        private void addToDiscountCommand()
        {
            if (ProductList == null) return;
            foreach (var item in ProductList)
            {
                if (item.IsSelected && !DiscountDetail.Any(x => x.Id == item.Id)) DiscountDetail.Add(item);
            }
        }
        public DelegateCommand AddToDiscountCommand { get; }
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
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _viewModelStore.CurrentViewModel = this;
            Id = await _unitOfWork.Discounts.GetSuggestID();
        }
        private string id;
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        private string name;
        [Required(ErrorMessage = "Không được để trống")]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        private DateTime? startDate;
        [Required(ErrorMessage = "Không được để trống")]
        public DateTime? StartDate
        {
            get => startDate;
            set => SetProperty(ref startDate, value);
        }
        private DateTime? endDate;
        [Required(ErrorMessage = "Không được để trống")]
        public DateTime? EndDate
        {
            get => endDate;
            set => SetProperty(ref endDate, value);
        }
        private double? discount;
        [Range(0, 100, ErrorMessage = "Giá trị khuyến mãi phải nằm từ 0 đến 100")]
        public double? Discount1
        {
            get => discount;
            set => SetProperty(ref discount, value, () => this.ValidateProperty(value, nameof(Discount1)));
        }
        private string ProductIdlist;
        private string GetDiscountDetail()
        {
            List<string> ids = new();
            foreach (var item in DiscountDetail)
            {
                ids.Add(item.Id);
            }
            return string.Join(' ', ids);
        }
        public async Task save()
        {
            if (Name == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tên khuyến mãi", "Cảnh báo");
                return;
            }
            if (StartDate == null || EndDate == null || StartDate >= EndDate)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập thời gian khuyến mãi hợp lệ", "Cảnh báo");
                return;
            }
            if (Discount1 < 0 || Discount1 > 100)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập giá trị khuyến mãi từ 0 đến 100", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                ProductIdlist = GetDiscountDetail();
                Discount p = new()
                {
                    Id = Convert.ToInt32(Id),
                    Discount1 = Discount1,
                    EndDate = EndDate,
                    ProductIdlist = ProductIdlist,
                    Name = Name,
                    StartDate = StartDate,
                };
                bool res = (bool)await _unitOfWork.Discounts.Add(p);
                if (res)
                {
                    _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                    _viewModelStore.ParentViewModal.OnChildViewNotify();
                }
                else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
            }
        }
    }
}
