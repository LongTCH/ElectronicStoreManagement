using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.Export.Utilities;
using LiveCharts.Configurations;
using LiveCharts;
using Prism.Commands;
using System;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using LiveCharts.Wpf;
using ESM.Modules.DataAccess;

namespace ESM.Modules.Export.ViewModels
{
    public class RevenueViewModel : BindableBase, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        public string Header => "Doanh thu";
        public RevenueViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            AddCommand = new(addCommand);
            var customerVmMapper = new CartesianMapper<ReportMockVm>()
                .X((value, index) => index) // lets use the position of the item as X
                .Y(value => value.Value); //and PurchasedItems property as Y

            //lets save the mapper globally
            Charting.For<ReportMockVm>(customerVmMapper, SeriesOrientation.Horizontal);

        }
        public DelegateCommand AddCommand { get; }
        private SeriesCollection series;
        public SeriesCollection Series
        {
            get => series;
            set => SetProperty(ref series, value);
        }
        private void addCommand()
        {
            var series = new SeriesCollection(new Charting().GetConfig<ReportMockVm>(SeriesOrientation.Horizontal));
            if (IsLaptopCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueWeekDuration(StartTime, EndTime, ProductType.LAPTOP);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tuần {l.Sub}"
                        });
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueMonthDuration(StartTime, EndTime, ProductType.LAPTOP);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tháng {l.Sub}"
                        });
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueQuarterDuration(StartTime, EndTime, ProductType.LAPTOP);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} quý {l.Sub}"
                        });
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueYearDuration(StartTime, EndTime, ProductType.LAPTOP);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year}"
                        });
                    }
                }
                var laptopSeries = new LineSeries
                {
                    Title = "Laptop",
                    Values = values,
                    DataLabels = true,
                    LineSmoothness = 0,
                    LabelPoint = point => $"{point.Y:N0} đ"
                };
                series.Add(laptopSeries);
            }
            if (IsSmartphoneCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueWeekDuration(StartTime, EndTime, ProductType.SMARTPHONE);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tuần {l.Sub}"
                        });
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueMonthDuration(StartTime, EndTime, ProductType.SMARTPHONE);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tháng {l.Sub}"
                        });
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueQuarterDuration(StartTime, EndTime, ProductType.SMARTPHONE);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} quý {l.Sub}"
                        });
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueYearDuration(StartTime, EndTime, ProductType.SMARTPHONE);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year}"
                        });
                    }
                }
                var smartphoneSeries = new LineSeries
                {
                    Title = "Smartphone",
                    Values = values,
                    DataLabels = true,
                    LineSmoothness = 0,
                    LabelPoint = point => $"{point.Y:N0} đ"
                };
                series.Add(smartphoneSeries);
            }
            if (IsPCCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueWeekDuration(StartTime, EndTime, ProductType.PC);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tuần {l.Sub}"
                        });
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueMonthDuration(StartTime, EndTime, ProductType.PC);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tháng {l.Sub}"
                        });
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueQuarterDuration(StartTime, EndTime, ProductType.PC);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} quý {l.Sub}"
                        });
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueYearDuration(StartTime, EndTime, ProductType.PC);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year}"
                        });
                    }
                }
                var pcSeries = new LineSeries
                {
                    Title = "PC",
                    Values = values,
                    DataLabels = true,
                    LineSmoothness = 0,
                    LabelPoint = point => $"{point.Y:N0} đ"
                };
                series.Add(pcSeries);
            }
            if (IsCPUCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueWeekDuration(StartTime, EndTime, ProductType.CPU);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tuần {l.Sub}"
                        });
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueMonthDuration(StartTime, EndTime, ProductType.CPU);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tháng {l.Sub}"
                        });
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueQuarterDuration(StartTime, EndTime, ProductType.CPU);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} quý {l.Sub}"
                        });
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueYearDuration(StartTime, EndTime, ProductType.CPU);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year}"
                        });
                    }
                }
                var cpuSeries = new LineSeries
                {
                    Title = "CPU",
                    Values = values,
                    DataLabels = true,
                    LineSmoothness = 0,
                    LabelPoint = point => $"{point.Y:N0} đ"
                };
                series.Add(cpuSeries);
            }
            if (IsVGACheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueWeekDuration(StartTime, EndTime, ProductType.VGA);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tuần {l.Sub}"
                        });
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueMonthDuration(StartTime, EndTime, ProductType.VGA);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tháng {l.Sub}"
                        });
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueQuarterDuration(StartTime, EndTime, ProductType.VGA);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} quý {l.Sub}"
                        });
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueYearDuration(StartTime, EndTime, ProductType.VGA);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year}"
                        });
                    }
                }
                var vgaSeries = new LineSeries
                {
                    Title = "VGA",
                    Values = values,
                    DataLabels = true,
                    LineSmoothness = 0,
                    LabelPoint = point => $"{point.Y:N0} đ"
                };
                series.Add(vgaSeries);
            }
            if (IsMonitorCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueWeekDuration(StartTime, EndTime, ProductType.MONITOR);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tuần {l.Sub}"
                        });
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueMonthDuration(StartTime, EndTime, ProductType.MONITOR);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tháng {l.Sub}"
                        });
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueQuarterDuration(StartTime, EndTime, ProductType.MONITOR);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} quý {l.Sub}"
                        });
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueYearDuration(StartTime, EndTime, ProductType.MONITOR);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year}"
                        });
                    }
                }
                var monitorSeries = new LineSeries
                {
                    Title = "PC",
                    Values = values,
                    DataLabels = true,
                    LineSmoothness = 0,
                    LabelPoint = point => $"{point.Y:N0} đ"
                };
                series.Add(monitorSeries);
            }
            if (IsHarddiskCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueWeekDuration(StartTime, EndTime, ProductType.HARDDISK);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tuần {l.Sub}"
                        });
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueMonthDuration(StartTime, EndTime, ProductType.HARDDISK);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tháng {l.Sub}"
                        });
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueQuarterDuration(StartTime, EndTime, ProductType.HARDDISK);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} quý {l.Sub}"
                        });
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueYearDuration(StartTime, EndTime, ProductType.HARDDISK);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year}",
                        });
                    }
                }
                var harddiskSeries = new LineSeries
                {
                    Title = "Harddisk",
                    Values = values,
                    DataLabels = true,
                    LineSmoothness=0,
                    LabelPoint = point => $"{point.Y:N0} đ"
                };
                series.Add(harddiskSeries);
            }
            if (IsComboCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueWeekDuration(StartTime, EndTime, ProductType.COMBO);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tuần {l.Sub}"
                        });
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueMonthDuration(StartTime, EndTime, ProductType.COMBO);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tháng {l.Sub}"
                        });
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueQuarterDuration(StartTime, EndTime, ProductType.COMBO);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} quý {l.Sub}"
                        });
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueYearDuration(StartTime, EndTime, ProductType.COMBO);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year}"
                        });
                    }
                }
                var comboSeries = new LineSeries
                {
                    Title = "Combo",
                    Values = values,
                    DataLabels = true,
                    LineSmoothness=0,
                    LabelPoint = point => $"{point.Y:N0} đ"
                };
                series.Add(comboSeries);
            }
            if (IsTotalCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueWeekDuration(StartTime, EndTime, ProductType.TOTAL);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tuần {l.Sub}"
                        });
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueMonthDuration(StartTime, EndTime, ProductType.TOTAL);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} tháng {l.Sub}"
                        });
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueQuarterDuration(StartTime, EndTime, ProductType.TOTAL);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year} quý {l.Sub}"
                        });
                    }
                }
                else if (IsYearCheck)
                {
                    var list = _unitOfWork.Reports.GetRevenueYearDuration(StartTime, EndTime, ProductType.TOTAL);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)l.Value,
                            Name = $"Năm {l.Year}"
                        });
                    }
                }
                var totalSeries = new LineSeries
                {
                    Title = "Total",
                    Values = values,
                    DataLabels = true,
                    LineSmoothness = 0,
                    LabelPoint = point => $"{point.Y:N0} đ"
                };
                series.Add(totalSeries);
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
        public void OnNavigatedFrom(NavigationContext navigationContext) { }

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
        public object ComboList { get; set; }
        public object TotalList { get; set; }

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
        private bool _isComboCheck = false;
        public bool IsComboCheck
        {
            get => _isComboCheck;
            set => SetProperty(ref _isComboCheck, value);
        }
        private bool _isTotalCheck = false;
        public bool IsTotalCheck
        {
            get => _isTotalCheck;
            set => SetProperty(ref _isTotalCheck, value);
        }
    }
}
