using System.Windows;

namespace TurtleDialogService.Service.Core.Bases
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window, IDialogWindow
    {
        /// <summary>
        /// DialogWindow constructor.
        /// </summary>
        public DialogWindow()
        {
            InitializeComponent();
        }
    }
}
