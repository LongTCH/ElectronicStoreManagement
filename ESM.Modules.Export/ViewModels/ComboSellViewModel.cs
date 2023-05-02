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
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        private ICollection<ProductBill> ProductBillList;
        public decimal SellPrice => (SelectedCombo == null) ? 0 : SelectedCombo.SellPrice;
        public double Discount => (SelectedCombo == null) ? 0 : SelectedCombo.Discount;
        public string TextFormPrice => NumberToText.FuncNumberToText((double)SellPrice);
        public string CustomerName { get; set; }
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
            if (SelectedCombo == null) return;
            ProductBillList = new List<ProductBill>();
            foreach (var product in ProductList)
            {
                ProductBillList.Add(new(null)
                {
                    Name = product.Name,
                    SellPrice = product.SellPrice,
                    Remain = product.Remain,
                    Number = "1",
                });
            };
            var Result = new InvoiceCombo(_modalService, _unitOfWork, _accountStore, CustomerName, CustomerPhone, ProductBillList, SelectedCombo).ShowDialog();
            if (Result == true)
            {
                ProductBillList.Clear();
                ComboList = await _unitOfWork.Combos.GetAll();
                CustomerName = null;
                CustomerPhone = null;
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
