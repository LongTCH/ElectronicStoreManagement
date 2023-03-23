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
        void Register<T>(string name);
        void CloseModal(string view);
    }
    public class ModalService : IModalService
    {
        private readonly IRegionManager _regionManager;
        private readonly IApplicationCommand _applicationCommand;
        private readonly Dictionary<string, Type> ViewNameToType = new();

        public ModalService(IRegionManager regionManager, 
            IApplicationCommand applicationCommand)
        {
            _regionManager = regionManager;
            _applicationCommand = applicationCommand;
        }

        public Action? Action { get; set; }

        public void CloseModal(string view)
        {
            if (ViewNameToType.ContainsKey(view))
            {
                var v = _regionManager.Regions[RegionNames.HostRegion].Views.First(v => ViewNameToType[view].Equals(v.GetType()));
                _regionManager.Regions[RegionNames.HostRegion].Deactivate(v);
                _applicationCommand.ChangeModalState.Execute(true);
            }
        }
        public void Register<T>(string name)
        {
            if (ViewNameToType.ContainsValue(typeof(T))) return;
            _regionManager.RegisterViewWithRegion(RegionNames.HostRegion, typeof(T));
            var view = _regionManager.Regions[RegionNames.HostRegion].Views.First(v => v.GetType().Equals(typeof(T)));
            _regionManager.Regions[RegionNames.HostRegion].Deactivate(view);
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
            _applicationCommand.ChangeModalState.Execute(true);
        }
        public void ShowModal(string view, NavigationParameters parameter)
        {
            if (ViewNameToType.ContainsKey(view))
            {
                _regionManager.RequestNavigate(RegionNames.HostRegion, view, parameter);
                _applicationCommand.ChangeModalState.Execute(true);
            }
        }
    }
}
