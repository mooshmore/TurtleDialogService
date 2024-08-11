using CrossUtilitiesWPF.MiscUtilities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TurtleDialogService.Service.Dialogs.Extended
{
    /// <summary>
    /// Interaction logic for ExtendedDialogView.xaml
    /// </summary>
    public partial class ExtendedDialogView : UserControl
    {
        /// <summary>
        /// Construcotr for ExtendedDialogView.
        /// </summary>
        public ExtendedDialogView()
        {
            InitializeComponent();

            // Focus the first input on load
            Inputs.Loaded += new RoutedEventHandler(InputsItemsControlLoaded);
        }

        /// <summary>
        /// Focuses the first input of the <see cref="Inputs"/> itemsControl.
        /// </summary>
        private void InputsItemsControlLoaded(object sender, RoutedEventArgs e)
        {
            DependencyObject firstContainer = Inputs.ItemContainerGenerator.ContainerFromIndex(0);
            Control control = MiscWPFUtilities.FindFirstInput(firstContainer);
            FocusManager.SetFocusedElement(Inputs, control);
        }
    }
}
