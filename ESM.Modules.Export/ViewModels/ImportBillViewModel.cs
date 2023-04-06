using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESM.Modules.Export.ViewModels
{
    public class ImportBillViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        private readonly AccountStore _accountStore;

        public ImportBillViewModel(IUnitOfWork unitOfWork, IModalService modalService, AccountStore accountStore)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            _accountStore = accountStore;
        }
    }
}
