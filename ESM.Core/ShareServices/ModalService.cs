using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ESM.Core.ShareServices
{
    public enum ModalType { Error, Information };
    public interface IModalService
    {
        void ShowModal(ModalType type, string message, string title);
        Action? Action { get; set; }
        void ShowModal(string view, NavigationParameters parameter);
        void Register<T>(string name);
        void CloseModal(string view);
    }
    public class ModalService : IModalService
    {
        private readonly IRegionManager _regionManager;
        private Dictionary<string, Type> ViewNameToType = new();

        public ModalService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public Action? Action { get; set; }

        public void CloseModal(string view)
        {
            if (ViewNameToType.ContainsKey(view))
            {
                var v = _regionManager.Regions[RegionNames.HostRegion].Views.First(v => ViewNameToType[view].Equals(v.GetType()));
                _regionManager.Regions[RegionNames.HostRegion].Deactivate(v);
                Action?.Invoke();
            }
        }

        public void Register<T>(string name)
        {
            ViewNameToType.Add(name, typeof(T));
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
            Action?.Invoke();
        }
        public void ShowModal(string view, NavigationParameters parameter)
        {
            if (ViewNameToType.ContainsKey(view))
            {
                _regionManager.RequestNavigate(RegionNames.HostRegion, view, parameter);
                Action?.Invoke();
            }
        }
    }
}
