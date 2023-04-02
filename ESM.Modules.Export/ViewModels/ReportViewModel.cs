using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using LiveCharts.Wpf;
using LiveCharts;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ESM.Modules.DataAccess;
using MahApps.Metro.Controls;
using System.Windows;

namespace ESM.Modules.Export.ViewModels
{
    public class ReportViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;

        public ReportViewModel(IUnitOfWork unitOfWork, IModalService modalService)
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
            DateTime start = startTime;
            DateTime end = endTime;
            if (startTime > endTime)
            {
                MessageBox.Show("invalid date range!");
                return; 
            }
            if (IsLaptopCheck)
               LaptopList = _unitOfWork.Laptops.GetSoldNumberWeekDuration(start, end);
            
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
            SeriesCollection series = new SeriesCollection();
            if (showLaptop)
            {
                series.Add(new LineSeries
                {
                    Title = "Laptop",
                    Values = (IChartValues) _unitOfWork.Laptops.GetSoldNumberWeekDuration(startTime, endTime).ToList(),
                    //Values = new ChartValues<double> { 10, 9, 26, 38, 35, 40, 45 },
                    DataLabels = true
                });
            }
            if (showSmartphone)
            {
                series.Add(new LineSeries
                {
                    Title = "Smartphone",
                    //Values = new ChartValues<double> { 5, 15, 12, 20, 45, 30, 65 },
                    DataLabels = true
                });
            }
            if (showPC)
            {
                series.Add(new LineSeries
                {
                    Title = "PC",
                    //Values = new ChartValues<double> { 8, 18, 28, 25, 48, 30, 40 },
                    DataLabels = true
                });
            }
            if (showCPU)
            {
                series.Add(new LineSeries
                {
                    Title = "CPU",
                    ///Values = new ChartValues<double> { 9, 13, 49, 39, 40, 52, 43 },
                });
            }
            if (showVGA)
            {
                series.Add(new LineSeries
                {
                    Title = "VGA",
                    //Values = new ChartValues<double> { 7, 17, 27, 37, 47, 36, 30 },
                    DataLabels = true
                });
            }
            if (showMonitor)
            {
                series.Add(new LineSeries
                {
                    Title = "Monitor",
                   // Values = new ChartValues<double> { 4, 14, 24, 28, 44, 54, 40 },
                    DataLabels = true
                });
            }
            if (showHarddisk)
            {
                series.Add(new LineSeries
                {
                    Title = "Harddisk",
                    //Values = new ChartValues<double> { 6, 16, 20, 25, 30, 36, 24 },
                    DataLabels = true
                });
            }
            //ChartPlotter.Series = series;
            //ChartPlotter.LegendLocation = LegendLocation.Right;
        }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public bool IsLaptopCheck { get; set; }
        public bool IsSmartphoneCheck { get; set; }
        public bool IsPCCheck { get; set; }
        public bool IsCPUCheck { get; set; }
        public bool IsVGACheck { get; set; }
        public bool IsMonitorCheck { get; set; }
        public bool IsHarddiskCheck { get; set; }
        public object LaptopList { get; set; }
    }
}
