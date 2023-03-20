using Prism.Mvvm;
using System;

namespace ESM.Core.ShareServices;

public class ModalNavigationStore
{
    private BindableBase _currentViewModel;
    public BindableBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
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
