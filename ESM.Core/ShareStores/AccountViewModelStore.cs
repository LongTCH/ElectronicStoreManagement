using System;
using System.Threading.Tasks;

namespace ESM.Core.ShareStores
{
    public interface IAccountViewModelStore
    {
        Task save();
    }
    public class AccountViewModelStore
    {
        public IAccountViewModelStore? CurrentAccountViewModel { get; set; }

        public AccountViewModelStore()
        {
            CurrentAccountViewModel = null;
        }
        public Action Back;
    }
}
