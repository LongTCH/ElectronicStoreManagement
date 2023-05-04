using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESM.Modules.Export.ViewModels
{
    public class ImportInvoiceViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        private DateTime _startDate;
        private DateTime _endDate;
        public ImportInvoiceViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            AddCommand = new(async () => await addCommand());
            
        }
        public DelegateCommand AddCommand { get; }
        private async Task addCommand()
        {

        }

    }
}
