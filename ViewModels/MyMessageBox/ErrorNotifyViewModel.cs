﻿using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Stores.Navigations;

namespace ViewModels.MyMessageBox;

public class ErrorNotifyViewModel : ViewModelBase, IErrorNotifyViewModel
{
    private static ErrorNotifyViewModel? _instance = new();
    internal static ErrorNotifyViewModel? Instance => _instance;
    public string? Message { get; set; }
    public string? Title { get; set; }
    private ErrorNotifyViewModel()
    {
        CloseCommand = new RelayCommand<object>(_ => TopLevelStore.Instance!.Close());
    }
    public void Show(string? message, string? title = null)
    {
        Message = message;
        Title = title;
        TopLevelStore.Instance!.CurrentViewModel = this;
    }
    public ICommand CloseCommand { get; }
}
