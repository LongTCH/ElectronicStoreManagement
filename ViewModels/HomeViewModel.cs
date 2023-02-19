using Models.DTOs;
using Models.Interfaces;
using System.Collections.Generic;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;

namespace ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ProductDetailStore _productDetailStore;
    private readonly INavigationService _productDetailNavigate;
    public IEnumerable<LaptopDTO>? LaptopList { get; }
    public IEnumerable<SmartphoneDTO>? SmartphoneList { get; }
    public IEnumerable<PccpuDTO>? PccpuList { get; }
    public IEnumerable<MonitorDTO>? MonitorList { get; }
    public IEnumerable<PcDTO>? PcList { get; }
    public IEnumerable<PcharddiskDTO>? PcharddiskList { get; }
    public IEnumerable<VgaDTO>? VgaList { get; }
    public ICommand ProductDetailNavigateCommand { get; }
    public ICommand LaptopNavigateCommand { get; }
    public ICommand MonitorNavigateCommand { get; }
    public ICommand PccpuNavigateCommand { get; }
    public ICommand PcNavigateCommand { get; }
    public ICommand PcharddiskNavigateCommand { get; }
    public ICommand VgaNavigateCommand { get; }
    public ICommand SmartphoneNavigateCommand { get; }
    private void openDetailCommand<T>(T dto) where T : IProductDTO
    {
        _productDetailStore.CurrentProduct = dto;
        _productDetailNavigate.Navigate();
    }
    public HomeViewModel(IUnitOfWork unitOfWork,
        ProductDetailStore productDetailStore,
        INavigationService productDetailNavigate,
        INavigationService laptop,
        INavigationService monitor,
        INavigationService pc,
        INavigationService pccpu,
        INavigationService pcharddisk,
        INavigationService vga,
        INavigationService smartphone)
    {
        _unitOfWork = unitOfWork;
        _productDetailStore = productDetailStore;
        _productDetailNavigate = productDetailNavigate;
        LaptopList = unitOfWork.Laptops.GetAll();
        SmartphoneList = unitOfWork.Smartphones.GetAll();
        PccpuList = unitOfWork.Pccpus.GetAll();
        MonitorList = unitOfWork.Monitors.GetAll();
        PcList = unitOfWork.Pcs.GetAll();
        PcharddiskList = unitOfWork.Pcharddisks.GetAll();
        VgaList = unitOfWork.Vgas.GetAll();
        ProductDetailNavigateCommand = new RelayCommand<IProductDTO>(s => openDetailCommand(s));
        LaptopNavigateCommand = new RelayCommand<object>(_ => laptop.Navigate());
        MonitorNavigateCommand = new RelayCommand<object>(_ => monitor.Navigate());
        PcNavigateCommand = new RelayCommand<object>(_ => pc.Navigate());
        PccpuNavigateCommand = new RelayCommand<object>(_ => pccpu.Navigate());
        PcharddiskNavigateCommand = new RelayCommand<object>(_ => pcharddisk.Navigate());
        VgaNavigateCommand = new RelayCommand<object>(_ => vga.Navigate());
        SmartphoneNavigateCommand = new RelayCommand<object>(_ => smartphone.Navigate());
    }
}
