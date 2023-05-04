using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.Export.Utilities;
using LiveCharts;
using LiveCharts.Configurations;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESM.Modules.Export.ViewModels
{
    public class TopSellingReportViewModel : BindableBase, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        public string Header => "Bán chạy";
        public TopSellingReportViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            AddCommand = new(async()=> await addCommand());
            StartTime = DateTime.Now.AddMonths(-1);
            EndTime = DateTime.Now;
            IsHarddiskCheck = true;
            var customerVmMapper = new CartesianMapper<ReportMockVm>()
                .Y((value, index) => index) // lets use the position of the item as X
                .X(value => value.Value); //and PurchasedItems property as Y

            //lets save the mapper globally
            Charting.For<ReportMockVm>(customerVmMapper, SeriesOrientation.Vertical);

        }
        public string Legend { get; set; }
        public DelegateCommand AddCommand { get; }
        private List<string> _labels;
        public List<string> Labels
        {
            get => _labels;
            set => SetProperty(ref  _labels, value);
        }
        private async Task addCommand()
        {
            if (StartTime > EndTime)
            {
                string message = "Nhập khoảng thời gian hợp lệ";
                string title = "Time Error";
                _modalService.ShowModal(ModalType.Error, message, title);
                return;
            }
            var series = new SeriesCollection(new Charting().GetConfig<ReportMockVm>(SeriesOrientation.Vertical));
            var chartValues = new ChartValues<ReportMockVm>();
            var labels = new List<string>();
            IEnumerable<TopSellDTO> productList = null;
            if (IsLaptopCheck)
            {
                productList = await _unitOfWork.Reports.GetTopSoldProducts(StartTime, EndTime, ProductType.LAPTOP, 10);
                Legend = "Laptop";
                
            }
            else if (IsSmartphoneCheck)
            {
                productList = await _unitOfWork.Reports.GetTopSoldProducts(StartTime, EndTime, ProductType.SMARTPHONE, 10);
                Legend = "Smartphone";
                
            }
            else if (IsPCCheck)
            {
                productList = await _unitOfWork.Reports.GetTopSoldProducts(StartTime, EndTime, ProductType.PC, 10);
                Legend = "PC";
                
            }
            else if (IsCPUCheck)
            {
                productList = await _unitOfWork.Reports.GetTopSoldProducts(StartTime, EndTime, ProductType.CPU, 10);
                Legend = "CPU";
                
            }
            else if (IsVGACheck)
            {
                Legend = "VGA";
                
                productList = await _unitOfWork.Reports.GetTopSoldProducts(StartTime, EndTime, ProductType.VGA, 10);
            }
            else if (IsMonitorCheck)
            {
                productList = await _unitOfWork.Reports.GetTopSoldProducts(StartTime, EndTime, ProductType.MONITOR, 10);
                Legend = "Monitor";
                
            }
            else if (IsHarddiskCheck)
            {
                productList = await _unitOfWork.Reports.GetTopSoldProducts(StartTime, EndTime, ProductType.HARDDISK, 10);
                Legend = "Hard Disk";
                
            }
            foreach (var sale in productList)
            {
                chartValues.Add(new ReportMockVm()
                {
                    Name = sale.Name,
                    Value = sale.Number,
                });
                labels.Add(sale.Id);
            }
            Customers = chartValues;
            RaisePropertyChanged(nameof(Customers));
            RaisePropertyChanged(nameof(Legend));
            Labels = labels;
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        public Func<double, string> Formatter { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsLaptopCheck { get; set; }
        public bool IsSmartphoneCheck { get; set; }
        public bool IsPCCheck { get; set; }
        public bool IsCPUCheck { get; set; }
        public bool IsVGACheck { get; set; }
        public bool IsMonitorCheck { get; set; }
        public bool IsHarddiskCheck { get; set; }
        public object ProductList { get; set; }
        public ChartValues<ReportMockVm> Customers { get; set; }
    }
}
