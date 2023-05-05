using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using ESM.Modules.DataAccess.Models;
using System.Collections;
using System.Collections.Generic;

namespace ESM.Modules.Export.ViewModels
{
    public class InvoiceManagementViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;

        private ObservableCollection<Bill> _bills;
        private DateTime _startDate;
        private DateTime _endDate;

        public InvoiceManagementViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            GetCommand = new(async () => await getCommand());
            ShowCommand = new(showCommand);
            StartTime = DateTime.Now.AddMonths(-1);
            EndTime = DateTime.Now;
        }
        public DelegateCommand GetCommand { get; }
        public DelegateCommand<ICollection<BillProduct>> ShowCommand { get; }
        public ObservableCollection<Bill> Bills
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
            var list = await _unitOfWork.Bills.GetAll(StartTime, EndTime);
            foreach (var item in list) Bills.Add(item);
        }
        public ICollection<BillProduct> BillProducts { get;set; }
        private void showCommand(ICollection<BillProduct> list)
        {
            BillProducts = null;
            BillProducts = list;
            RaisePropertyChanged(nameof(BillProducts));
        }
    }
}

