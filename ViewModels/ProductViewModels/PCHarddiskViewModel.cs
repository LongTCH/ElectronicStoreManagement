using Models.DTOs;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;
using ViewModels.Stores.PCHardDiskAttributes;

namespace ViewModels.ProductViewModels;

public class PCHarddiskViewModel : ViewModelBase
{
    private readonly DataProvider _dataProvider;
    private readonly IEnumerable<PcharddiskDTO>? _pcharddiskDTOs;
    private readonly INavigationService _productDetailNavigate;
    private readonly ProductDetailStore _productDetailStore;
    public List<PcharddiskDTO>? PcharddiskList { get; set; } = null;
    public HashSet<PcharddiskCompany> CompanyList { get; set; }
    public HashSet<PcharddiskConnect> ConnectList { get; set; }
    public HashSet<PcharddiskType> TypeList { get; set; }
    public HashSet<PcharddiskSeries> SeriesList { get; set; }
    public HashSet<PcharddiskStorage> StorageList { get; set; }
    public ICommand ProductDetailNavigateCommand { get; }
    public PCHarddiskViewModel(DataProvider dataProvider,
        ProductDetailStore productDetailStore,
        INavigationService productDetailNavigate)
    {
        _dataProvider = dataProvider;
        _productDetailNavigate = productDetailNavigate;
        _productDetailStore = productDetailStore;
        ProductDetailNavigateCommand = new RelayCommand<PcharddiskDTO>(s => openDetailCommand(s));
        if (_dataProvider.GetPcharddiskList() != null) PcharddiskList = new(_pcharddiskDTOs = _dataProvider.GetPcharddiskList()!);
        getCompanyList();
        getConnectList();
        getTypeList();
        getStorageList();
        getSeriesList();
    }
    private void openDetailCommand(PcharddiskDTO dto)
    {
        _productDetailStore.CurrentProduct = dto;
        _productDetailNavigate.Navigate();
    }
    private void OnIsCheckedChanged()
    {
        List<string> ListCompany = new();
        List<string> ListConnect = new();
        List<string> ListType = new();
        List<string> ListStorage = new();
        List<string> ListSeries = new();
        PcharddiskList = new();
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
        if (ListCompany.Count != 0) PcharddiskList = _pcharddiskDTOs!.Where(x => ListCompany.Contains(x.Company)).ToList();
        else PcharddiskList = (List<PcharddiskDTO>?)_pcharddiskDTOs;
        if (ListConnect.Count != 0) PcharddiskList = PcharddiskList!.Where(x => ListConnect.Contains(x.Connect)).ToList();
        if (ListType.Count != 0) PcharddiskList = PcharddiskList!.Where(x => ListType.Contains(x.Type)).ToList();
        if (ListStorage.Count != 0) PcharddiskList = PcharddiskList!.Where(x => ListStorage.Contains(x.Storage)).ToList();
        if (ListSeries.Count != 0) PcharddiskList = PcharddiskList!.Where(x => ListSeries.Contains(x.Series)).ToList();
        OnPropertyChanged(nameof(PcharddiskList));
    }
    private void getCompanyList()
    {
        if (_pcharddiskDTOs == null) return;
        CompanyList = new();
        foreach (var pcharddisk in _pcharddiskDTOs)
        {
            PcharddiskCompany pcharddiskCompany = new() { Name = pcharddisk.Company };
            pcharddiskCompany.CurrentStoreChanged += OnIsCheckedChanged;
            CompanyList.Add(pcharddiskCompany);
        }
    }
    private void getConnectList()
    {
        if (_pcharddiskDTOs == null) return;
        ConnectList = new();
        foreach (var pcharddisk in _pcharddiskDTOs)
        {
            PcharddiskConnect pcharddiskCPU = new() { Name = pcharddisk.Connect };
            pcharddiskCPU.CurrentStoreChanged += OnIsCheckedChanged;
            ConnectList.Add(pcharddiskCPU);
        }
    }
    private void getTypeList()
    {
        if (_pcharddiskDTOs == null) return;
        TypeList = new();
        foreach (var pcharddisk in _pcharddiskDTOs)
        {
            if (pcharddisk.Type == null) continue;
            PcharddiskType pcharddiskNeed = new() { Name = pcharddisk.Type };
            pcharddiskNeed.CurrentStoreChanged += OnIsCheckedChanged;
            TypeList.Add(pcharddiskNeed);
        }
    }
    private void getStorageList()
    {
        if (_pcharddiskDTOs == null) return;
        StorageList = new();
        foreach (var pcharddisk in _pcharddiskDTOs)
        {
            PcharddiskStorage pcharddiskRAM = new() { Name = pcharddisk.Storage };
            pcharddiskRAM.CurrentStoreChanged += OnIsCheckedChanged;
            StorageList.Add(pcharddiskRAM);
        }
    }
    private void getSeriesList()
    {
        if (_pcharddiskDTOs == null) return;
        SeriesList = new();
        foreach (var pcharddisk in _pcharddiskDTOs)
        {
            if (pcharddisk.Series == null) continue;
            PcharddiskSeries pcharddiskSeries = new() { Name = pcharddisk.Series };
            pcharddiskSeries.CurrentStoreChanged += OnIsCheckedChanged;
            SeriesList.Add(pcharddiskSeries);
        }
    }
}
