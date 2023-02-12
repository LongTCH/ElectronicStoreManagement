using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Stores.Navigations;

namespace ViewModels.Services;

public  class CloseFloatNavigationService : INavigationService
{
    private readonly FloatingNavigationStore _navigationStore;

    public CloseFloatNavigationService(FloatingNavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public void Navigate()
    {
        _navigationStore.Close();
    }
}
