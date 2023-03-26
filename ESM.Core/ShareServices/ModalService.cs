using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESM.Core.ShareServices
{
    public enum ModalType { Error, Information };
    public interface IModal
    {
        DelegateCommand CloseCommand { get; }
    }
    public interface IModalService
    {
        void ShowModal(ModalType type, string message, string title);
        void ShowModal(string view, NavigationParameters parameter);
        void CloseModal();
    }
    public class ModalService : IModalService
    {
        private readonly IRegionManager _regionManager;
        private readonly IApplicationCommand _applicationCommand;

        public ModalService(IRegionManager regionManager,
            IApplicationCommand applicationCommand)
        {
            _regionManager = regionManager;
            _applicationCommand = applicationCommand;
        }

        public Action? Action { get; set; }

        public void CloseModal()
        {
            foreach (var v in _regionManager.Regions[RegionNames.HostRegion].ActiveViews)
            { _regionManager.Regions[RegionNames.HostRegion].Deactivate(v); }
            _applicationCommand.ChangeModalState.Execute(true);
        }

        public void ShowModal(ModalType type, string message, string title)
        {
            var parameters = new NavigationParameters();
            string[] mess = { message, title };
            parameters.Add("mess", mess);
            switch (type)
            {
                case ModalType.Error:
                    _regionManager.RequestNavigate(RegionNames.HostRegion, ViewNames.ErrorModal, parameters); break;
                case ModalType.Information:
                    _regionManager.RequestNavigate(RegionNames.HostRegion, ViewNames.InformationModal, parameters); break;
            }
            _applicationCommand.ChangeModalState.Execute(true);
        }
        public void ShowModal(string view, NavigationParameters parameter)
        {
            _regionManager.RequestNavigate(RegionNames.HostRegion, view, parameter);
            _applicationCommand.ChangeModalState.Execute(true);
        }
    }
}
