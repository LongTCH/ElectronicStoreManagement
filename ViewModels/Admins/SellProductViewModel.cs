using Models.DTOs;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.MyMessageBox;
using ViewModels.Utilities;

namespace ViewModels.Admins;

public class SellProductViewModel : ViewModelBase
{
    private readonly IUnitOfWork _unitOfWork;
    public SellProductViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        AddCommand = new RelayCommand<object>(_ => addCommand());
        DeleteCommand = new RelayCommand<object>(_ => ProductBillList.RemoveAt(SelectedIndex));
        DeleteAllCommand = new RelayCommand<object>(_ => ProductBillList.Clear());
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
                OnPropertyChanged(nameof(Products));
            }
            catch
            {
                ErrorNotifyViewModel.Instance.Show("Cannot load data", "Failed");
                Products = null;
                OnPropertyChanged(nameof(Products));
            }
        }
    }
    public int SelectedIndex { get; set; }
    public decimal TotalAmount => ProductBillList.Sum(s => s.Amount);
    public string TextFormPrice => NumberToText.FuncNumberToText((double)TotalAmount);
    public ProductDTO SelectedProduct { get; set; }
    public IEnumerable<ProductDTO>? Products { get; set; }
    public ObservableCollection<ProductBill> ProductBillList { get; } = new();
    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand DeleteAllCommand { get; }
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
            ErrorNotifyViewModel.Instance.Show("Choose Product", "Warning");
            return;
        }
        var product = ProductBillList.FirstOrDefault(s => s.Id == SelectedProduct.Id);
        if (product != null)
        {
            product.Number = Convert.ToInt32(product.Number) + 1 + "";
        }
        else
        {
            ProductBill p = new()
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
        OnPropertyChanged(nameof(TotalAmount));
        OnPropertyChanged(nameof(TextFormPrice));
    }
}
public class ProductBill : ViewModelBase
{
    public string Id { get; set; }
    private string? number;
    public string? Number
    {
        get => number;
        set
        {
            number = checkValidNumber(value).ToString();
            OnPropertyChanged(nameof(Number));
            OnPropertyChanged(nameof(Amount));
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
            ErrorNotifyViewModel.Instance.Show("Invalid Number Format", "Error");
        }
        else if (n <= 0)
        {
            ErrorNotifyViewModel.Instance.Show("Number must be greater than 0", "Error");
        }
        return (n == 0) ? 1 : n;
    }
}