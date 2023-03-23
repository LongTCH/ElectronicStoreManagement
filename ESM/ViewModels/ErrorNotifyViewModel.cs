using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Linq;

namespace ESM.ViewModels
{
    public class ErrorNotifyViewModel : BindableBase, INavigationAware, IModal
    {
        private string _title;
        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public DelegateCommand CloseCommand { get; }
        public ErrorNotifyViewModel(IModalService modalService)
        {
            CloseCommand = new(() => modalService.CloseModal(ViewNames.ErrorModal));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var mess = navigationContext.Parameters["mess"] as string[];
            Message = mess[0];
            Title = mess[1];
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
