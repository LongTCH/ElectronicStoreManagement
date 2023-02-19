using Models.DTOs;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;

namespace ViewModels.ProductViewModels;

public abstract class ProductViewModel<T> : ViewModelBase
    where T : IProductDTO
{
    protected IUnitOfWork _unitOfWork;
    protected IEnumerable<T>? _productDTOs;
    protected INavigationService _productDetailNavigate;
    protected ProductDetailStore _productDetailStore;
    public List<T>? ProductList { get; set; } = null;
    public List<string> Conditions { get; } = new GetConditionsCommand().Execute();
    protected string? selectedCondition;
    public double MaxPrice { get; set; } = 0;
    private double currentPrice;
    public double CurrentPrice
    {
        get => currentPrice;
        set
        {
            currentPrice = value;
            priceRangeCommand();
        }
    }
    protected ProductViewModel(IUnitOfWork unitOfWork,
        ProductDetailStore productDetailStore,
        INavigationService productDetailNavigate)
    {
        _unitOfWork = unitOfWork;
        _productDetailNavigate = productDetailNavigate;
        _productDetailStore = productDetailStore;
        ProductDetailNavigateCommand = new RelayCommand<T>(s => openDetailCommand(s));
    }
    private void priceRangeCommand()
    {
        Action?.Invoke();
        ProductList = ProductList.Where(x => (double)x.Price <= CurrentPrice).ToList();
        OnPropertyChanged(nameof(ProductList));
    }
    private void openDetailCommand(T dto)
    {
        _productDetailStore.CurrentProduct = dto;
        _productDetailNavigate.Navigate();
    }
    public string? SelectedCondition
    {
        get => selectedCondition;
        set
        {
            selectedCondition = value;
            OnPropertyChanged(nameof(SelectedCondition));
            SelectedConditionChanged();
        }
    }
    public ICommand ProductDetailNavigateCommand { get; set; }
    protected Action? Action { get; set; }
    protected void SelectedConditionChanged()
    {
        if (SelectedCondition == null) return;
        if (SelectedCondition == Conditions[0])
        {
            SelectedCondition = null;
            Action?.Invoke();
        }
        else if (SelectedCondition == Conditions[1])
        {
            ProductList = ProductList?.OrderBy(x => x.Price).ToList();
        }
        else if (SelectedCondition == Conditions[2])
        {
            ProductList = ProductList?.OrderByDescending(x => x.Price).ToList();
        }
        OnPropertyChanged(nameof(ProductList));
    }
}
