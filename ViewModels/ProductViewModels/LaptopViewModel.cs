using Models;
using Models.DTOs;
using Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;
using ViewModels.Stores.LaptopAttributes;

namespace ViewModels.ProductViewModels;

public class LaptopViewModel : ViewModelBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<LaptopDTO>? _laptopDTOs;
    private readonly INavigationService _productDetailNavigate;
    private readonly ProductDetailStore _productDetailStore;
    public List<LaptopDTO>? LaptopList { get; set; } = null;
    public HashSet<LaptopCompany> CompanyList { get; set; }
    public HashSet<LaptopCPU> CPUList { get; set; }
    public HashSet<LaptopGraphic> GraphicList { get; set; }
    public HashSet<LaptopNeed> NeedList { get; set; }
    public HashSet<LaptopSeries> SeriesList { get; set; }
    public HashSet<LaptopRAM> RAMList { get; set; }
    public HashSet<LaptopStorage> StorageList { get; set; }
    public ICommand ProductDetailNavigateCommand { get; }
    public LaptopViewModel(IUnitOfWork unitOfWork,
        ProductDetailStore productDetailStore,
        INavigationService productDetailNavigate)
    {
        _unitOfWork = unitOfWork;
        _productDetailNavigate = productDetailNavigate;
        _productDetailStore = productDetailStore;
        ProductDetailNavigateCommand = new RelayCommand<LaptopDTO>(s => openDetailCommand(s));
        var list = _unitOfWork.Laptops.GetAll();
        if (list != null) LaptopList = new(_laptopDTOs = list!);
        getCompanyList();
        getCPUList();
        getGraphicList();
        getNeedList();
        getRAMList();
        getSeriesList();
        getStorageList();
    }
    private void openDetailCommand(LaptopDTO dto)
    {
        _productDetailStore.CurrentProduct = dto;
        _productDetailNavigate.Navigate();
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
        LaptopList = new();
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
        if (ListCompany.Count != 0) LaptopList = _laptopDTOs!.Where(x => ListCompany.Contains(x.Company)).ToList();
        else LaptopList = (List<LaptopDTO>?)_laptopDTOs;
        if (ListCPU.Count != 0) LaptopList = LaptopList!.Where(x => ListCPU.Contains(x.Cpu)).ToList();
        if (ListGraphic.Count != 0) LaptopList = LaptopList!.Where(x => ListGraphic.Contains(x.Graphic)).ToList();
        if (ListNeed.Count != 0) LaptopList = LaptopList!.Where(x => ListNeed.Contains(x.Need)).ToList();
        if (ListRAM.Count != 0) LaptopList = LaptopList!.Where(x => ListRAM.Contains(x.Ram)).ToList();
        if (ListSeries.Count != 0) LaptopList = LaptopList!.Where(x => ListSeries.Contains(x.Series)).ToList();
        if (ListStorage.Count != 0) LaptopList = LaptopList!.Where(x => x.Storage.Contains(x.Storage)).ToList();
        OnPropertyChanged(nameof(LaptopList));
    }
    private void getCompanyList()
    {
        if (_laptopDTOs == null) return;
        CompanyList = new();
        foreach (var laptop in _laptopDTOs)
        {
            LaptopCompany laptopCompany = new() { Name = laptop.Company };
            laptopCompany.CurrentStoreChanged += OnIsCheckedChanged;
            CompanyList.Add(laptopCompany);
        }
    }
    private void getCPUList()
    {
        if (_laptopDTOs == null) return;
        CPUList = new();
        foreach (var laptop in _laptopDTOs)
        {
            LaptopCPU laptopCPU = new() { Name = laptop.Cpu };
            laptopCPU.CurrentStoreChanged += OnIsCheckedChanged;
            CPUList.Add(laptopCPU);
        }
    }
    private void getGraphicList()
    {
        if (_laptopDTOs == null) return;
        GraphicList = new();
        foreach (var laptop in _laptopDTOs)
        {
            LaptopGraphic laptopGraphic = new() { Name = laptop.Graphic };
            laptopGraphic.CurrentStoreChanged += OnIsCheckedChanged;
            GraphicList.Add(laptopGraphic);
        }
    }
    private void getNeedList()
    {
        if (_laptopDTOs == null) return;
        NeedList = new();
        foreach (var laptop in _laptopDTOs)
        {
            if (laptop.Need == null) continue;
            LaptopNeed laptopNeed = new() { Name = laptop.Need };
            laptopNeed.CurrentStoreChanged += OnIsCheckedChanged;
            NeedList.Add(laptopNeed);
        }
    }
    private void getRAMList()
    {
        if (_laptopDTOs == null) return;
        RAMList = new();
        foreach (var laptop in _laptopDTOs)
        {
            LaptopRAM laptopRAM = new() { Name = laptop.Ram };
            laptopRAM.CurrentStoreChanged += OnIsCheckedChanged;
            RAMList.Add(laptopRAM);
        }
    }
    private void getSeriesList()
    {
        if (_laptopDTOs == null) return;
        SeriesList = new();
        foreach (var laptop in _laptopDTOs)
        {
            if (laptop.Series == null) continue;
            LaptopSeries laptopSeries = new() { Name = laptop.Series };
            laptopSeries.CurrentStoreChanged += OnIsCheckedChanged;
            SeriesList.Add(laptopSeries);
        }
    }
    private void getStorageList()
    {
        if (_laptopDTOs == null) return;
        StorageList = new();
        foreach (var laptop in _laptopDTOs)
        {
            LaptopStorage laptopStorage = new() { Name = laptop.Storage };
            laptopStorage.CurrentStoreChanged += OnIsCheckedChanged;
            StorageList.Add(laptopStorage);
        }
    }
}
