using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Stores.Navigations;

namespace ViewModels.Services;

public class ModalNavigationService<TViewModel> : INavigationService
        where TViewModel : ViewModelBase
{
    private readonly ModalNavigationStore _navigationStore;
    private readonly Func<TViewModel> _createViewModel;
    private readonly INavigationService _closeFloatService;

    public ModalNavigationService(ModalNavigationStore navigationStore, 
        Func<TViewModel> createViewModel,
        INavigationService closeFloatService)
    {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
        _closeFloatService = closeFloatService;
    }

    public void Navigate()
    {
        _navigationStore.CurrentViewModel = _createViewModel();
        _closeFloatService.Navigate();
    }
}
