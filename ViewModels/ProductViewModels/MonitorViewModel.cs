using Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using ViewModels.Services;
using ViewModels.Stores;
using Models.Interfaces;
using System;
using Microsoft.IdentityModel.Tokens;

namespace ViewModels.ProductViewModels;

public class MonitorViewModel : ProductViewModel<MonitorDTO>
{
    public HashSet<ProductAttributeStore> CompanyList { get; set; }
    public HashSet<ProductAttributeStore> NeedList { get; set; }
    public HashSet<ProductAttributeStore> PanelList { get; set; }
    public HashSet<ProductAttributeStore> SeriesList { get; set; }
    public HashSet<ProductAttributeStore> RefreshRateList { get; set; }
    public HashSet<ProductAttributeStore> SizeList { get; set; }
    public MonitorViewModel(IUnitOfWork unitOfWork,
        ProductDetailStore productDetailStore,
        INavigationService productDetailNavigate)
        : base(unitOfWork, productDetailStore, productDetailNavigate)
    {
        var list = _unitOfWork.Monitors.GetAll();
        if (list != null && list.Any())
        {
            ProductList = new(_productDTOs = list!);
            MaxPrice = Math.Ceiling((double)list.Max(x => x.SellPrice) / TickFrequency) * TickFrequency;
            CurrentPrice = MaxPrice;
        }
        Action += OnIsCheckedChanged;
        getCompanyList();
        getPanelList();
        getNeedList();
        getRefreshRateList();
        getSeriesList();
        getSizeList();
    }
    private void OnIsCheckedChanged()
    {
        List<string> ListCompany = new();
        List<string> ListPanel = new();
        List<string> ListNeed = new();
        List<string> ListRefreshRate = new();
        List<string> ListSeries = new();
        List<string> ListSize = new();
        ProductList = new();
        foreach (var e in CompanyList)
            if (e.IsChecked) ListCompany.Add(e.Name);
        foreach (var e in PanelList)
            if (e.IsChecked) ListPanel.Add(e.Name);
        foreach (var e in NeedList)
            if (e.IsChecked) ListNeed.Add(e.Name);
        foreach (var e in RefreshRateList)
            if (e.IsChecked) ListRefreshRate.Add(e.Name);
        foreach (var e in SeriesList)
            if (e.IsChecked) ListSeries.Add(e.Name);
        foreach (var e in SizeList)
            if (e.IsChecked) ListSize.Add(e.Name);
        if (ListCompany.Count != 0) ProductList = _productDTOs!.Where(x => ListCompany.Contains(x.Company)).ToList();
        else ProductList = (List<MonitorDTO>?)_productDTOs;
        if (ListPanel.Count != 0) ProductList = ProductList!.Where(x => ListPanel.Contains(x.Panel)).ToList();
        if (ListNeed.Count != 0) ProductList = ProductList!.Where(x => ListNeed.Contains(x.Need)).ToList();
        if (ListRefreshRate.Count != 0) ProductList = ProductList!.Where(x => ListRefreshRate.Contains(x.RefreshRate.ToString())).ToList();
        if (ListSeries.Count != 0) ProductList = ProductList!.Where(x => ListSeries.Contains(x.Series)).ToList();
        if (ListSize.Count != 0) ProductList = ProductList!.Where(x => ListSize.Contains(x.Size)).ToList();
        OnPropertyChanged(nameof(ProductList));
    }
    private void getCompanyList()
    {
        if (_productDTOs == null) return;
        CompanyList = new();
        foreach (var monitor in _productDTOs)
        {
            ProductAttributeStore monitorCompany = new() { Name = monitor.Company };
            monitorCompany.CurrentStoreChanged += OnIsCheckedChanged;
            CompanyList.Add(monitorCompany);
        }
    }
    private void getPanelList()
    {
        if (_productDTOs == null) return;
        PanelList = new();
        foreach (var monitor in _productDTOs)
        {
            ProductAttributeStore monitorCPU = new() { Name = monitor.Panel };
            monitorCPU.CurrentStoreChanged += OnIsCheckedChanged;
            PanelList.Add(monitorCPU);
        }
    }
    private void getNeedList()
    {
        if (_productDTOs == null) return;
        NeedList = new();
        foreach (var monitor in _productDTOs)
        {
            if (string.IsNullOrWhiteSpace(monitor.Need)) continue;
            ProductAttributeStore monitorNeed = new() { Name = monitor.Need };
            monitorNeed.CurrentStoreChanged += OnIsCheckedChanged;
            NeedList.Add(monitorNeed);
        }
    }
    private void getRefreshRateList()
    {
        if (_productDTOs == null) return;
        RefreshRateList = new();
        foreach (var monitor in _productDTOs)
        {
            ProductAttributeStore monitorRAM = new() { Name = monitor.RefreshRate.ToString() };
            monitorRAM.CurrentStoreChanged += OnIsCheckedChanged;
            RefreshRateList.Add(monitorRAM);
        }
    }
    private void getSeriesList()
    {
        if (_productDTOs == null) return;
        SeriesList = new();
        foreach (var monitor in _productDTOs)
        {
            if (string.IsNullOrWhiteSpace(monitor.Series)) continue;
            ProductAttributeStore monitorSeries = new() { Name = monitor.Series };
            monitorSeries.CurrentStoreChanged += OnIsCheckedChanged;
            SeriesList.Add(monitorSeries);
        }
    }
    private void getSizeList()
    {
        if (_productDTOs == null) return;
        SizeList = new();
        foreach (var monitor in _productDTOs)
        {
            ProductAttributeStore monitorStorage = new() { Name = monitor.Size };
            monitorStorage.CurrentStoreChanged += OnIsCheckedChanged;
            SizeList.Add(monitorStorage);
        }
    }
}
