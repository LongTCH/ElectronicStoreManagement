using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;

namespace ViewModels.ControlViewModels;

public class PopupListItemViewModel : ViewModelBase
{
    public ICommand LaptopNavigation { get; }
    public ICommand MonitorNavigation { get; }
    public ICommand PCNavigation { get; }
    public ICommand PCCPUNavigation { get; }
    public ICommand PCDiskNavigation { get; }
    public ICommand VGANavigation { get; }
    public ICommand SmartPhoneNavigation { get; }
    public PopupListItemViewModel(
        INavigationService closeFloatNavi,
        INavigationService laptopNavi,
        INavigationService monitorNavi,
        INavigationService pcNavi,
        INavigationService pccpuNavi,
        INavigationService pcdiskNavi,
        INavigationService vgaNavi,
        INavigationService smtphoneNavi)
    {
        LaptopNavigation = new RelayCommand<object>(_ => { laptopNavi.Navigate(); closeFloatNavi.Navigate(); });
        MonitorNavigation = new RelayCommand<object>(_ =>{ monitorNavi.Navigate(); closeFloatNavi.Navigate(); });
        PCNavigation = new RelayCommand<object>(_ => { pcNavi.Navigate(); closeFloatNavi.Navigate(); });
        PCCPUNavigation = new RelayCommand<object>(_ => { pccpuNavi.Navigate(); closeFloatNavi.Navigate(); });
        PCDiskNavigation = new RelayCommand<object>(_ => { pcdiskNavi.Navigate(); closeFloatNavi.Navigate(); });
        VGANavigation = new RelayCommand<object>(_ => { vgaNavi.Navigate(); closeFloatNavi.Navigate(); });
        SmartPhoneNavigation = new RelayCommand<object>(_ => { smtphoneNavi.Navigate(); closeFloatNavi.Navigate(); });
    }
}
