using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESM.Modules.Import.ViewModels
{
    public class LaptopInputViewModel : BaseProductInputViewModel<LaptopDTO>
    {
        public LaptopInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {
        }

        public string Header => "Laptop";

        protected override void clearCommand()
        {
            throw new NotImplementedException();
        }

        protected override void findCommand()
        {
            throw new NotImplementedException();
        }

        protected override void saveCommand()
        {
            throw new NotImplementedException();
        }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
    }
}
