using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;

namespace ViewModels.NotifyControlViewModels;

public class ProductDetailViewModel : ViewModelBase
{
    private readonly ProductDetailStore _currentProduct;
    public ICommand CloseCommand { get; }
    public ProductDetailViewModel(ProductDetailStore currentProduct,
        INavigationService closeModalNavigationService)
    {
        _currentProduct = currentProduct;
        CloseCommand = new RelayCommand<object>(_ => closeModalNavigationService.Navigate());
    }
    public string? ImagePath => _currentProduct.CurrentProduct?.ImagePath;
    public string? DetailPath => _currentProduct.CurrentProduct?.DetailPath;
}
