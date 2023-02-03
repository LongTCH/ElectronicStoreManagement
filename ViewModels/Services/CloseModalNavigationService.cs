using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Stores.Navigations;

namespace ViewModels.Services;

public class CloseModalNavigationService : INavigationService
{
    private readonly ModalNavigationStore _navigationStore;

    public CloseModalNavigationService(ModalNavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public void Navigate()
    {
        _navigationStore.Close();
    }
}
