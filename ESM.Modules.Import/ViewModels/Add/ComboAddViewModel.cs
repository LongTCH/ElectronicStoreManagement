using ESM.Core.ShareServices;
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
    public class ComboAddViewModel : BindableBase, IViewModel, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        private readonly ViewModelStore _viewModelStore;

        public ComboAddViewModel(IUnitOfWork unitOfWork, IModalService modalService, ViewModelStore viewModelStore)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            _viewModelStore = viewModelStore;
            ComboDetail = new();
            ProductType = new[] { "LAPTOP", "PC", "HARD DISK", "CPU", "MONITOR", "SMARTPHONE", "VGA" };
            AddToComboCommand = new(addToComboCommand);
            RemoveFromComboDetailCommand = new(executeRemove);
        }
        public DelegateCommand AddToComboCommand { get; }
        public DelegateCommand<SelectableViewModel> RemoveFromComboDetailCommand { get; }
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
        private void addToComboCommand()
        {
            if (ProductList == null) return;
            foreach (var item in ProductList)
            {
                if (item.IsSelected && !ComboDetail.Any(x => x.Id == item.Id)) ComboDetail.Add(item);
            }
        }
        private void FillCombo()
        {
            List<string> ids = new();
            foreach (var item in ComboDetail)
            {
                ids.Add(item.Id);
            }
            ProductIdlist = string.Join(' ', ids);
        }

        private void executeRemove(SelectableViewModel obj)
        {
            ComboDetail.Remove(obj);
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
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
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
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
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
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
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
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
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
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
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
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
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
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
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
            Id = await _unitOfWork.Combos.GetSuggestID();
        }
        private string id;
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        private string name;
        [Required]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        private string unit;
        [Required]
        public string Unit
        {
            get => unit;
            set => SetProperty(ref unit, value);
        }
        private decimal price;
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        private double? discount;
        [Range(0, 100, ErrorMessage = "Giá trị khuyến mãi phải nằm từ 0 đến 100")]
        public double? Discount
        {
            get => discount;
            set => SetProperty(ref discount, value, () => this.ValidateProperty(value, nameof(Discount)));
        }
        private string ProductIdlist;
        public async Task save()
        {
            if (Name == null || Unit == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                FillCombo();
                Combo p = new()
                {
                    Id = Id,
                    Name = Name,
                    Unit = Unit,
                    Discount = Discount ?? 0,
                    ProductIdlist = ProductIdlist
                };
                bool res = (bool)await _unitOfWork.Combos.Add(p);
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
