using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.Export.Utilities;
using LiveCharts.Configurations;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using ESM.Modules.Export.Views;

namespace ESM.Modules.Export.ViewModels
{
    public class InvoiceManagementViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        public string Header => "";

        private ObservableCollection<Invoice> _invoices;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _invoiceType;

        public InvoiceManagementViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            AddCommand = new(async () => await addCommand());
            StartTime = DateTime.Now.AddMonths(-1);
            EndTime = DateTime.Now;
        }
        public DelegateCommand AddCommand { get; }

        public ObservableCollection<Invoice> Invoices
        {
            get { return _invoices; }
            set { SetProperty(ref _invoices, value); }
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

        public string InvoiceType
        {
            get { return _invoiceType; }
            set { SetProperty(ref _invoiceType, value); }
        }

        public DelegateCommand SearchCommand { get; }

        private async Task addCommand()
        {
            //var invoices = await _unitOfWork.Bills.GetInvoicesByDateAsync(StartTime, EndTime);
            //if (!string.IsNullOrEmpty(InvoiceType))
            //{
            //    invoices = invoices.Where(x => x.Type.Equals(InvoiceType)).ToList();
            //}
            //Invoices = new ObservableCollection<Invoice>(invoices);
        }
    }
}

