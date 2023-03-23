using ESM.Modules.Authentication.Views;
using ESM.Modules.Import.Views;
using ESM.Modules.Normal.Views;
using System;
using System.Collections.Generic;

namespace ESM.Utilities
{
    public class StaticData
    {
        public static readonly Dictionary<Type, int> ViewTypeToHamburgerIndex = new()
        {
            {typeof(HomeView), 0},
            {typeof(LoginView), 1},
            {typeof(AccountView), 2 },
            {typeof(ProductInputView), 3 },
        };
    }
}
