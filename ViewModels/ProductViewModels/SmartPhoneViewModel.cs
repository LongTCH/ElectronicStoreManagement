using Models.DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;
using ViewModels.Stores.SmartPhoneAttributes;

namespace ViewModels.ProductViewModels;
public class SmartPhoneViewModel : ViewModelBase
{
    private readonly DataProvider _dataProvider;
    private readonly IEnumerable<SmartphoneDTO>? _smartphoneDTOs;
    private readonly INavigationService _productDetailNavigate;
    private readonly ProductDetailStore _productDetailStore;
    public List<SmartphoneDTO>? SmartphoneList { get; set; } = null;
    public HashSet<SmartphoneCompany> CompanyList { get; set; }
    public HashSet<SmartphoneCPU> CPUList { get; set; }
    public HashSet<SmartphoneSeries> SeriesList { get; set; }
    public HashSet<SmartphoneRAM> RAMList { get; set; }
    public HashSet<SmartphoneStorage> StorageList { get; set; }
    public ICommand ProductDetailNavigateCommand { get; }
    public SmartPhoneViewModel(DataProvider dataProvider,
        ProductDetailStore productDetailStore,
        INavigationService productDetailNavigate)
    {
        _dataProvider = dataProvider;
        _productDetailNavigate = productDetailNavigate;
        _productDetailStore = productDetailStore;
        ProductDetailNavigateCommand = new RelayCommand<SmartphoneDTO>(s => openDetailCommand(s));
        if (_dataProvider.GetSmartphoneList() != null) SmartphoneList = new(_smartphoneDTOs = _dataProvider.GetSmartphoneList()!);
        getCompanyList();
        getCPUList();
        getRAMList();
        getSeriesList();
        getStorageList();
    }
    private void openDetailCommand(SmartphoneDTO dto)
    {
        _productDetailStore.CurrentProduct = dto;
        _productDetailNavigate.Navigate();
    }
    private void OnIsCheckedChanged()
    {
        List<string> ListCompany = new();
        List<string> ListCPU = new();
        List<string> ListRAM = new();
        List<string> ListSeries = new();
        List<string> ListStorage = new();
        SmartphoneList = new();
        foreach (var e in CompanyList)
            if (e.IsChecked) ListCompany.Add(e.Name);
        foreach (var e in CPUList)
            if (e.IsChecked) ListCPU.Add(e.Name);
        foreach (var e in RAMList)
            if (e.IsChecked) ListRAM.Add(e.Name);
        foreach (var e in SeriesList)
            if (e.IsChecked) ListSeries.Add(e.Name);
        foreach (var e in StorageList)
            if (e.IsChecked) ListStorage.Add(e.Name);
        if (ListCompany.Count != 0) SmartphoneList = _smartphoneDTOs!.Where(x => ListCompany.Contains(x.Company)).ToList();
        else SmartphoneList = (List<SmartphoneDTO>?)_smartphoneDTOs;
        if (ListCPU.Count != 0) SmartphoneList = SmartphoneList!.Where(x => ListCPU.Contains(x.Cpu)).ToList();
        if (ListRAM.Count != 0) SmartphoneList = SmartphoneList!.Where(x => ListRAM.Contains(x.Ram)).ToList();
        if (ListSeries.Count != 0) SmartphoneList = SmartphoneList!.Where(x => ListSeries.Contains(x.Series)).ToList();
        if (ListStorage.Count != 0) SmartphoneList = SmartphoneList!.Where(x => ListStorage.Contains(x.Storage)).ToList();
        OnPropertyChanged(nameof(SmartphoneList));
    }
    private void getCompanyList()
    {
        if (_smartphoneDTOs == null) return;
        CompanyList = new();
        foreach (var smartphone in _smartphoneDTOs)
        {
            SmartphoneCompany smartphoneCompany = new() { Name = smartphone.Company };
            smartphoneCompany.CurrentStoreChanged += OnIsCheckedChanged;
            CompanyList.Add(smartphoneCompany);
        }
    }
    private void getCPUList()
    {
        if (_smartphoneDTOs == null) return;
        CPUList = new();
        foreach (var smartphone in _smartphoneDTOs)
        {
            SmartphoneCPU smartphoneCPU = new() { Name = smartphone.Cpu };
            smartphoneCPU.CurrentStoreChanged += OnIsCheckedChanged;
            CPUList.Add(smartphoneCPU);
        }
    }
    private void getRAMList()
    {
        if (_smartphoneDTOs == null) return;
        RAMList = new();
        foreach (var smartphone in _smartphoneDTOs)
        {
            SmartphoneRAM smartphoneRAM = new() { Name = smartphone.Ram };
            smartphoneRAM.CurrentStoreChanged += OnIsCheckedChanged;
            RAMList.Add(smartphoneRAM);
        }
    }
    private void getSeriesList()
    {
        if (_smartphoneDTOs == null) return;
        SeriesList = new();
        foreach (var smartphone in _smartphoneDTOs)
        {
            if (smartphone.Series == null) continue;
            SmartphoneSeries smartphoneSeries = new() { Name = smartphone.Series };
            smartphoneSeries.CurrentStoreChanged += OnIsCheckedChanged;
            SeriesList.Add(smartphoneSeries);
        }
    }
    private void getStorageList()
    {
        if (_smartphoneDTOs == null) return;
        StorageList = new();
        foreach (var smartphone in _smartphoneDTOs)
        {
            SmartphoneStorage smartphoneStorage = new() { Name = smartphone.Storage };
            smartphoneStorage.CurrentStoreChanged += OnIsCheckedChanged;
            StorageList.Add(smartphoneStorage);
        }
    }
}
