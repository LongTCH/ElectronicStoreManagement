using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Export.Utilities;
using ESM.Modules.Export.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ESM.Modules.Export.ViewModels
{
    public class ComboSellViewModel : BindableBase, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        private readonly AccountStore _accountStore;
        public ComboSellViewModel(IUnitOfWork unitOfWork, IModalService modalService, AccountStore accountStore)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            _accountStore = accountStore;
            AddCommand = new(addCommand);
            CancelCommand = new(cancelCommand);
            PayCommand = new(async () => await ExecutePay());
        }
        public string Header => "Combo";
        public double Number { get; set; } = 1;
        private Combo SelectedCombo;
        private IEnumerable<ProductDTO> productList;
        public IEnumerable<ProductDTO> ProductList
        {
            get => productList;
            set => SetProperty(ref productList, value);
        }
        private IEnumerable<Combo> comboList;
        public IEnumerable<Combo> ComboList
        {
            get => comboList;
            set => SetProperty(ref comboList, value);
        }
        private ObservableCollection<ProductBill> productBillList;
        public ObservableCollection<ProductBill> ProductBillList
        {
            get => productBillList;
            set => SetProperty(ref productBillList, value);
        }
        public decimal SellPrice => (SelectedCombo == null) ? 0 : SelectedCombo.SellPrice * (decimal)Number;
        public double Discount => (SelectedCombo == null) ? 0 : SelectedCombo.Discount;
        public string TextFormPrice => NumberToText.FuncNumberToText((double)SellPrice);
        private string customerName;
        public string CustomerName
        {
            get => customerName;
            set => SetProperty(ref customerName, value);
        }
        private string customerPhone;
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string CustomerPhone
        {
            get => customerPhone;
            set => SetProperty(ref customerPhone, value, () => this.ValidateProperty(value, nameof(CustomerPhone)));
        }
        public DelegateCommand<Combo> AddCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand PayCommand { get; }
        private async Task ExecutePay()
        {
            if (ProductBillList == null || ProductBillList.Count == 0) return;
            var Result = new InvoiceCombo(_modalService, _unitOfWork, _accountStore, CustomerName, CustomerPhone, ProductBillList, SelectedCombo, (int)Number).ShowDialog();
            if (Result == true)
            {
                ProductBillList.Clear();
                ComboList = null;
                ComboList = await _unitOfWork.Combos.GetAll();
                cancelCommand();
            }
        }
        private void addCommand(Combo combo)
        {
            if (combo == null) return;
            if (combo.Remain == 0)
            {
                _modalService.ShowModal(ModalType.Information, "Sản phẩm tạm hết hàng", "Xin lỗi");
                return;
            }
            SelectedCombo = combo;
            ProductList = combo.ListProducts;
            ProductBillList = new();
            foreach (var product in ProductList)
            {
                ProductBillList.Add(new(_modalService)
                {
                    Id = product.Id,
                    Unit = product.Unit,
                    Name = product.Name,
                    SellPrice = product.Price,
                    Remain = product.Remain,
                    Number = Math.Min((int)Number, combo.Remain) + "",
                });
            };
            RaisePropertyChanged(nameof(SellPrice));
            RaisePropertyChanged(nameof(TextFormPrice));
            RaisePropertyChanged(nameof(Discount));
        }
        private void cancelCommand()
        {
            SelectedCombo = null;
            ProductList = null;
            CustomerName = null;
            CustomerPhone = null;
            RaisePropertyChanged(nameof(SellPrice));
            RaisePropertyChanged(nameof(TextFormPrice));
            RaisePropertyChanged(nameof(Discount));
            RaisePropertyChanged(nameof(CustomerName));
            RaisePropertyChanged(nameof(CustomerPhone));
        }
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            ComboList = await _unitOfWork.Combos.GetAll();
            cancelCommand();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
