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
            Test = new(execute);
            AddCommand = new(addCommand);
            var customerVmMapper = new CartesianMapper<ReportMockVm>()
                .Y((value, index) => index) // lets use the position of the item as X
                .X(value => value.Value); //and PurchasedItems property as Y

            //lets save the mapper globally
            Charting.For<ReportMockVm>(customerVmMapper, SeriesOrientation.Vertical);

        }
        public string Legend { get; set; }
        public DelegateCommand<string> Test { get; }
        public DelegateCommand AddCommand { get; }
        private void execute(string w)
        {
            DateTime start = StartTime;
            DateTime end = EndTime;
            if (StartTime > EndTime)
            {
                _modalService.ShowModal(ModalType.Error, "Vui lòng chọn khoảng thời gian hợp lệ", "Thông báo");
                return;
            }
            if (IsLaptopCheck)
                ProductList = _unitOfWork.Reports.GetTopSoldProducts(start, end, ProductType.LAPTOP, 10);
            if (IsSmartphoneCheck)
                ProductList = _unitOfWork.Reports.GetTopSoldProducts(start, end, ProductType.SMARTPHONE, 10);
            if (IsPCCheck)
                ProductList = _unitOfWork.Reports.GetTopSoldProducts(start, end, ProductType.PC, 10);
            if (IsCPUCheck)
                ProductList = _unitOfWork.Reports.GetTopSoldProducts(start, end, ProductType.CPU, 10);
            if (IsVGACheck)
                ProductList = _unitOfWork.Reports.GetTopSoldProducts(start, end, ProductType.VGA, 10);
            if (IsMonitorCheck)
                ProductList = _unitOfWork.Reports.GetTopSoldProducts(start, end, ProductType.MONITOR, 10);
            if (IsHarddiskCheck)
                ProductList = _unitOfWork.Reports.GetTopSoldProducts(start, end, ProductType.HARDDISK, 10);

        }
        private List<string> _labels;
        public List<string> Labels
        {
            get => _labels;
            set => SetProperty(ref  _labels, value);
        }
        private void addCommand()
        {
            var series = new SeriesCollection(new Charting().GetConfig<ReportMockVm>(SeriesOrientation.Vertical));
            var chartValues = new ChartValues<ReportMockVm>();
            var labels = new List<string>();
            IEnumerable<TopSellDTO> productList = null;
            if (IsLaptopCheck)
            {
                productList = _unitOfWork.Reports.GetTopSoldProducts(StartTime, EndTime, ProductType.LAPTOP, 10);
                Legend = "Laptop";
                RaisePropertyChanged(nameof(Legend));
            }
            else if (IsSmartphoneCheck)
            {
                productList = _unitOfWork.Reports.GetTopSoldProducts(StartTime, EndTime, ProductType.SMARTPHONE, 10);
                Legend = "Smartphone";
                RaisePropertyChanged(nameof(Legend));
            }
            else if (IsPCCheck)
            {
                productList = _unitOfWork.Reports.GetTopSoldProducts(StartTime, EndTime, ProductType.PC, 10);
                Legend = "PC";
                RaisePropertyChanged(nameof(Legend));
            }
            else if (IsCPUCheck)
            {
                productList = _unitOfWork.Reports.GetTopSoldProducts(StartTime, EndTime, ProductType.CPU, 10);
                Legend = "CPU";
                RaisePropertyChanged(nameof(Legend));
            }
            else if (IsVGACheck)
            {
                Legend = "VGA";
                RaisePropertyChanged(nameof(Legend));
                productList = _unitOfWork.Reports.GetTopSoldProducts(StartTime, EndTime, ProductType.VGA, 10);
            }
            else if (IsMonitorCheck)
            {
                productList = _unitOfWork.Reports.GetTopSoldProducts(StartTime, EndTime, ProductType.MONITOR, 10);
                Legend = "Monitor";
                RaisePropertyChanged(nameof(Legend));
            }
            else if (IsHarddiskCheck)
            {
                productList = _unitOfWork.Reports.GetTopSoldProducts(StartTime, EndTime, ProductType.HARDDISK, 10);
                Legend = "Hard Disk";
                RaisePropertyChanged(nameof(Legend));
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
            Labels = labels;
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            StartTime = DateTime.Now.AddMonths(-1);
            EndTime = DateTime.Now;
            IsHarddiskCheck = true;
            RaisePropertyChanged(nameof(IsHarddiskCheck));
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
