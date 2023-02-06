using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels.Commands;

public class RelayCommand<T> : ICommand
{
    Action<T> _execute;
    Predicate<T> _predicate = s => true;

    public RelayCommand(Action<T> execute)
    {
        _execute = execute;
    }

    public RelayCommand(Action<T> execute, Predicate<T> predicate)
    {
        _execute = execute;
        _predicate = predicate;
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter) => _predicate((T)parameter);

    public void Execute(object parameter) => _execute((T)parameter);
}
