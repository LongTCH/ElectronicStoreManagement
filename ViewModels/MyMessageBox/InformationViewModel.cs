using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Stores.Navigations;

namespace ViewModels.MyMessageBox;

public class InformationViewModel : ViewModelBase, IErrorNotifyViewModel
{
    private static InformationViewModel? _instance = new();
    internal static InformationViewModel? Instance => _instance;
    public string? Message { get; set; }
    public string? Title { get; set; }

    public ICommand CloseCommand { get; }
    private InformationViewModel()
    {
        CloseCommand = new RelayCommand<object>(_ => TopLevelStore.Instance!.Close());
    }
    public void Show(string? message, string? title = null)
    {
        Message = message;
        Title = title;
        TopLevelStore.Instance!.CurrentViewModel = this;
    }
}
