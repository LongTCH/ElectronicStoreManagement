using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Services;

namespace ViewModels.Commands;

public class CLoseNotifyViewCommand : CommandBase
{
    private readonly INavigationService _navigationService;

    public CLoseNotifyViewCommand(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }
}
