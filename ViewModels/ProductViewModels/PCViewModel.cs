using Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using ViewModels.Services;
using ViewModels.Stores;
using Models.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;

namespace ViewModels.ProductViewModels;
public class PCViewModel : ProductViewModel<PcDTO>
{
    public HashSet<ProductAttributeStore> CompanyList { get; set; }
    public HashSet<ProductAttributeStore> CPUList { get; set; }
    public HashSet<ProductAttributeStore> NeedList { get; set; }
    public HashSet<ProductAttributeStore> SeriesList { get; set; }
    public HashSet<ProductAttributeStore> RAMList { get; set; }
    public PCViewModel(IUnitOfWork unitOfWork,
        ProductDetailStore productDetailStore,
        INavigationService productDetailNavigate)
        : base(unitOfWork, productDetailStore, productDetailNavigate)
    {
        var list = _unitOfWork.Pcs.GetAll();
        if (list != null && list.Any())
        {
            ProductList = new(_productDTOs = list!);
            MaxPrice = Math.Ceiling((double)list.Max(x => x.SellPrice) / TickFrequency) * TickFrequency;
            CurrentPrice = MaxPrice;
        }
        Action += OnIsCheckedChanged;
        getCompanyList();
        getCPUList();
        getNeedList();
        getRAMList();
        getSeriesList();
    }
    private void OnIsCheckedChanged()
    {
        List<string> ListCompany = new();
        List<string> ListCPU = new();
        List<string> ListNeed = new();
        List<string> ListRAM = new();
        List<string> ListSeries = new();
        ProductList = new();
        foreach (var e in CompanyList)
            if (e.IsChecked) ListCompany.Add(e.Name);
        foreach (var e in CPUList)
            if (e.IsChecked) ListCPU.Add(e.Name);
        foreach (var e in NeedList)
            if (e.IsChecked) ListNeed.Add(e.Name);
        foreach (var e in RAMList)
            if (e.IsChecked) ListRAM.Add(e.Name);
        foreach (var e in SeriesList)
            if (e.IsChecked) ListSeries.Add(e.Name);
        if (ListCompany.Count != 0) ProductList = _productDTOs!.Where(x => ListCompany.Contains(x.Company)).ToList();
        else ProductList = (List<PcDTO>?)_productDTOs;
        if (ListCPU.Count != 0) ProductList = ProductList!.Where(x => ListCPU.Contains(x.Cpu)).ToList();
        if (ListNeed.Count != 0) ProductList = ProductList!.Where(x => ListNeed.Contains(x.Need)).ToList();
        if (ListRAM.Count != 0) ProductList = ProductList!.Where(x => ListRAM.Contains(x.Ram)).ToList();
        if (ListSeries.Count != 0) ProductList = ProductList!.Where(x => ListSeries.Contains(x.Series)).ToList();
        OnPropertyChanged(nameof(ProductList));
    }
    private void getCompanyList()
    {
        if (_productDTOs == null) return;
        CompanyList = new();
        foreach (var pc in _productDTOs)
        {
            ProductAttributeStore pcCompany = new() { Name = pc.Company };
            pcCompany.CurrentStoreChanged += OnIsCheckedChanged;
            CompanyList.Add(pcCompany);
        }
    }
    private void getCPUList()
    {
        if (_productDTOs == null) return;
        CPUList = new();
        foreach (var pc in _productDTOs)
        {
            ProductAttributeStore pcCPU = new() { Name = pc.Cpu };
            pcCPU.CurrentStoreChanged += OnIsCheckedChanged;
            CPUList.Add(pcCPU);
        }
    }
    private void getNeedList()
    {
        if (_productDTOs == null) return;
        NeedList = new();
        foreach (var pc in _productDTOs)
        {
            if (string.IsNullOrWhiteSpace(pc.Need)) continue;
            ProductAttributeStore pcNeed = new() { Name = pc.Need };
            pcNeed.CurrentStoreChanged += OnIsCheckedChanged;
            NeedList.Add(pcNeed);
        }
    }
    private void getRAMList()
    {
        if (_productDTOs == null) return;
        RAMList = new();
        foreach (var pc in _productDTOs)
        {
            ProductAttributeStore pcRAM = new() { Name = pc.Ram };
            pcRAM.CurrentStoreChanged += OnIsCheckedChanged;
            RAMList.Add(pcRAM);
        }
    }
    private void getSeriesList()
    {
        if (_productDTOs == null) return;
        SeriesList = new();
        foreach (var pc in _productDTOs)
        {
            if (string.IsNullOrWhiteSpace(pc.Series)) continue;
            ProductAttributeStore pcSeries = new() { Name = pc.Series };
            pcSeries.CurrentStoreChanged += OnIsCheckedChanged;
            SeriesList.Add(pcSeries);
        }
    }
}
