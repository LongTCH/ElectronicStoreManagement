using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using LiveCharts;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace ESM.Modules.Export.ViewModels
{
    public class TopSellingReportViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        public TopSellingReportViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            Test = new(execute);
            AddCommand = new(addCommand);

            LaptopSales = new ChartValues<int>();
            SmartphoneSales = new ChartValues<int>();
            PCSales = new ChartValues<int>();
            CPUSales = new ChartValues<int>();
            VGASales = new ChartValues<int>();
            MonitorSales = new ChartValues<int>();
            HarddiskSales = new ChartValues<int>();
            StartTime = new DateTime(2023, 1, 1);
            EndTime = new DateTime(2023, 4, 1);
        }
        public DelegateCommand<string> Test { get; }
        public DelegateCommand AddCommand { get; }
        public ChartValues<int> LaptopSales { get; set; }
        public ChartValues<int> SmartphoneSales { get; set; }
        public ChartValues<int> PCSales { get; set; }
        public ChartValues<int> CPUSales { get; set; }
        public ChartValues<int> VGASales { get; set; }
        public ChartValues<int> MonitorSales { get; set; }
        public ChartValues<int> HarddiskSales { get; set; }
        public ObservableCollection<string> Labels { get; set; } = new ObservableCollection<string>();
        public string ChartTitle { get; set; } = "Top 10 Best Selling Products";
        private void execute(string w) 
        { 
            LaptopSales.Clear();
            SmartphoneSales.Clear();
            PCSales.Clear();
            CPUSales.Clear();
            VGASales.Clear();
            MonitorSales.Clear();
            HarddiskSales.Clear();
            Labels.Clear();
  
            DateTime start = StartTime;
            DateTime end = EndTime;
            if (StartTime > EndTime)
            {
                MessageBox.Show("invalid date range!");
                return;
            }
            if (IsLaptopCheck) 
            {
                //laptopSales = _unitOfWork.Laptops.GetTopSellingProducts(start, end, ProductType.LAPTOP, 10);
                //foreach (var sale in laptopsales)
                //{
                //    laptopsales.add(sale.quantitysold);
                //    labels.add(sale.product.name);
                //}
            }
            if (IsSmartphoneCheck)
            {
                //smartphoneSales = _unitOfWork.ProductRepository.GetTopSellingProducts(start, end, ProductType.Smartphone, 10);
                //foreach (var sale in smartphoneSales)
                //{
                //    SmartphoneSales.Add(sale.QuantitySold);
                //    Labels.Add(sale.Product.Name);
                //}
            }

            if (IsPCCheck)
            {
                // pcSales = _unitOfWork.ProductRepository.GetTopSellingProducts(start, end, ProductType.PC, 10);
                //foreach (var sale in pcSales)
                //{
                //    PCSales.Add(sale.QuantitySold);
                //    Labels.Add(sale.Product.Name);
                //}
            }

            if (IsCPUCheck)
            {
                //cpuSales = _unitOfWork.ProductRepository.GetTopSellingProducts(start, end, ProductType.CPU, 10);
                //foreach (var sale in cpuSales)
                //{
                //    CPUSales.Add(sale.QuantitySold);
                //    Labels.Add(sale.Product.Name);
                //}
            }

            if (IsVGACheck)
            {
                //vgaSales = _unitOfWork.ProductRepository.GetTopSellingProducts(start, end, ProductType.VGA, 10);
                //foreach (var sale in vgaSales)
                //{
                //    VGASales.Add(sale.QuantitySold);
                //    Labels.Add(sale.Product.Name);
                //}
            }

            if (IsMonitorCheck)
            {     
               // monitorSales = _unitOfWork.ProductRepository.GetTopSellingProducts(start, end, ProductType.Monitor, 10);
                //foreach (var sale in monitorSales)
                //{
                //    MonitorSales.Add(sale.QuantitySold);
                //    Labels.Add(sale.Product.Name);
                //}
            }

            if (IsHarddiskCheck)
            {
                //harddiskSales = _unitOfWork.ProductRepository.GetTopSellingProducts(start, end, ProductType.Harddisk, 10);
                //foreach (var sale in harddiskSales)
                //{
                //    HarddiskSales.Add(sale.QuantitySold);
                //    Labels.Add(sale.Product.Name);
                //}
            }
            if (Labels.Count == 0)
            {
                MessageBox.Show("No sales found for the selected date range and product types!");
            }
            ChartTitle = $"Top 10 Best Selling Products ({StartTime.ToShortDateString()} - {EndTime.ToShortDateString()})";
        }
        private void addCommand()
        {
            bool showLaptop = IsLaptopCheck;
            bool showSmartphone = IsSmartphoneCheck;
            bool showPC = IsPCCheck;
            bool showCPU = IsCPUCheck;
            bool showVGA = IsVGACheck;
            bool showMonitor = IsMonitorCheck;
            bool showHarddisk = IsHarddiskCheck;
            if (showLaptop) {
               
            }
            if (showSmartphone) { }
            if (showPC) { }
            if (showCPU) { }
            if (showVGA) { }
            if (showMonitor) { }
            if(showHarddisk) { }
        }
        public ChartValues<int> Sales { get; set; }   
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsLaptopCheck { get; set; }
        public bool IsSmartphoneCheck { get; set; }
        public bool IsPCCheck { get; set; }
        public bool IsCPUCheck { get; set; }
        public bool IsVGACheck { get; set; }
        public bool IsMonitorCheck { get; set; }
        public bool IsHarddiskCheck { get; set; }
        public object laptopSales { get; set; }
        public object smartphoneSales { get; set; }
        public object pcSales { get; set; }
        public object cpuSales { get; set; }
        public object vgaSales { get; set; }
        public object monitorSales { get; set; }
        public object harddiskSales { get; set; }

    }
}
