using Prism.Services.Dialogs;
using System.Windows;

namespace ESM.Views
{
    /// <summary>
    /// Interaction logic for ErrorShowWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window, IDialogWindow
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        public IDialogResult Result { get; set; }
    }
}
