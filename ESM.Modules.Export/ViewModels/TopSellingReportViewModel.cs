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
            if (IsLaptopCheck) { }
               

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
            if (showLaptop) { }
            if(showSmartphone) { }
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

    }
}
