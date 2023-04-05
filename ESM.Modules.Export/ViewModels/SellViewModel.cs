using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Export.Views;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace ESM.Modules.Export.ViewModels
{
    public class SellViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        private readonly AccountStore _accountStore;

        public SellViewModel(IUnitOfWork unitOfWork, IModalService modalService, AccountStore accountStore)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            _accountStore = accountStore;
            AddCommand = new DelegateCommand(() => addCommand());
            DeleteCommand = new DelegateCommand(() => ProductBillList.RemoveAt(SelectedIndex));
            DeleteAllCommand = new DelegateCommand(() => ProductBillList.Clear());
            ProductBillList.CollectionChanged += (_, _) => OnTotalAmountChanged();
            PayCommand = new(ExecutePay);
        }
        private string category;

        public string Category
        {
            get => category;
            set
            {
                SetProperty(ref category, value);
                try
                {
                    getProductList();
                }
                catch
                {
                    _modalService.ShowModal(ModalType.Error, "Cannot load data", "Failed");
                    Products = null;
                }
            }
        }


        private string customerName;
        public string CustomerName
        {
            get => customerName;
            set => SetProperty(ref customerName, value);
        }
        private string customerPhone;
        public string CustomerPhone
        {
            get => customerPhone;
            set => SetProperty(ref customerPhone, value);
        }
        public int SelectedIndex { get; set; }
        public decimal TotalAmount => ProductBillList.Sum(s => s.Amount);
        public string TextFormPrice => NumberToText.FuncNumberToText((double)TotalAmount);
        public ProductDTO SelectedProduct { get; set; }
        private IEnumerable<ProductDTO> products;
        public IEnumerable<ProductDTO> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }
        public ObservableCollection<ProductBill> ProductBillList { get; } = new();
        public DelegateCommand AddCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand DeleteAllCommand { get; }
        public DelegateCommand PayCommand { get; }
        public IEnumerable<string> CategoryList { get; } = new[] { "Laptop", "PC", "Monitor", "Hard Disk", "CPU", "VGA", "SmartPhone", "Combo" };
        private void getProductList()
        {
            if (Category == "Laptop") Products = _unitOfWork.Laptops.GetAll();
            else if (Category == "PC") Products = _unitOfWork.Pcs.GetAll();
            else if (Category == "Monitor") Products = _unitOfWork.Monitors.GetAll();
            else if (Category == "Hard Disk") Products = _unitOfWork.Pcharddisks.GetAll();
            else if (Category == "CPU") Products = _unitOfWork.Pccpus.GetAll();
            else if (Category == "VGA") Products = _unitOfWork.Vgas.GetAll();
            else if (Category == "SmartPhone") Products = _unitOfWork.Smartphones.GetAll();
        }
        private void addCommand()
        {
            if (SelectedProduct == null)
            {
                _modalService.ShowModal(ModalType.Error, "Choose Product", "Warning");
                return;
            }
            var product = ProductBillList.FirstOrDefault(s => s.Id == SelectedProduct.Id);
            if (product != null)
            {
                var n = Convert.ToInt32(product.Number);
                if (n == SelectedProduct.Remain)
                    _modalService.ShowModal(ModalType.Error, $"Chỉ còn {SelectedProduct.Remain} sản phẩm", "Không đủ sản phẩm");
                else
                    product.Number = n + 1 + "";
            }
            else
            {
                if (SelectedProduct.Remain == 0)
                {
                    _modalService.ShowModal(ModalType.Information, "Sản phẩm tạm hết hàng", "Xin lỗi");
                    return;
                }
                ProductBill p = new(_modalService)
                {
                    Id = SelectedProduct.Id,
                    Remain = SelectedProduct.Remain,
                    Number = "1",
                    SellPrice = SelectedProduct.SellPrice,
                    Unit = SelectedProduct.Unit,
                    Name = SelectedProduct.Name,
                };
                p.Action += OnTotalAmountChanged;
                ProductBillList.Add(p);
            }
        }
        private void ExecutePay()
        {
            var Result = new Invoice(_modalService, _unitOfWork, _accountStore, CustomerName, CustomerPhone, ProductBillList, TotalAmount).ShowDialog();
            if (Result == true)
            {
                ProductBillList.Clear();
                Category = null;
                CustomerName = null;
                CustomerPhone = null;
            }
        }

        private void OnTotalAmountChanged()
        {
            RaisePropertyChanged(nameof(TotalAmount));
            RaisePropertyChanged(nameof(TextFormPrice));
        }
    }
    public class ProductBill : BindableBase
    {
        private readonly IModalService _modalService;

        public ProductBill(IModalService modalService)
        {
            _modalService = modalService;
        }

        public string Id { get; set; }
        public int Remain { get; set; }
        private string? number;
        public string? Number
        {
            get => number;
            set
            {
                number = checkValidNumber(value).ToString();
                RaisePropertyChanged(nameof(Number));
                RaisePropertyChanged(nameof(Amount));
                Action?.Invoke();
            }
        }
        public string Name { get; set; }
        public decimal SellPrice { get; set; }
        public string Unit { get; set; }
        public decimal Amount => SellPrice * Convert.ToInt32(Number);
        public string? Warranty { get; set; }
        public Action? Action { get; set; }
        public int checkValidNumber(string? s)
        {
            if (!int.TryParse(s, out int n))
            {
                _modalService.ShowModal(ModalType.Error, "Không đúng định dạng", "Lỗi");
            }
            else if (n <= 0)
            {
                _modalService.ShowModal(ModalType.Error, "Số lượng phải lớn hơn 0", "Lỗi");
            }
            else if (n > Remain)
            {
                _modalService.ShowModal(ModalType.Error, $"Chỉ còn {Remain} sản phẩm", "Không đủ sản phẩm");
                return Remain;
            }
            return (n == 0) ? 1 : n;
        }
    }
}
