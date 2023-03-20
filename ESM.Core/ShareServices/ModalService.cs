using Prism.Regions;
using System;

namespace ESM.Core.ShareServices
{
    public enum ModalType { Error, Information };
    public interface IModalService
    {
        void ShowModal(ModalType type, string message, string title);
        Action? Action { get; set; }
    }
    public class ModalService : IModalService
    {
        private readonly IRegionManager _regionManager;

        public ModalService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public Action? Action { get; set; }

        public void ShowModal(ModalType type, string message, string title)
        {
            var parameters = new NavigationParameters();
            string[] mess = {message, title};
            parameters.Add("mess", mess);
            switch (type)
            {
                case ModalType.Error:
                    _regionManager.RequestNavigate(RegionNames.HostRegion, HostNames.ErrorModal, parameters); break;
                case ModalType.Information:
                    _regionManager.RequestNavigate(RegionNames.HostRegion, HostNames.InformationModal, parameters); break;
            }
            Action?.Invoke();
        }
    }
}
