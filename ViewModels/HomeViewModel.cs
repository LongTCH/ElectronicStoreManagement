using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;

namespace ViewModels;

public class HomeViewModel : ViewModelBase
{
    public string WelcomeMessage => "Welcome to my application.";

    public ICommand NavigateLoginCommand { get; }

    public HomeViewModel(INavigationService loginNavigationService)
    {
        NavigateLoginCommand = new NavigateCommand(loginNavigationService);
    }
}
