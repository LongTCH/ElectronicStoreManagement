using ESM.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESM.Modules.Import.ViewModels
{
    public class ProductManagementViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public ProductManagementViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new(navigate);
        }
        public DelegateCommand<string> NavigateCommand { get; }
        private void navigate(string path)
        {
            _regionManager.RequestNavigateContentRegionWithTrace(path);
        }
    }
}
