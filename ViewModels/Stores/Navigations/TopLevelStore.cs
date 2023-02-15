using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Stores.Navigations;

public class TopLevelStore : IStore
{
    public event Action? CurrentStoreChanged;
    private static TopLevelStore? _instance = new TopLevelStore();
    internal static TopLevelStore? Instance => _instance;
    private TopLevelStore() => CurrentViewModel = null;
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

    public void Close() => CurrentViewModel = null;
    public bool IsOpen => CurrentViewModel != null;
}
