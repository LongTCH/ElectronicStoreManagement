using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.Authentication.ViewModels;
using ESM.Modules.Authentication.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

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
            _regionManager.RegisterViewWithContentRegion<LoginView>();
            _regionManager.RegisterViewWithContentRegion<InputVerificationView>();
            _regionManager.RegisterViewWithContentRegion<ResetPasswordView>();
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

    }
}