using Prism.Commands;

namespace ESM.Core.ShareServices
{
    public interface IApplicationCommand
    {
        CompositeCommand ResetIndexCommand { get; }
        CompositeCommand ChangeModalState { get; }
    }
    public class ApplicationCommand : IApplicationCommand
    {
        private readonly CompositeCommand _resetIndexCommand = new();
        private readonly CompositeCommand _changeModalState = new();
        public CompositeCommand ResetIndexCommand => _resetIndexCommand;
        public CompositeCommand ChangeModalState => _changeModalState;
    }
}
