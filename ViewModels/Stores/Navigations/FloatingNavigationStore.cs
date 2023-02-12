using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Stores.Navigations;

public class FloatingNavigationStore : IStore
{
    public event Action? CurrentStoreChanged;
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

    public void Close()
    {
        CurrentViewModel = null;
    }
    public bool IsOpen => CurrentViewModel != null;
}
