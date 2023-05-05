using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ESM.Modules.Export.ViewModels
{
    public class ImportInvoiceViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;

        private ObservableCollection<Import> _bills;
        private DateTime _startDate;
        private DateTime _endDate;

        public ImportInvoiceViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            GetCommand = new(async () => await getCommand());
            ShowCommand = new(showCommand);
            StartTime = DateTime.Now.AddMonths(-1);
            EndTime = DateTime.Now;
        }
        public DelegateCommand GetCommand { get; }
        public DelegateCommand<ICollection<ImportProduct>> ShowCommand { get; }
        public ObservableCollection<Import> Bills
        {
            get { return _bills; }
            set { SetProperty(ref _bills, value); }
        }

        public DateTime StartTime
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value); }
        }

        public DateTime EndTime
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }

        private async Task getCommand()
        {
            Bills = new();
            var list = await _unitOfWork.Imports.GetAll(StartTime, EndTime);
            foreach (var item in list) Bills.Add(item);
        }
        public ICollection<ImportProduct> BillProducts { get; set; }
        private void showCommand(ICollection<ImportProduct> list)
        {
            BillProducts = null;
            BillProducts = list;
            RaisePropertyChanged(nameof(BillProducts));
        }
    }
}
