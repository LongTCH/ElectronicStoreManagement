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
        public string Header => "Số lượng";
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
            if (IsWeekCheck)
            {
                if (IsLaptopCheck)
                    LaptopList = _unitOfWork.Laptops.GetSoldNumberWeekDuration(start, end);
                if (IsSmartphoneCheck)
                    SmartphoneList = _unitOfWork.Smartphones.GetSoldNumberWeekDuration(start, end);
                if (IsPCCheck)
                    PCList = _unitOfWork.Pcs.GetSoldNumberWeekDuration(start, end);
                if (IsCPUCheck)
                    CPUList = _unitOfWork.Pccpus.GetSoldNumberWeekDuration(start, end);
                if (IsVGACheck)
                    VGAList = _unitOfWork.Vgas.GetSoldNumberWeekDuration(start, end);
                if (IsMonitorCheck)
                    MonitorList = _unitOfWork.Monitors.GetSoldNumberWeekDuration(start, end);
                if (IsHarddiskCheck)
                    HarddiskList = _unitOfWork.Pcharddisks.GetSoldNumberWeekDuration(start, end);
            }
            else if (IsMonthCheck)
            {
                if (IsLaptopCheck)
                    LaptopList = _unitOfWork.Laptops.GetSoldNumberMonthDuration(start, end);
                if (IsSmartphoneCheck)
                    SmartphoneList = _unitOfWork.Smartphones.GetSoldNumberMonthDuration(start, end);
                if (IsPCCheck)
                    PCList = _unitOfWork.Pcs.GetSoldNumberMonthDuration(start, end);
                if (IsCPUCheck)
                    CPUList = _unitOfWork.Pccpus.GetSoldNumberMonthDuration(start, end);
                if (IsVGACheck)
                    VGAList = _unitOfWork.Vgas.GetSoldNumberMonthDuration(start, end);
                if (IsMonitorCheck)
                    MonitorList = _unitOfWork.Monitors.GetSoldNumberMonthDuration(start, end);
                if (IsHarddiskCheck)
                    HarddiskList = _unitOfWork.Pcharddisks.GetSoldNumberMonthDuration(start, end);
            }
            else if (IsQuarterCheck)
            {
                if (IsLaptopCheck)
                    LaptopList = _unitOfWork.Laptops.GetSoldNumberQuarterDuration(start, end);
                if (IsSmartphoneCheck)
                    SmartphoneList = _unitOfWork.Smartphones.GetSoldNumberQuarterDuration(start, end);
                if (IsPCCheck)
                    PCList = _unitOfWork.Pcs.GetSoldNumberQuarterDuration(start, end);
                if (IsCPUCheck)
                    CPUList = _unitOfWork.Pccpus.GetSoldNumberQuarterDuration(start, end);
                if (IsVGACheck)
                    VGAList = _unitOfWork.Vgas.GetSoldNumberQuarterDuration(start, end);
                if (IsMonitorCheck)
                    MonitorList = _unitOfWork.Monitors.GetSoldNumberQuarterDuration(start, end);
                if (IsHarddiskCheck)
                    HarddiskList = _unitOfWork.Pcharddisks.GetSoldNumberQuarterDuration(start, end);
            }
            else if (IsYearCheck)
            {

                if (IsLaptopCheck)
                    LaptopList = _unitOfWork.Laptops.GetSoldNumberYearDuration(start, end);
                if (IsSmartphoneCheck)
                    SmartphoneList = _unitOfWork.Smartphones.GetSoldNumberYearDuration(start, end);
                if (IsPCCheck)
                    PCList = _unitOfWork.Pcs.GetSoldNumberYearDuration(start, end);
                if (IsCPUCheck)
                    CPUList = _unitOfWork.Pccpus.GetSoldNumberYearDuration(start, end);
                if (IsVGACheck)
                    VGAList = _unitOfWork.Vgas.GetSoldNumberYearDuration(start, end);
                if (IsMonitorCheck)
                    MonitorList = _unitOfWork.Monitors.GetSoldNumberYearDuration(start, end);
                if (IsHarddiskCheck)
                    HarddiskList = _unitOfWork.Pcharddisks.GetSoldNumberYearDuration(start, end);
            }
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
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Laptops.GetSoldNumberWeekDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Laptops.GetSoldNumberMonthDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Laptops.GetSoldNumberQuarterDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Laptops.GetSoldNumberYearDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
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
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Smartphones.GetSoldNumberWeekDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Smartphones.GetSoldNumberMonthDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Smartphones.GetSoldNumberQuarterDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Smartphones.GetSoldNumberYearDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
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
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Pcs.GetSoldNumberWeekDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Pcs.GetSoldNumberMonthDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Pcs.GetSoldNumberQuarterDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Pcs.GetSoldNumberYearDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
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
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Pccpus.GetSoldNumberWeekDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Pccpus.GetSoldNumberMonthDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Pccpus.GetSoldNumberQuarterDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Pccpus.GetSoldNumberYearDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
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
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Vgas.GetSoldNumberWeekDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Vgas.GetSoldNumberMonthDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Vgas.GetSoldNumberQuarterDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Vgas.GetSoldNumberYearDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
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
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Monitors.GetSoldNumberWeekDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Monitors.GetSoldNumberMonthDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Monitors.GetSoldNumberQuarterDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Monitors.GetSoldNumberYearDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
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
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Pcharddisks.GetSoldNumberWeekDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Pcharddisks.GetSoldNumberMonthDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Pcharddisks.GetSoldNumberQuarterDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Pcharddisks.GetSoldNumberYearDuration(StartTime, EndTime).Select(x => x.Value).ToList();
                    foreach (var l in list)
                    {
                        values.Add(l);
                    }
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
        private bool _isWeekCheck = true;
        public bool IsWeekCheck
        {
            get => _isWeekCheck;
            set => SetProperty(ref _isWeekCheck, value);
        }

        private bool _isMonthCheck = false;
        public bool IsMonthCheck
        {
            get => _isMonthCheck;
            set => SetProperty(ref _isMonthCheck, value);
        }

        private bool _isQuarterCheck = false;
        public bool IsQuarterCheck
        {
            get => _isQuarterCheck;
            set => SetProperty(ref _isQuarterCheck, value);
        }

        private bool _isYearCheck = false;
        public bool IsYearCheck
        {
            get => _isYearCheck;
            set => SetProperty(ref _isYearCheck, value);
        }
    }
}
