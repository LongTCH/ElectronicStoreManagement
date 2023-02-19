using Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using ViewModels.Services;
using ViewModels.Stores;
using ViewModels.Stores.PCAttributes;
using Models.Interfaces;

namespace ViewModels.ProductViewModels;
public class PCViewModel : ProductViewModel<PcDTO>
{
    public HashSet<PcCompany> CompanyList { get; set; }
    public HashSet<PcCPU> CPUList { get; set; }
    public HashSet<PcNeed> NeedList { get; set; }
    public HashSet<PcSeries> SeriesList { get; set; }
    public HashSet<PcRAM> RAMList { get; set; }
    public PCViewModel(IUnitOfWork unitOfWork,
        ProductDetailStore productDetailStore,
        INavigationService productDetailNavigate)
        : base(unitOfWork, productDetailStore, productDetailNavigate)
    {
        var list = _unitOfWork.Pcs.GetAll();
        if (list != null) ProductList = new(_productDTOs = list!);
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
            PcCompany pcCompany = new() { Name = pc.Company };
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
            PcCPU pcCPU = new() { Name = pc.Cpu };
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
            if (pc.Need == null) continue;
            PcNeed pcNeed = new() { Name = pc.Need };
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
            PcRAM pcRAM = new() { Name = pc.Ram };
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
            if (pc.Series == null) continue;
            PcSeries pcSeries = new() { Name = pc.Series };
            pcSeries.CurrentStoreChanged += OnIsCheckedChanged;
            SeriesList.Add(pcSeries);
        }
    }
}
