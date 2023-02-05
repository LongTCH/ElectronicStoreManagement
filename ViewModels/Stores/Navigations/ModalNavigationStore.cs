using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Stores.Navigations;

public class ModalNavigationStore : IStore
{
    private ViewModelBase? _currentViewModel;
    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel?.Dispose();
            _currentViewModel = value;
            CurrentStoreChanged?.Invoke();
        }
    }

    public bool IsOpen => CurrentViewModel != null;

    public event Action? CurrentStoreChanged;

    public void Close()
    {
        CurrentViewModel = null;
    }
}
