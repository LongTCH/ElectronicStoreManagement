using Microsoft.Extensions.Configuration;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;

namespace ViewModels;

public class ErrorViewModel : ViewModelBase
{
    public string? ErrorMessage { get; }
    public string? ErrorTitle { get; }
    public ICommand CloseNotifyView { get; }
    
    public ErrorViewModel(CloseModalNavigationService closeNotifyView, IConfigurationRoot configurationRoot)
    {
        CloseNotifyView = new CLoseNotifyViewCommand(closeNotifyView);
        ErrorMessage = configurationRoot.GetSection("loginfail").GetSection("ErrorMessage").Value?.ToString();
        ErrorTitle = configurationRoot.GetSection("loginfail").GetSection("ErrorTitle").Value?.ToString();
    }
}
