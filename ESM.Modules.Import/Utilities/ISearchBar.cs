using Prism.Commands;

namespace ESM.Modules.Import.Utilities
{
    public interface ISearchBar
    {
        public DelegateCommand GetAllCommand { get; }
        public DelegateCommand<string> SearchCommand { get; }
    }
}
