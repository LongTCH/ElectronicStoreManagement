using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ESM.Modules.Export.ViewModels
{
    public class SellViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;

        public SellViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            AddCommand = new DelegateCommand(() => addCommand());
            DeleteCommand = new DelegateCommand(() => ProductBillList.RemoveAt(SelectedIndex));
            DeleteAllCommand = new DelegateCommand(() => ProductBillList.Clear());
            ProductBillList.CollectionChanged += (_, _) => OnTotalAmountChanged();
        }
        private string? category;

        public string? Category
        {
            get => category;
            set
            {
                category = value;
                try
                {
                    getProductList();
                    RaisePropertyChanged(nameof(Products));
                }
                catch
                {
                    _modalService.ShowModal(ModalType.Error, "Cannot load data", "Failed");
                    Products = null;
                    RaisePropertyChanged(nameof(Products));
                }
            }
        }
        public int SelectedIndex { get; set; }
        public decimal TotalAmount => ProductBillList.Sum(s => s.Amount);
        public string TextFormPrice => NumberToText.FuncNumberToText((double)TotalAmount);
        public ProductDTO SelectedProduct { get; set; }
        public IEnumerable<ProductDTO>? Products { get; set; }
        public ObservableCollection<ProductBill> ProductBillList { get; } = new();
        public DelegateCommand AddCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand DeleteAllCommand { get; }
        public List<string>? CategoryList { get; } = new() { "Laptop", "PC", "Monitor", "Hard Disk", "CPU", "VGA", "SmartPhone", "Combo" };
        private void getProductList()
        {
            if (Category == "Laptop") Products = _unitOfWork.Laptops.GetAll();
            else if (Category == "PC") Products = _unitOfWork.Pcs.GetAll();
            else if (Category == "Monitor") Products = _unitOfWork.Monitors.GetAll();
            else if (Category == "Hard Disk") Products = _unitOfWork.Pcharddisks.GetAll();
            else if (Category == "CPU") Products = _unitOfWork.Pccpus.GetAll();
            else if (Category == "VGA") Products = _unitOfWork.Vgas.GetAll();
            else if (Category == "Smartphone") Products = _unitOfWork.Smartphones.GetAll();
            else throw new Exception();
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
                product.Number = Convert.ToInt32(product.Number) + 1 + "";
            }
            else
            {
                ProductBill p = new(_modalService)
                {
                    Id = SelectedProduct.Id,
                    Number = "1",
                    SellPrice = SelectedProduct.SellPrice,
                    Unit = SelectedProduct.Unit,
                    Name = SelectedProduct.Name,
                };
                p.Action += OnTotalAmountChanged;
                ProductBillList.Add(p);
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
        public string? Unit { get; set; }
        public decimal Amount => SellPrice * Convert.ToInt32(Number);
        public string? Warranty { get; set; }
        public Action? Action { get; set; }
        public int checkValidNumber(string? s)
        {
            int n;
            if (!int.TryParse(s, out n))
            {
                _modalService.ShowModal(ModalType.Error, "Invalid Number Format", "Error");
            }
            else if (n <= 0)
            {
                _modalService.ShowModal(ModalType.Error, "Number must be greater than 0", "Error");
            }
            return (n == 0) ? 1 : n;
        }
    }
}
