using System;
using System.Threading.Tasks;

namespace ESM.Core.ShareStores
{
    public interface IViewModel
    {
        Task save();
    }
    public interface IParentViewModel
    {
        void OnChildViewNotify();
    }
    public class ViewModelStore
    {
        public IViewModel? CurrentViewModel { get; set; }
        public IParentViewModel? ParentViewModal { get; set; }
         
        public ViewModelStore()
        {
            CurrentViewModel = null;
            ParentViewModal = null;
        }
    }
}
