using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;

namespace ViewModels.NotifyControlViewModels;

public class ErrorNotifyViewModel : ViewModelBase
{
    public string? ErrorMessage { get; }
    public string? ErrorTitle { get; }
    public ICommand CloseNotifyView { get; }

    public ErrorNotifyViewModel(CloseModalNavigationService closeNotifyView)
    {
        CloseNotifyView = new RelayCommand<object>(_ => closeNotifyView.Navigate());
        ConfigurationBuilder configurationBuilder = new();
        try
        {
            configurationBuilder.SetBasePath(Directory.GetParent(
            Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent
            + "\\ViewModels\\JsonConfig");
        }
        catch { }
        configurationBuilder.AddJsonFile("error.json");
        IConfigurationRoot configurationRoot = configurationBuilder.Build();
        ErrorMessage = configurationRoot.GetSection("loginfail").GetSection("ErrorMessage").Value?.ToString();
        ErrorTitle = configurationRoot.GetSection("loginfail").GetSection("ErrorTitle").Value?.ToString();
    }
}
