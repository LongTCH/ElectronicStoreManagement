using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.Export.Utilities;
using ESM.Modules.Export.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ESM.Modules.Export.ViewModels
{
    public class SellViewModel : BindableBase, INavigationAware
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
        public string Header => "Bán lẻ";
        private string category;

        public string Category
        {
            get => category;
            set
            {
                SetProperty(ref category, value);
                try
                {
                    getProductList().Await();
                }
                catch
                {
                    _modalService.ShowModal(ModalType.Error, "Không thể tải dữ liệu", "Lỗi");
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
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string CustomerPhone
        {
            get => customerPhone;
            set => SetProperty(ref customerPhone, value, () => this.ValidateProperty(value, nameof(CustomerPhone)));
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
        public IEnumerable<string> CategoryList { get; } = new[] { "Laptop", "PC", "Monitor", "Hard Disk", "CPU", "VGA", "SmartPhone" };
        private async Task getProductList()
        {
            Products = null;
            if (Category == "Laptop") Products = await _unitOfWork.Laptops.GetAll();
            else if (Category == "PC") Products = await _unitOfWork.Pcs.GetAll();
            else if (Category == "Monitor") Products = await _unitOfWork.Monitors.GetAll();
            else if (Category == "Hard Disk") Products = await _unitOfWork.Pcharddisks.GetAll();
            else if (Category == "CPU") Products = await _unitOfWork.Pccpus.GetAll();
            else if (Category == "VGA") Products = await _unitOfWork.Vgas.GetAll();
            else if (Category == "SmartPhone") Products = await _unitOfWork.Smartphones.GetAll();
        }
        private void addCommand()
        {
            if (SelectedProduct == null)
            {
                _modalService.ShowModal(ModalType.Error, "Chưa chọn sản phẩm", "Thông báo");
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
                RaisePropertyChanged(nameof(TextFormPrice));
            }
        }

        private void OnTotalAmountChanged()
        {
            RaisePropertyChanged(nameof(TotalAmount));
            RaisePropertyChanged(nameof(TextFormPrice));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ProductBillList.Clear();
            Category = null;
            CustomerName = null;
            CustomerPhone = null;
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
