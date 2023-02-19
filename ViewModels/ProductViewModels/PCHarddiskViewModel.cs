using Models.DTOs;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;
using ViewModels.Stores.PCHardDiskAttributes;
using Models.Interfaces;

namespace ViewModels.ProductViewModels;

public class PCHarddiskViewModel : ProductViewModel<PcharddiskDTO>
{
    public HashSet<PcharddiskCompany> CompanyList { get; set; }
    public HashSet<PcharddiskConnect> ConnectList { get; set; }
    public HashSet<PcharddiskType> TypeList { get; set; }
    public HashSet<PcharddiskSeries> SeriesList { get; set; }
    public HashSet<PcharddiskStorage> StorageList { get; set; }
    public PCHarddiskViewModel(IUnitOfWork unitOfWork,
        ProductDetailStore productDetailStore,
        INavigationService productDetailNavigate)
        : base(unitOfWork, productDetailStore, productDetailNavigate)
    {
        var list = _unitOfWork.Pcharddisks.GetAll();
        if (list != null) ProductList = new(_productDTOs = list!);
        Action += OnIsCheckedChanged;
        getCompanyList();
        getConnectList();
        getTypeList();
        getStorageList();
        getSeriesList();
    }
    private void OnIsCheckedChanged()
    {
        List<string> ListCompany = new();
        List<string> ListConnect = new();
        List<string> ListType = new();
        List<string> ListStorage = new();
        List<string> ListSeries = new();
        ProductList = new();
        foreach (var e in CompanyList)
            if (e.IsChecked) ListCompany.Add(e.Name);
        foreach (var e in ConnectList)
            if (e.IsChecked) ListConnect.Add(e.Name);
        foreach (var e in TypeList)
            if (e.IsChecked) ListType.Add(e.Name);
        foreach (var e in StorageList)
            if (e.IsChecked) ListStorage.Add(e.Name);
        foreach (var e in SeriesList)
            if (e.IsChecked) ListSeries.Add(e.Name);
        if (ListCompany.Count != 0) ProductList = _productDTOs!.Where(x => ListCompany.Contains(x.Company)).ToList();
        else ProductList = (List<PcharddiskDTO>?)_productDTOs;
        if (ListConnect.Count != 0) ProductList = ProductList!.Where(x => ListConnect.Contains(x.Connect)).ToList();
        if (ListType.Count != 0) ProductList = ProductList!.Where(x => ListType.Contains(x.Type)).ToList();
        if (ListStorage.Count != 0) ProductList = ProductList!.Where(x => ListStorage.Contains(x.Storage)).ToList();
        if (ListSeries.Count != 0) ProductList = ProductList!.Where(x => ListSeries.Contains(x.Series)).ToList();
        OnPropertyChanged(nameof(ProductList));
    }
    private void getCompanyList()
    {
        if (_productDTOs == null) return;
        CompanyList = new();
        foreach (var pcharddisk in _productDTOs)
        {
            PcharddiskCompany pcharddiskCompany = new() { Name = pcharddisk.Company };
            pcharddiskCompany.CurrentStoreChanged += OnIsCheckedChanged;
            CompanyList.Add(pcharddiskCompany);
        }
    }
    private void getConnectList()
    {
        if (_productDTOs == null) return;
        ConnectList = new();
        foreach (var pcharddisk in _productDTOs)
        {
            PcharddiskConnect pcharddiskCPU = new() { Name = pcharddisk.Connect };
            pcharddiskCPU.CurrentStoreChanged += OnIsCheckedChanged;
            ConnectList.Add(pcharddiskCPU);
        }
    }
    private void getTypeList()
    {
        if (_productDTOs == null) return;
        TypeList = new();
        foreach (var pcharddisk in _productDTOs)
        {
            if (pcharddisk.Type == null) continue;
            PcharddiskType pcharddiskNeed = new() { Name = pcharddisk.Type };
            pcharddiskNeed.CurrentStoreChanged += OnIsCheckedChanged;
            TypeList.Add(pcharddiskNeed);
        }
    }
    private void getStorageList()
    {
        if (_productDTOs == null) return;
        StorageList = new();
        foreach (var pcharddisk in _productDTOs)
        {
            PcharddiskStorage pcharddiskRAM = new() { Name = pcharddisk.Storage };
            pcharddiskRAM.CurrentStoreChanged += OnIsCheckedChanged;
            StorageList.Add(pcharddiskRAM);
        }
    }
    private void getSeriesList()
    {
        if (_productDTOs == null) return;
        SeriesList = new();
        foreach (var pcharddisk in _productDTOs)
        {
            if (pcharddisk.Series == null) continue;
            PcharddiskSeries pcharddiskSeries = new() { Name = pcharddisk.Series };
            pcharddiskSeries.CurrentStoreChanged += OnIsCheckedChanged;
            SeriesList.Add(pcharddiskSeries);
        }
    }
}
