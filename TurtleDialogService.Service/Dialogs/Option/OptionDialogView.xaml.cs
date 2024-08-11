using CrossUtilitiesWPF.MiscUtilities;
using System.Windows.Controls;

namespace TurtleDialogService.Service.Dialogs.Option
{
    /// <summary>
    /// Interaction logic for OptionDialogView.xaml
    /// </summary>
    public partial class OptionDialogView : UserControl
    {
        /// <summary>
        /// Constructor for OptionDialogView.
        /// </summary>
        public OptionDialogView()
        {
            InitializeComponent();

            // Todo: Is this supposed to be like that (250)?
            MainDataGrid.SetAutoSizeMaxColumnWidth(250);
        }
    }
}
