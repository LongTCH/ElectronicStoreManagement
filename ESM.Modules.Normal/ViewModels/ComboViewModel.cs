using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
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
        private Combo selectedCombo;
        public Combo SelectedCombo
        {
            get => selectedCombo;
            set
            {
                SetProperty(ref selectedCombo, value);
                GetProduct().Await();
            }
        }
        private IEnumerable<ProductDTO> productList;
        public IEnumerable<ProductDTO> ProductList
        {
            get => productList;
            set => SetProperty(ref productList, value);
        }
        public DelegateCommand<ProductDTO> ProductDetailNavigateCommand { get; set; }
        private void navigate(ProductDTO product)
        {
            NavigationParameters parameter = new()
            {
                {"Product" , product}
            };
            _modalService.ShowModal(ViewNames.ProductDetailView, parameter);
        }
        private IEnumerable<Combo> comboList;
        public IEnumerable<Combo> ComboList
        {
            get => comboList;
            set => SetProperty(ref comboList, value);
        }
        private async Task GetProduct()
        {
            ProductList = (await _unitOfWork.Combos.GetListProduct(SelectedCombo));
        }
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            ComboList = await _unitOfWork.Combos.GetAll();
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
