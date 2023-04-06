using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using static System.Net.Mime.MediaTypeNames;

namespace ESM.Modules.Export.ViewModels
{
    public class TopSellingReportViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        public string Header => "Bán chạy";
        public TopSellingReportViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            Test = new(execute);
            AddCommand = new(addCommand);
        }
        public DelegateCommand<string> Test { get; }
        public DelegateCommand AddCommand { get; }
        private void execute(string w)
        {
            DateTime start = StartTime;
            DateTime end = EndTime;
            if (StartTime > EndTime)
            {
                MessageBox.Show("invalid date range!");
                return;
            }
            if (IsLaptopCheck)
                LaptopList = _unitOfWork.Laptops.GetTopSoldProducts(start, end, 10);
            if (IsSmartphoneCheck)
                SmartphoneList = _unitOfWork.Smartphones.GetTopSoldProducts(start, end, 10);
            if (IsPCCheck)
                PCList = _unitOfWork.Pcs.GetTopSoldProducts(start, end, 10);
            if (IsCPUCheck)
                CPUList = _unitOfWork.Pccpus.GetTopSoldProducts(start, end, 10);
            if (IsVGACheck)
                VGAList = _unitOfWork.Vgas.GetTopSoldProducts(start, end, 10);
            if (IsMonitorCheck)
                MonitorList = _unitOfWork.Monitors.GetTopSoldProducts(start, end, 10);
            if (IsHarddiskCheck)
                HarddiskList = _unitOfWork.Pcharddisks.GetTopSoldProducts(start, end, 10);

        }
        private SeriesCollection series;
        public SeriesCollection Series
        {
            get => series;
            set => SetProperty(ref series, value);
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
            var series = new SeriesCollection();
            var chartValues = new ChartValues<int>();
            var labels = new List<string>();
            if (showLaptop)
            {
                var laptopSales = _unitOfWork.Laptops.GetTopSoldProducts(StartTime, EndTime, 10);
                foreach (var sale in laptopSales)
                {
                    chartValues.Add(sale.Number);
                    labels.Add(sale.Name);
                }
                series.Add(new RowSeries
                {
                    Title = "Laptop",
                    Values = chartValues,
                    DataLabels = true,
                    LabelPoint = point => $"{point.Y}",
                }); ;

            }
            else if (showSmartphone)
            {
                var smartphoneSales = _unitOfWork.Smartphones.GetTopSoldProducts(StartTime, EndTime, 10);
                foreach (var sale in smartphoneSales)
                {
                    chartValues.Add(sale.Number);
                    labels.Add(sale.Name);
                }
                series.Add(new RowSeries
                {
                    Title = "smartphone",
                    Values = chartValues,
                    DataLabels = true,
                    LabelsPosition = BarLabelPosition.Top,
                    LabelPoint = point => $"{point.X}",
                });
            }
            else if (showPC)
            {
                var pcSales = _unitOfWork.Pcs.GetTopSoldProducts(StartTime, EndTime, 10);
                foreach (var sale in pcSales)
                {
                    chartValues.Add(sale.Number);
                    labels.Add(sale.Name);
                }
                series.Add(new RowSeries
                {
                    Title = "pc",
                    Values = chartValues,
                    DataLabels = true,
                    LabelsPosition = BarLabelPosition.Top,
                    LabelPoint = point => $"{point.X}",
                });
            }
            else if (showCPU)
            {
                var cpuSales = _unitOfWork.Pccpus.GetTopSoldProducts(StartTime, EndTime, 10);
                foreach (var sale in cpuSales)
                {
                    chartValues.Add(sale.Number);
                    labels.Add(sale.Name);
                }
                series.Add(new RowSeries
                {
                    Title = "cpu",
                    Values = chartValues,
                    DataLabels = true,
                    LabelsPosition = BarLabelPosition.Top,
                    LabelPoint = point => $"{point.X}",
                });
            }
            else if (showVGA)
            {
                var vgaSales = _unitOfWork.Vgas.GetTopSoldProducts(StartTime, EndTime, 10);
                foreach (var sale in vgaSales)
                {
                    chartValues.Add(sale.Number);
                    labels.Add(sale.Name);
                }
                series.Add(new RowSeries
                {
                    Title = "vga",
                    Values = chartValues,
                    DataLabels = true,
                    LabelsPosition = BarLabelPosition.Top,
                    LabelPoint = point => $"{point.X}",
                });
            }
            else if (showMonitor)
            {
                var monitorSales = _unitOfWork.Monitors.GetTopSoldProducts(StartTime, EndTime, 10);
                foreach (var sale in monitorSales)
                {
                    chartValues.Add(sale.Number);
                    labels.Add(sale.Name);
                }
                series.Add(new RowSeries
                {
                    Title = "monitor",
                    Values = chartValues,
                    DataLabels = true,
                    LabelsPosition = BarLabelPosition.Top,
                    LabelPoint = point => $"{point.X}",
                });
            }
            else if (showHarddisk)
            {
                var harddiskSales = _unitOfWork.Pcharddisks.GetTopSoldProducts(StartTime, EndTime, 10);
                foreach (var sale in harddiskSales)
                {
                    chartValues.Add(sale.Number);
                    labels.Add(sale.Name);
                }
                series.Add(new RowSeries
                {
                    Title = "harddisk",
                    Values = chartValues,
                    DataLabels = true,
                    LabelsPosition = BarLabelPosition.Top,
                    LabelPoint = point => $"{point.X}",
                });
            }
            Series = series;
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            StartTime = DateTime.Now.AddMonths(-1);
            EndTime = DateTime.Now;
            RaisePropertyChanged(nameof(StartTime));
            RaisePropertyChanged(nameof(EndTime));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsLaptopCheck { get; set; }
        public bool IsSmartphoneCheck { get; set; }
        public bool IsPCCheck { get; set; }
        public bool IsCPUCheck { get; set; }
        public bool IsVGACheck { get; set; }
        public bool IsMonitorCheck { get; set; }
        public bool IsHarddiskCheck { get; set; }
        public object LaptopList { get; set; }
        public object SmartphoneList { get; set; }
        public object PCList { get; set; }
        public object CPUList { get; set; }
        public object VGAList { get; set; }
        public object MonitorList { get; set; }
        public object HarddiskList { get; set; }
    }
}
