using Models.DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;
using ViewModels.Stores.PCAttributes;

namespace ViewModels.ProductViewModels;
public class PCViewModel : ViewModelBase
{
    private readonly DataProvider _dataProvider;
    private readonly IEnumerable<PcDTO>? _pcDTOs;
    private readonly INavigationService _productDetailNavigate;
    private readonly ProductDetailStore _productDetailStore;
    public List<PcDTO>? PcList { get; set; } = null;
    public HashSet<PcCompany> CompanyList { get; set; }
    public HashSet<PcCPU> CPUList { get; set; }
    public HashSet<PcNeed> NeedList { get; set; }
    public HashSet<PcSeries> SeriesList { get; set; }
    public HashSet<PcRAM> RAMList { get; set; }
    public ICommand ProductDetailNavigateCommand { get; }
    public PCViewModel(DataProvider dataProvider,
        ProductDetailStore productDetailStore,
        INavigationService productDetailNavigate)
    {
        _dataProvider = dataProvider;
        _productDetailNavigate = productDetailNavigate;
        _productDetailStore = productDetailStore;
        ProductDetailNavigateCommand = new RelayCommand<PcDTO>(s => openDetailCommand(s));
        if (_dataProvider.GetPcList() != null) PcList = new(_pcDTOs = _dataProvider.GetPcList()!);
        getCompanyList();
        getCPUList();
        getNeedList();
        getRAMList();
        getSeriesList();
    }
    private void openDetailCommand(PcDTO dto)
    {
        _productDetailStore.CurrentProduct = dto;
        _productDetailNavigate.Navigate();
    }
    private void OnIsCheckedChanged()
    {
        List<string> ListCompany = new();
        List<string> ListCPU = new();
        List<string> ListNeed = new();
        List<string> ListRAM = new();
        List<string> ListSeries = new();
        PcList = new();
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
        if (ListCompany.Count != 0) PcList = _pcDTOs!.Where(x => ListCompany.Contains(x.Company)).ToList();
        else PcList = (List<PcDTO>?)_pcDTOs;
        if (ListCPU.Count != 0) PcList = PcList!.Where(x => ListCPU.Contains(x.Cpu)).ToList();
        if (ListNeed.Count != 0) PcList = PcList!.Where(x => ListNeed.Contains(x.Need)).ToList();
        if (ListRAM.Count != 0) PcList = PcList!.Where(x => ListRAM.Contains(x.Ram)).ToList();
        if (ListSeries.Count != 0) PcList = PcList!.Where(x => ListSeries.Contains(x.Series)).ToList();
        OnPropertyChanged(nameof(PcList));
    }
    private void getCompanyList()
    {
        if (_pcDTOs == null) return;
        CompanyList = new();
        foreach (var pc in _pcDTOs)
        {
            PcCompany pcCompany = new() { Name = pc.Company };
            pcCompany.CurrentStoreChanged += OnIsCheckedChanged;
            CompanyList.Add(pcCompany);
        }
    }
    private void getCPUList()
    {
        if (_pcDTOs == null) return;
        CPUList = new();
        foreach (var pc in _pcDTOs)
        {
            PcCPU pcCPU = new() { Name = pc.Cpu };
            pcCPU.CurrentStoreChanged += OnIsCheckedChanged;
            CPUList.Add(pcCPU);
        }
    }
    private void getNeedList()
    {
        if (_pcDTOs == null) return;
        NeedList = new();
        foreach (var pc in _pcDTOs)
        {
            if (pc.Need == null) continue;
            PcNeed pcNeed = new() { Name = pc.Need };
            pcNeed.CurrentStoreChanged += OnIsCheckedChanged;
            NeedList.Add(pcNeed);
        }
    }
    private void getRAMList()
    {
        if (_pcDTOs == null) return;
        RAMList = new();
        foreach (var pc in _pcDTOs)
        {
            PcRAM pcRAM = new() { Name = pc.Ram };
            pcRAM.CurrentStoreChanged += OnIsCheckedChanged;
            RAMList.Add(pcRAM);
        }
    }
    private void getSeriesList()
    {
        if (_pcDTOs == null) return;
        SeriesList = new();
        foreach (var pc in _pcDTOs)
        {
            if (pc.Series == null) continue;
            PcSeries pcSeries = new() { Name = pc.Series };
            pcSeries.CurrentStoreChanged += OnIsCheckedChanged;
            SeriesList.Add(pcSeries);
        }
    }
}
