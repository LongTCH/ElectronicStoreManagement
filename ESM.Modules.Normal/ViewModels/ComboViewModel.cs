using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Normal.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESM.Modules.Normal.ViewModels
{
    public class ComboViewModel : BindableBase, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        public ComboViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            _modalService = modalService;
            _unitOfWork = unitOfWork;
            ProductDetailNavigateCommand = new(navigate);
        }
        public DelegateCommand<ProductDTO> ProductDetailNavigateCommand { get; set; }
        private void navigate(ProductDTO product)
        {
            new ProductDetailView(new ProductDetailViewModel(product)).Show();
        }
        private IEnumerable<Combo> comboList;
        public IEnumerable<Combo> ComboList
        {
            get => comboList;
            set => SetProperty(ref comboList, value);
        }
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            ComboList = await _unitOfWork.Combos.GetAll();
            RaisePropertyChanged(nameof(ComboList));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}
