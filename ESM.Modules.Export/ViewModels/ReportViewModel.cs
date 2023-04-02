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
using ESM.Modules.DataAccess.Models;
using System.Diagnostics;
using Prism.Regions;

namespace ESM.Modules.Export.ViewModels
{
    public class ReportViewModel : BindableBase, INavigationAware
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
            DateTime start = StartTime;
            DateTime end = EndTime;
            if (StartTime > EndTime)
            {
                MessageBox.Show("invalid date range!");
                return; 
            }
            if (IsLaptopCheck)
               LaptopList = _unitOfWork.Pcharddisks.GetSoldNumberWeekDuration(start, end);
            if (IsSmartphoneCheck)
               SmartphoneList = _unitOfWork.Pcharddisks.GetSoldNumberWeekDuration(start, end);
            if (IsPCCheck)
                PCList = _unitOfWork.Pcharddisks.GetSoldNumberWeekDuration(start, end);
            if (IsCPUCheck)
                CPUList = _unitOfWork.Pcharddisks.GetSoldNumberWeekDuration(start, end);
            if (IsVGACheck)
                VGAList = _unitOfWork.Pcharddisks.GetSoldNumberWeekDuration(start, end);
            if (IsMonitorCheck)
                MonitorList = _unitOfWork.Pcharddisks.GetSoldNumberWeekDuration(start, end);
            if (IsHarddiskCheck)
                HarddiskList = _unitOfWork.Pcharddisks.GetSoldNumberWeekDuration(start, end);

        }
        private SeriesCollection series;
        public SeriesCollection Series
        {
            get=> series;
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
            if (showLaptop)
            {
                var values = new ChartValues<int>();
                var list = _unitOfWork.Laptops.GetSoldNumberWeekDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                foreach(var l in list)
                {
                    values.Add(l);
                }
                series.Add(new LineSeries
                {
                    Title = "Laptop",
                    Values = values,
                    DataLabels = true
                }); ;
            }
            if (showSmartphone)
            {
                var values = new ChartValues<int>();
                var list = _unitOfWork.Smartphones.GetSoldNumberWeekDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                foreach (var l in list)
                {
                    values.Add(l);
                }
                series.Add(new LineSeries
                {
                    Title = "Smartphone",
                    Values = values,
                    DataLabels = true
                }) ;
            }
            if (showPC)
            {
                var values = new ChartValues<int>();
                var list = _unitOfWork.Pcs.GetSoldNumberWeekDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                foreach (var l in list)
                {
                    values.Add(l);
                }
                series.Add(new LineSeries
                {
                    Title = "PC",
                    Values = values,
                    DataLabels = true
                }) ;
            }
            if (showCPU)
            {
                var values = new ChartValues<int>();
                var list = _unitOfWork.Pccpus.GetSoldNumberWeekDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                foreach (var l in list)
                {
                    values.Add(l);
                }
                series.Add(new LineSeries
                {
                    Title = "CPU",
                    Values = values,
                    DataLabels = true
                }) ;
            }
            if (showVGA)
            {
                var values = new ChartValues<int>();
                var list = _unitOfWork.Vgas.GetSoldNumberWeekDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                foreach (var l in list)
                {
                    values.Add(l);
                }
                series.Add(new LineSeries
                {
                    Title = "VGA",
                    Values = values,              
                    DataLabels = true
                });
            }
            if (showMonitor)
            {
                var values = new ChartValues<int>();
                var list = _unitOfWork.Monitors.GetSoldNumberWeekDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                foreach (var l in list)
                {
                    values.Add(l);
                }
                series.Add(new LineSeries
                {
                    Title = "Monitor",
                    Values = values,
                    DataLabels = true
                });
            }
            if (showHarddisk)
            {
                var values = new ChartValues<int>();
                var list = _unitOfWork.Pcharddisks.GetSoldNumberWeekDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                foreach (var l in list)
                {
                    values.Add(l);
                }
                series.Add(new LineSeries
                {
                    Title = "Harddisk",
                    Values = values ,
                    DataLabels = true
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
        public object CPUList { get; set;}
        public object VGAList { get; set; }
        public object MonitorList { get; set; }
        public object HarddiskList { get; set; }
    }
}
