using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels.MyMessageBox;
using ViewModels.Services;
using ViewModels.Stores;

namespace ViewModels.ProductViewModels;

public class LaptopViewModel : ProductViewModel<LaptopDTO>
{
    public HashSet<ProductAttributeStore> CompanyList { get; set; }
    public HashSet<ProductAttributeStore> CPUList { get; set; }
    public HashSet<ProductAttributeStore> GraphicList { get; set; }
    public HashSet<ProductAttributeStore> NeedList { get; set; }
    public HashSet<ProductAttributeStore> SeriesList { get; set; }
    public HashSet<ProductAttributeStore> RAMList { get; set; }
    public HashSet<ProductAttributeStore> StorageList { get; set; }
    
    public LaptopViewModel(IUnitOfWork unitOfWork,
        ProductDetailStore productDetailStore,
        INavigationService productDetailNavigate)
        : base(unitOfWork, productDetailStore, productDetailNavigate)
    {
        try
        {
            var list = _unitOfWork.Laptops.GetAll();
            if (list != null && list.Any())
            {
                ProductList = new(_productDTOs = list!);
                MaxPrice = Math.Ceiling((double)list.Max(x => x.SellPrice) / TickFrequency) * TickFrequency;
                CurrentPrice = MaxPrice;
            }
            Action += OnIsCheckedChanged;
            getCompanyList();
            getCPUList();
            getGraphicList();
            getNeedList();
            getRAMList();
            getSeriesList();
            getStorageList();
        }
        catch(Exception ex)
        {
            ErrorNotifyViewModel.Instance!.Show(ex.Message, "Error");
        }
    }
    private void OnIsCheckedChanged()
    {
        List<string> ListCompany = new();
        List<string> ListCPU = new();
        List<string> ListGraphic = new();
        List<string> ListNeed = new();
        List<string> ListRAM = new();
        List<string> ListSeries = new();
        List<string> ListStorage = new();
        ProductList = new();
        foreach (var e in CompanyList)
            if (e.IsChecked) ListCompany.Add(e.Name);
        foreach (var e in CPUList)
            if (e.IsChecked) ListCPU.Add(e.Name);
        foreach (var e in GraphicList)
            if (e.IsChecked) ListGraphic.Add(e.Name);
        foreach (var e in NeedList)
            if (e.IsChecked) ListNeed.Add(e.Name);
        foreach (var e in RAMList)
            if (e.IsChecked) ListRAM.Add(e.Name);
        foreach (var e in SeriesList)
            if (e.IsChecked) ListSeries.Add(e.Name);
        foreach (var e in StorageList)
            if (e.IsChecked) ListStorage.Add(e.Name);
        if (ListCompany.Count != 0) ProductList = _productDTOs!.Where(x => ListCompany.Contains(x.Company)).ToList();
        else ProductList = (List<LaptopDTO>?)_productDTOs;
        if (ListCPU.Count != 0) ProductList = ProductList!.Where(x => ListCPU.Contains(x.Cpu)).ToList();
        if (ListGraphic.Count != 0) ProductList = ProductList!.Where(x => ListGraphic.Contains(x.Graphic)).ToList();
        if (ListNeed.Count != 0) ProductList = ProductList!.Where(x => ListNeed.Contains(x.Need)).ToList();
        if (ListRAM.Count != 0) ProductList = ProductList!.Where(x => ListRAM.Contains(x.Ram)).ToList();
        if (ListSeries.Count != 0) ProductList = ProductList!.Where(x => ListSeries.Contains(x.Series)).ToList();
        if (ListStorage.Count != 0) ProductList = ProductList!.Where(x => ListStorage.Contains(x.Storage)).ToList();
        SelectedConditionChanged();
        OnPropertyChanged(nameof(ProductList));
    }
    private void getCompanyList()
    {
        if (_productDTOs == null) return;
        CompanyList = new();
        foreach (var laptop in _productDTOs)
        {
            ProductAttributeStore laptopCompany = new() { Name = laptop.Company };
            laptopCompany.CurrentStoreChanged += OnIsCheckedChanged;
            CompanyList.Add(laptopCompany);
        }
    }
    private void getCPUList()
    {
        if (_productDTOs == null) return;
        CPUList = new();
        foreach (var laptop in _productDTOs)
        {
            ProductAttributeStore laptopCPU = new() { Name = laptop.Cpu };
            laptopCPU.CurrentStoreChanged += OnIsCheckedChanged;
            CPUList.Add(laptopCPU);
        }
    }
    private void getGraphicList()
    {
        if (_productDTOs == null) return;
        GraphicList = new();
        foreach (var laptop in _productDTOs)
        {
            ProductAttributeStore laptopGraphic = new() { Name = laptop.Graphic };
            laptopGraphic.CurrentStoreChanged += OnIsCheckedChanged;
            GraphicList.Add(laptopGraphic);
        }
    }
    private void getNeedList()
    {
        if (_productDTOs == null) return;
        NeedList = new();
        foreach (var laptop in _productDTOs)
        {
            if (string.IsNullOrWhiteSpace(laptop.Need)) continue;
            ProductAttributeStore laptopNeed = new() { Name = laptop.Need };
            laptopNeed.CurrentStoreChanged += OnIsCheckedChanged;
            NeedList.Add(laptopNeed);
        }
    }
    private void getRAMList()
    {
        if (_productDTOs == null) return;
        RAMList = new();
        foreach (var laptop in _productDTOs)
        {
            ProductAttributeStore laptopRAM = new() { Name = laptop.Ram };
            laptopRAM.CurrentStoreChanged += OnIsCheckedChanged;
            RAMList.Add(laptopRAM);
        }
    }
    private void getSeriesList()
    {
        if (_productDTOs == null) return;
        SeriesList = new();
        foreach (var laptop in _productDTOs)
        {
            if (string.IsNullOrWhiteSpace(laptop.Series)) continue;
            ProductAttributeStore laptopSeries = new() { Name = laptop.Series };
            laptopSeries.CurrentStoreChanged += OnIsCheckedChanged;
            SeriesList.Add(laptopSeries);
        }
    }
    private void getStorageList()
    {
        if (_productDTOs == null) return;
        StorageList = new();
        foreach (var laptop in _productDTOs)
        {
            ProductAttributeStore laptopStorage = new() { Name = laptop.Storage };
            laptopStorage.CurrentStoreChanged += OnIsCheckedChanged;
            StorageList.Add(laptopStorage);
        }
    }
}
