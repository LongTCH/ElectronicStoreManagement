using ESM.Core;
using ESM.Modules.Authentication.ViewModels;
using ESM.Modules.Authentication.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Linq;

namespace ESM.Modules.Authentication
{
    public class AuthenticationModule : IModule
    {
        private readonly IRegionManager _regionManager;
        public AuthenticationModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(LoginView));
            var loginview = _regionManager.Regions[RegionNames.ContentRegion].Views.First(v => v.GetType().Equals(typeof(LoginView)));
            _regionManager.Regions[RegionNames.ContentRegion].Deactivate(loginview);

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(InputVerificationView));
            var inputverifyview = _regionManager.Regions[RegionNames.ContentRegion].Views.First(v => v.GetType().Equals(typeof(InputVerificationView)));
            _regionManager.Regions[RegionNames.ContentRegion].Deactivate(inputverifyview);
        }
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<LoginView>();
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>(ViewNames.LoginView);
            containerRegistry.Register<LoginView>();
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>(ViewNames.InputVerificationView);
        }
    }
}