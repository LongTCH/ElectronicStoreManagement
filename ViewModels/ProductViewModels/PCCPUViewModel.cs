using Models.DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;
using ViewModels.Stores.PCCPUAttributes;

namespace ViewModels.ProductViewModels;
public class PCCPUViewModel : ViewModelBase 
{
    private readonly DataProvider _dataProvider;
    private readonly IEnumerable<PccpuDTO>? _pccpuDTOs;
    private readonly INavigationService _productDetailNavigate;
    private readonly ProductDetailStore _productDetailStore;
    public List<PccpuDTO>? PccpuList { get; set; } = null;
    public HashSet<PccpuCompany> CompanyList { get; set; }
    public HashSet<PccpuNeed> NeedList { get; set; }
    public HashSet<PccpuSocket> SocketList { get; set; }
    public HashSet<PccpuSeries> SeriesList { get; set; }
    public ICommand ProductDetailNavigateCommand { get; }
    public PCCPUViewModel(DataProvider dataProvider,
        ProductDetailStore productDetailStore,
        INavigationService productDetailNavigate)
    {
        _dataProvider = dataProvider;
        _productDetailNavigate = productDetailNavigate;
        _productDetailStore = productDetailStore;
        ProductDetailNavigateCommand = new RelayCommand<PccpuDTO>(s => openDetailCommand(s));
        if (_dataProvider.GetPccpuList() != null) PccpuList = new(_pccpuDTOs = _dataProvider.GetPccpuList()!);
        getCompanyList();
        getSocketList();
        getNeedList();
        getSeriesList();
    }
    private void openDetailCommand(PccpuDTO dto)
    {
        _productDetailStore.CurrentProduct = dto;
        _productDetailNavigate.Navigate();
    }
    private void OnIsCheckedChanged()
    {
        List<string> ListCompany = new();
        List<string> ListSocket = new();
        List<string> ListNeed = new();
        List<string> ListSeries = new();
        PccpuList = new();
        foreach (var e in CompanyList)
            if (e.IsChecked) ListCompany.Add(e.Name);
        foreach (var e in SocketList)
            if (e.IsChecked) ListSocket.Add(e.Name);
        foreach (var e in NeedList)
            if (e.IsChecked) ListNeed.Add(e.Name);
        foreach (var e in SeriesList)
            if (e.IsChecked) ListSeries.Add(e.Name);
        if (ListCompany.Count != 0) PccpuList = _pccpuDTOs!.Where(x => ListCompany.Contains(x.Company)).ToList();
        else PccpuList = (List<PccpuDTO>?)_pccpuDTOs;
        if (ListSocket.Count != 0) PccpuList = PccpuList!.Where(x => ListSocket.Contains(x.Socket)).ToList();
        if (ListNeed.Count != 0) PccpuList = PccpuList!.Where(x => ListNeed.Contains(x.Need)).ToList();
        if (ListSeries.Count != 0) PccpuList = PccpuList!.Where(x => ListSeries.Contains(x.Series)).ToList();
        OnPropertyChanged(nameof(PccpuList));
    }
    private void getCompanyList()
    {
        if (_pccpuDTOs == null) return;
        CompanyList = new();
        foreach (var pccpu in _pccpuDTOs)
        {
            PccpuCompany pccpuCompany = new() { Name = pccpu.Company };
            pccpuCompany.CurrentStoreChanged += OnIsCheckedChanged;
            CompanyList.Add(pccpuCompany);
        }
    }
    private void getSocketList()
    {
        if (_pccpuDTOs == null) return;
        SocketList = new();
        foreach (var pccpu in _pccpuDTOs)
        {
            PccpuSocket pccpusocket = new() { Name = pccpu.Socket };
            pccpusocket.CurrentStoreChanged += OnIsCheckedChanged;
            SocketList.Add(pccpusocket);
        }
    }
    private void getNeedList()
    {
        if (_pccpuDTOs == null) return;
        NeedList = new();
        foreach (var pccpu in _pccpuDTOs)
        {
            if (pccpu.Need == null) continue;
            PccpuNeed pccpuNeed = new() { Name = pccpu.Need };
            pccpuNeed.CurrentStoreChanged += OnIsCheckedChanged;
            NeedList.Add(pccpuNeed);
        }
    }
    private void getSeriesList()
    {
        if (_pccpuDTOs == null) return;
        SeriesList = new();
        foreach (var pccpu in _pccpuDTOs)
        {
            if (pccpu.Series == null) continue;
            PccpuSeries pccpuSeries = new() { Name = pccpu.Series };
            pccpuSeries.CurrentStoreChanged += OnIsCheckedChanged;
            SeriesList.Add(pccpuSeries);
        }
    }
}
