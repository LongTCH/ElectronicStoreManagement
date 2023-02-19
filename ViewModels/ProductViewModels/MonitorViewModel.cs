using Models.DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;
using ViewModels.Stores.MonitorAttributes;
using Models.Interfaces;
using Microsoft.Identity.Client;

namespace ViewModels.ProductViewModels;

public class MonitorViewModel : ViewModelBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<MonitorDTO>? _monitorDTOs;
    private readonly INavigationService _productDetailNavigate;
    private readonly ProductDetailStore _productDetailStore;
    public List<MonitorDTO>? MonitorList { get; set; } = null;
    public HashSet<MonitorCompany> CompanyList { get; set; }
    public HashSet<MonitorNeed> NeedList { get; set; }
    public HashSet<MonitorPanel> PanelList { get; set; }
    public HashSet<MonitorSeries> SeriesList { get; set; }
    public HashSet<MonitorRefreshRate> RefreshRateList { get; set; }
    public HashSet<MonitorSize> SizeList { get; set; }
    public ICommand ProductDetailNavigateCommand { get; }
    public MonitorViewModel(IUnitOfWork unitOfWork,
        ProductDetailStore productDetailStore,
        INavigationService productDetailNavigate)
    {
        _unitOfWork = unitOfWork;
        _productDetailNavigate = productDetailNavigate;
        _productDetailStore = productDetailStore;
        ProductDetailNavigateCommand = new RelayCommand<MonitorDTO>(s => openDetailCommand(s));
        var list = _unitOfWork.Monitors.GetAll();
        if (list != null) MonitorList = new(_monitorDTOs = list!);
        getCompanyList();
        getPanelList();
        getNeedList();
        getRefreshRateList();
        getSeriesList();
        getSizeList();
    }
    private void openDetailCommand(MonitorDTO dto)
    {
        _productDetailStore.CurrentProduct = dto;
        _productDetailNavigate.Navigate();
    }
    private void OnIsCheckedChanged()
    {
        List<string> ListCompany = new();
        List<string> ListPanel = new();
        List<string> ListNeed = new();
        List<string> ListRefreshRate = new();
        List<string> ListSeries = new();
        List<string> ListSize = new();
        MonitorList = new();
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
        if (ListCompany.Count != 0) MonitorList = _monitorDTOs!.Where(x => ListCompany.Contains(x.Company)).ToList();
        else MonitorList = (List<MonitorDTO>?)_monitorDTOs;
        if (ListPanel.Count != 0) MonitorList = MonitorList!.Where(x => ListPanel.Contains(x.Panel)).ToList();
        if (ListNeed.Count != 0) MonitorList = MonitorList!.Where(x => ListNeed.Contains(x.Need)).ToList();
        if (ListRefreshRate.Count != 0) MonitorList = MonitorList!.Where(x => ListRefreshRate.Contains(x.RefreshRate.ToString())).ToList();
        if (ListSeries.Count != 0) MonitorList = MonitorList!.Where(x => ListSeries.Contains(x.Series)).ToList();
        if (ListSize.Count != 0) MonitorList = MonitorList!.Where(x => ListSize.Contains(x.Size)).ToList();
        OnPropertyChanged(nameof(MonitorList));
    }
    private void getCompanyList()
    {
        if (_monitorDTOs == null) return;
        CompanyList = new();
        foreach (var monitor in _monitorDTOs)
        {
            MonitorCompany monitorCompany = new() { Name = monitor.Company };
            monitorCompany.CurrentStoreChanged += OnIsCheckedChanged;
            CompanyList.Add(monitorCompany);
        }
    }
    private void getPanelList()
    {
        if (_monitorDTOs == null) return;
        PanelList = new();
        foreach (var monitor in _monitorDTOs)
        {
            MonitorPanel monitorCPU = new() { Name = monitor.Panel };
            monitorCPU.CurrentStoreChanged += OnIsCheckedChanged;
            PanelList.Add(monitorCPU);
        }
    }
    private void getNeedList()
    {
        if (_monitorDTOs == null) return;
        NeedList = new();
        foreach (var monitor in _monitorDTOs)
        {
            if (monitor.Need == null) continue;
            MonitorNeed monitorNeed = new() { Name = monitor.Need };
            monitorNeed.CurrentStoreChanged += OnIsCheckedChanged;
            NeedList.Add(monitorNeed);
        }
    }
    private void getRefreshRateList()
    {
        if (_monitorDTOs == null) return;
        RefreshRateList = new();
        foreach (var monitor in _monitorDTOs)
        {
            MonitorRefreshRate monitorRAM = new() { Name = monitor.RefreshRate.ToString() };
            monitorRAM.CurrentStoreChanged += OnIsCheckedChanged;
            RefreshRateList.Add(monitorRAM);
        }
    }
    private void getSeriesList()
    {
        if (_monitorDTOs == null) return;
        SeriesList = new();
        foreach (var monitor in _monitorDTOs)
        {
            if (monitor.Series == null) continue;
            MonitorSeries monitorSeries = new() { Name = monitor.Series };
            monitorSeries.CurrentStoreChanged += OnIsCheckedChanged;
            SeriesList.Add(monitorSeries);
        }
    }
    private void getSizeList()
    {
        if (_monitorDTOs == null) return;
        SizeList = new();
        foreach (var monitor in _monitorDTOs)
        {
            MonitorSize monitorStorage = new() { Name = monitor.Size };
            monitorStorage.CurrentStoreChanged += OnIsCheckedChanged;
            SizeList.Add(monitorStorage);
        }
    }
}
