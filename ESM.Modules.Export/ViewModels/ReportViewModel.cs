using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using LiveCharts.Wpf;
using LiveCharts;
using Prism.Commands;
using Prism.Mvvm;
using System;
using ESM.Modules.DataAccess;
using System.Windows;
using Prism.Regions;
using ESM.Modules.Export.Utilities;
using LiveCharts.Configurations;
using System.Threading.Tasks;

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
            AddCommand = new(async() => await addCommand());
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
            get=> series;
            set => SetProperty(ref series, value);
        }
         private async Task addCommand()
         {
            var series = new SeriesCollection(new Charting().GetConfig<ReportMockVm>(SeriesOrientation.Horizontal));
            if (IsLaptopCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = await _unitOfWork.Reports.GetSoldNumberWeekDuration(StartTime, EndTime, ProductType.LAPTOP);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)(double)l.Value,
                            Name = $"Năm {l.Year} tuần {l.Sub}"
                        });
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = await _unitOfWork.Reports.GetSoldNumberMonthDuration(StartTime, EndTime, ProductType.LAPTOP);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)(double)l.Value,
                            Name = $"Năm {l.Year} tháng {l.Sub}"
                        });
                    }
                }
                else if (IsQuarterCheck)
                {
                    var list = await _unitOfWork.Reports.GetSoldNumberQuarterDuration(StartTime, EndTime, ProductType.LAPTOP);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)(double)l.Value,
                            Name = $"Năm {l.Year} quý {l.Sub}"
                        });
                    }
                }
                else if (IsYearCheck)
                {
                    var list = await _unitOfWork.Reports.GetSoldNumberYearDuration(StartTime, EndTime, ProductType.LAPTOP);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)(double)l.Value,
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
                };
                series.Add(laptopSeries);
            }
            if (IsSmartphoneCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = await _unitOfWork.Reports.GetSoldNumberWeekDuration(StartTime, EndTime, ProductType.SMARTPHONE);
                    foreach (var l in list)
                    {
                        values.Add(new ReportMockVm()
                        {
                            Value = (double)(double)l.Value,
                            Name = $"Năm {l.Year} tuần {l.Sub}"
                        });
                    }
                }
                else if (IsMonthCheck)
                {
                    var list = await _unitOfWork.Reports.GetSoldNumberMonthDuration(StartTime, EndTime, ProductType.SMARTPHONE);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberQuarterDuration(StartTime, EndTime, ProductType.SMARTPHONE);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberYearDuration(StartTime, EndTime, ProductType.SMARTPHONE);
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
                };
                series.Add(smartphoneSeries);
            }
            if (IsPCCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = await _unitOfWork.Reports.GetSoldNumberWeekDuration(StartTime, EndTime, ProductType.PC);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberMonthDuration(StartTime, EndTime, ProductType.PC);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberQuarterDuration(StartTime, EndTime, ProductType.PC);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberYearDuration(StartTime, EndTime, ProductType.PC);
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
                };
                series.Add(pcSeries);
            }
            if (IsCPUCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = await _unitOfWork.Reports.GetSoldNumberWeekDuration(StartTime, EndTime, ProductType.CPU);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberMonthDuration(StartTime, EndTime, ProductType.CPU);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberQuarterDuration(StartTime, EndTime, ProductType.CPU);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberYearDuration(StartTime, EndTime, ProductType.CPU);
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
                };
                series.Add(cpuSeries);
            }
            if (IsVGACheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = await _unitOfWork.Reports.GetSoldNumberWeekDuration(StartTime, EndTime, ProductType.VGA);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberMonthDuration(StartTime, EndTime, ProductType.VGA);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberQuarterDuration(StartTime, EndTime, ProductType.VGA);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberYearDuration(StartTime, EndTime, ProductType.VGA);
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
                };
                series.Add(vgaSeries);
            }
            if (IsMonitorCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = await _unitOfWork.Reports.GetSoldNumberWeekDuration(StartTime, EndTime, ProductType.MONITOR);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberMonthDuration(StartTime, EndTime, ProductType.MONITOR);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberQuarterDuration(StartTime, EndTime, ProductType.MONITOR);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberYearDuration(StartTime, EndTime, ProductType.MONITOR);
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
                    Title = "Monitor",
                    Values = values,
                    DataLabels = true,
                    LineSmoothness = 0,
                };
                series.Add(monitorSeries);
            }
            if (IsHarddiskCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = await _unitOfWork.Reports.GetSoldNumberWeekDuration(StartTime, EndTime, ProductType.HARDDISK);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberMonthDuration(StartTime, EndTime, ProductType.HARDDISK);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberQuarterDuration(StartTime, EndTime, ProductType.HARDDISK);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberYearDuration(StartTime, EndTime, ProductType.HARDDISK);
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
                    Title = "Hard Disk",
                    Values = values,
                    DataLabels = true,
                    LineSmoothness = 0,
                };
                series.Add(harddiskSeries);
            }
            if (IsComboCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = await _unitOfWork.Reports.GetSoldNumberWeekDuration(StartTime, EndTime, ProductType.COMBO);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberMonthDuration(StartTime, EndTime, ProductType.COMBO);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberQuarterDuration(StartTime, EndTime, ProductType.COMBO);
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
                    var list = await _unitOfWork.Reports.GetSoldNumberYearDuration(StartTime, EndTime, ProductType.COMBO);
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
                    LineSmoothness = 0,
                };
                series.Add(comboSeries);
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
        public void OnNavigatedFrom(NavigationContext navigationContext) {}
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
        public object ComboList { get; set; }

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
    }
}
