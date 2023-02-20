using Models.DTOs;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.MyMessageBox;
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
    private void openDetailCommand<T>(T dto) where T : ProductDTO
    {
        _productDetailStore.CurrentProduct = dto;
        _productDetailNavigate.Navigate();
    }
    private readonly List<string> imageFiles;
    private int index = 0;
    private void dispatcherTimer_Tick(object sender, ElapsedEventArgs e)
    {
        Source = imageFiles[index];
        OnPropertyChanged(nameof(Source));
        index = (index + 1) % imageFiles.Count;
    }
    public string Source { get; set; }
    Timer Timer { get; set; }
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
        try
        {
            LaptopList = unitOfWork.Laptops.GetAll();
            SmartphoneList = unitOfWork.Smartphones.GetAll();
            PccpuList = unitOfWork.Pccpus.GetAll();
            MonitorList = unitOfWork.Monitors.GetAll();
            PcList = unitOfWork.Pcs.GetAll();
            PcharddiskList = unitOfWork.Pcharddisks.GetAll();
            VgaList = unitOfWork.Vgas.GetAll();
        }
        catch(Exception ex)
        {
            ErrorNotifyViewModel.Instance!.Show(ex.Message, "Error");
        }
        ProductDetailNavigateCommand = new RelayCommand<ProductDTO>(s => openDetailCommand(s));
        LaptopNavigateCommand = new RelayCommand<object>(_ => laptop.Navigate());
        MonitorNavigateCommand = new RelayCommand<object>(_ => monitor.Navigate());
        PcNavigateCommand = new RelayCommand<object>(_ => pc.Navigate());
        PccpuNavigateCommand = new RelayCommand<object>(_ => pccpu.Navigate());
        PcharddiskNavigateCommand = new RelayCommand<object>(_ => pcharddisk.Navigate());
        VgaNavigateCommand = new RelayCommand<object>(_ => vga.Navigate());
        SmartphoneNavigateCommand = new RelayCommand<object>(_ => smartphone.Navigate());

        Timer = new Timer();
        Timer.Elapsed += new ElapsedEventHandler(dispatcherTimer_Tick!);
        Timer.Interval = 3000;
        Timer.Enabled = true;
        var files = Directory.GetFiles(Directory.GetParent(
            Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent
            + "\\Views\\Images\\HomeImages", "*.*", SearchOption.AllDirectories);

        imageFiles = new();
        foreach (string filename in files)
        {
            if (Regex.IsMatch(filename, @"\.jpg$|\.png$|\.gif$|\.webp$"))
                imageFiles.Add(filename);
        }
        Source = imageFiles[index];
        OnPropertyChanged(nameof(Source));
        index = (index + 1) % imageFiles.Count;
    }
}
