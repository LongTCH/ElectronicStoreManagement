using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Export.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ESM.Modules.Export.ViewModels
{
    public class ImportBillViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        private readonly AccountStore _accountStore;

        public ImportBillViewModel(IUnitOfWork unitOfWork, IModalService modalService, AccountStore accountStore)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            _accountStore = accountStore;
            AddCommand = new DelegateCommand(() => addCommand());
            DeleteCommand = new DelegateCommand(() => ProductBillList.RemoveAt(SelectedIndex));
            DeleteAllCommand = new DelegateCommand(() => ProductBillList.Clear());
            ProductBillList.CollectionChanged += (_, _) => OnTotalAmountChanged();
            PayCommand = new(async () => await ExecutePay());
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
                    getProductList().Await();
                }
                catch
                {
                    _modalService.ShowModal(ModalType.Error, "Không thể tải dữ liệu", "Lỗi");
                    Products = null;
                }
            }
        }
        public int SelectedIndex { get; set; }
        public decimal TotalAmount => ProductBillList.Sum(s => s.Amount);
        public string TextFormPrice => NumberToText.FuncNumberToText((double)TotalAmount);
        private string providerName;
        public string ProviderName
        {
            get => providerName;
            set => SetProperty(ref providerName, value);
        }
        private string importBillId;
        public string ImportBillId
        {
            get => importBillId;
            set => SetProperty(ref importBillId, value);
        }
        private DateTime? importDate;
        public DateTime? ImportDate
        {
            get => importDate;
            set => SetProperty(ref importDate, value);
        }
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
            if (Category == "Laptop") Products = await _unitOfWork.Laptops.GetAll();
            else if (Category == "PC") Products = await _unitOfWork.Pcs.GetAll();
            else if (Category == "Monitor") Products = await _unitOfWork.Monitors.GetAll();
            else if (Category == "Hard Disk") Products = await _unitOfWork.Pcharddisks.GetAll();
            else if (Category == "CPU") Products = await _unitOfWork.Pccpus.GetAll();
            else if (Category == "VGA") Products = await _unitOfWork.Vgas.GetAll();
            else if (Category == "SmartPhone") Products = await _unitOfWork.Smartphones.GetAll();
            else Products = null;
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
                product.Number = n + 1 + "";
            }
            else
            {
                ProductBill p = new(_modalService)
                {
                    Id = SelectedProduct.Id,
                    Remain = int.MaxValue,
                    Number = "1",
                    SellPrice = SelectedProduct.SellPrice,
                    Unit = SelectedProduct.Unit,
                    Name = SelectedProduct.Name,
                };
                p.Action += OnTotalAmountChanged;
                ProductBillList.Add(p);
            }
        }
        private async Task ExecutePay()
        {
            if (string.IsNullOrWhiteSpace(ProviderName))
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tên nhà cung cấp", "Thông báo");
                return;
            }
            if (string.IsNullOrWhiteSpace(ImportBillId))
            {
                _modalService.ShowModal(ModalType.Error, "Nhập mã hóa đơn nhập hàng", "Thông báo");
                return;
            }
            if (!ImportDate.HasValue)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập ngày nhập hàng", "Thông báo");
                return;
            }
            ICollection<ImportProduct> billProducts = new List<ImportProduct>();
            foreach (var item in ProductBillList)
            {
                billProducts.Add(new ImportProduct()
                {
                    Amount = item.Amount,
                    Number = Convert.ToInt32(item.Number),
                    SellPrice = item.SellPrice,
                    Warranty = item.Warranty,
                    ProductId = item.Id,
                    Unit = item.Unit,
                });
            }
            try
            {
                Import bill = new()
                {
                    Provider = ProviderName,
                    ProviderBillId = ImportBillId,
                    ImportProducts = billProducts,
                    ImportDate = (DateTime)ImportDate,
                    StaffId = _accountStore.CurrentAccount.Id,
                    TotalAmount = TotalAmount,
                    City = "",
                    District = "",
                    SubDistrict = "",
                    Street = ""
                };
                await _unitOfWork.Imports.Add(bill);
                _modalService.ShowModal(ModalType.Information, "Nhập thành công", "Thông báo");

            }
            catch (Exception)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập không thành công", "Thông báo");

            }
            ProductBillList.Clear();
            Category = null;
            ProviderName = null;
            ImportBillId = null;
            ImportDate = null;
        }

        private void OnTotalAmountChanged()
        {
            RaisePropertyChanged(nameof(TotalAmount));
            RaisePropertyChanged(nameof(TextFormPrice));
        }
    }
}
