using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.Export.Utilities;
using LiveCharts.Configurations;
using LiveCharts;
using Prism.Commands;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Regions;
using System.Diagnostics;
using System.Windows;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;

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
            Test = new(execute);
            AddCommand = new(addCommand);
            var customerVmMapper = new CartesianMapper<ReportMockVm>()
                .X((value, index) => index) // lets use the position of the item as X
                .Y(value => value.Value); //and PurchasedItems property as Y

            //lets save the mapper globally
            Charting.For<ReportMockVm>(customerVmMapper, SeriesOrientation.Horizontal);

        }
        public DelegateCommand<string> Test { get; }
        public DelegateCommand AddCommand { get; }
        private void execute(string w)
        {
            DateTime start = StartTime;
            DateTime end = EndTime;
            if (StartTime > EndTime)
            {
                MessageBox.Show("Phạm vi ngày không hợp lệ!");
                return;
            }
            if (IsWeekCheck)
            {
                if (IsLaptopCheck)
                    LaptopList = _unitOfWork.Laptops.GetRevenueWeekDuration(start, end);
                if (IsSmartphoneCheck)
                    SmartphoneList = _unitOfWork.Smartphones.GetRevenueWeekDuration(start, end);
                if (IsPCCheck)
                    PCList = _unitOfWork.Pcs.GetRevenueWeekDuration(start, end);
                if (IsCPUCheck)
                    CPUList = _unitOfWork.Pccpus.GetRevenueWeekDuration(start, end);
                if (IsVGACheck)
                    VGAList = _unitOfWork.Vgas.GetRevenueWeekDuration(start, end);
                if (IsMonitorCheck)
                    MonitorList = _unitOfWork.Monitors.GetRevenueWeekDuration(start, end);
                if (IsHarddiskCheck)
                    HarddiskList = _unitOfWork.Pcharddisks.GetRevenueWeekDuration(start, end);
                if (IsComboCheck)
                    ComboList = _unitOfWork.Combos.GetRevenueWeekDuration(start, end);
            }
            else if (IsMonthCheck)
            {
                if (IsLaptopCheck)
                    LaptopList = _unitOfWork.Laptops.GetRevenueMonthDuration(start, end);
                if (IsSmartphoneCheck)
                    SmartphoneList = _unitOfWork.Smartphones.GetRevenueMonthDuration(start, end);
                if (IsPCCheck)
                    PCList = _unitOfWork.Pcs.GetRevenueMonthDuration(start, end);
                if (IsCPUCheck)
                    CPUList = _unitOfWork.Pccpus.GetRevenueMonthDuration(start, end);
                if (IsVGACheck)
                    VGAList = _unitOfWork.Vgas.GetRevenueMonthDuration(start, end);
                if (IsMonitorCheck)
                    MonitorList = _unitOfWork.Monitors.GetRevenueMonthDuration(start, end);
                if (IsHarddiskCheck)
                    HarddiskList = _unitOfWork.Pcharddisks.GetRevenueMonthDuration(start, end);
                if (IsComboCheck)
                    ComboList = _unitOfWork.Combos.GetRevenueMonthDuration(start, end);
            }
            else if (IsQuarterCheck)
            {
                if (IsLaptopCheck)
                    LaptopList = _unitOfWork.Laptops.GetRevenueQuarterDuration(start, end);
                if (IsSmartphoneCheck)
                    SmartphoneList = _unitOfWork.Smartphones.GetRevenueQuarterDuration(start, end);
                if (IsPCCheck)
                    PCList = _unitOfWork.Pcs.GetRevenueQuarterDuration(start, end);
                if (IsCPUCheck)
                    CPUList = _unitOfWork.Pccpus.GetRevenueQuarterDuration(start, end);
                if (IsVGACheck)
                    VGAList = _unitOfWork.Vgas.GetRevenueQuarterDuration(start, end);
                if (IsMonitorCheck)
                    MonitorList = _unitOfWork.Monitors.GetRevenueQuarterDuration(start, end);
                if (IsHarddiskCheck)
                    HarddiskList = _unitOfWork.Pcharddisks.GetRevenueQuarterDuration(start, end);
                if (IsComboCheck)
                    ComboList = _unitOfWork.Combos.GetRevenueQuarterDuration(start, end);
            }
            else if (IsYearCheck)
            {
                if (IsLaptopCheck)
                    LaptopList = _unitOfWork.Laptops.GetRevenueYearDuration(start, end);
                if (IsSmartphoneCheck)
                    SmartphoneList = _unitOfWork.Smartphones.GetRevenueYearDuration(start, end);
                if (IsPCCheck)
                    PCList = _unitOfWork.Pcs.GetRevenueYearDuration(start, end);
                if (IsCPUCheck)
                    CPUList = _unitOfWork.Pccpus.GetRevenueYearDuration(start, end);
                if (IsVGACheck)
                    VGAList = _unitOfWork.Vgas.GetRevenueYearDuration(start, end);
                if (IsMonitorCheck)
                    MonitorList = _unitOfWork.Monitors.GetRevenueYearDuration(start, end);
                if (IsHarddiskCheck)
                    HarddiskList = _unitOfWork.Pcharddisks.GetRevenueYearDuration(start, end);
                if (IsComboCheck)
                    ComboList = _unitOfWork.Combos.GetRevenueYearDuration(start, end);
            }
        }
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
                    var list = _unitOfWork.Laptops.GetRevenueWeekDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Laptops.GetRevenueMonthDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Laptops.GetRevenueQuarterDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Laptops.GetRevenueYearDuration(StartTime, EndTime);
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
                };
                series.Add(laptopSeries);
            }
            if (IsSmartphoneCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Smartphones.GetRevenueWeekDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Smartphones.GetRevenueMonthDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Smartphones.GetRevenueQuarterDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Smartphones.GetRevenueYearDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Pcs.GetRevenueWeekDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Pcs.GetRevenueMonthDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Pcs.GetRevenueQuarterDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Pcs.GetRevenueYearDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Pccpus.GetRevenueWeekDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Pccpus.GetRevenueMonthDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Pccpus.GetRevenueQuarterDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Pccpus.GetRevenueYearDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Vgas.GetRevenueWeekDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Vgas.GetRevenueMonthDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Vgas.GetRevenueQuarterDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Vgas.GetRevenueYearDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Monitors.GetRevenueWeekDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Monitors.GetRevenueMonthDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Monitors.GetRevenueQuarterDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Monitors.GetRevenueYearDuration(StartTime, EndTime);
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
                };
                series.Add(monitorSeries);
            }
            if (IsHarddiskCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Pcharddisks.GetRevenueWeekDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Pcharddisks.GetRevenueMonthDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Pcharddisks.GetRevenueQuarterDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Pcharddisks.GetRevenueYearDuration(StartTime, EndTime);
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
                };
                series.Add(harddiskSeries);
            }
            if (IsComboCheck)
            {
                var values = new ChartValues<ReportMockVm>();
                if (IsWeekCheck)
                {
                    var list = _unitOfWork.Combos.GetRevenueWeekDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Combos.GetRevenueMonthDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Combos.GetRevenueQuarterDuration(StartTime, EndTime);
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
                    var list = _unitOfWork.Combos.GetRevenueYearDuration(StartTime, EndTime);
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
                    LabelPoint = point => point.Y.ToString("C")
                };
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
