using ESM.Core;
using ESM.Modules.Authentication.ViewModels;
using ESM.Modules.Authentication.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
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
            RegisterViewWithContentRegion(typeof(LoginView));
            RegisterViewWithContentRegion(typeof(InputVerificationView));
            RegisterViewWithContentRegion(typeof(VerifyEmailView));
            RegisterViewWithContentRegion(typeof(ResetPasswordView));
        }
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<LoginViewModel>();
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>(ViewNames.LoginView);
            containerRegistry.Register<InputVerificationViewModel>();
            containerRegistry.RegisterForNavigation<InputVerificationView, InputVerificationViewModel>(ViewNames.InputVerificationView);
            containerRegistry.Register<VerifyEmailViewModel>();
            containerRegistry.RegisterForNavigation<VerifyEmailView, VerifyEmailViewModel>(ViewNames.VerifyEmailView);
            containerRegistry.Register<ResetPasswordViewModel>();
            containerRegistry.RegisterForNavigation<ResetPasswordView, ResetPasswordViewModel>(ViewNames.ResetPasswordView);
        }

        private void RegisterViewWithContentRegion(Type type)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, type);
            var view = _regionManager.Regions[RegionNames.ContentRegion].Views.First(v => v.GetType().Equals(type));
            _regionManager.Regions[RegionNames.ContentRegion].Deactivate(view);
        }
    }
}