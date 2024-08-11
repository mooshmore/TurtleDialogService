using CrossUtilites;
using System.Data;
using System.Windows.Input;
using TurtleDialogService.Service.Core;
using TurtleDialogService.Service.Core.Bases;
using TurtleDialogService.Service.Core.Models;

namespace TurtleDialogService.Service.Dialogs.Option
{
    /// <summary>
    /// A viewModel for a OptionDialog that allows to choose between two options, or just one, 
    /// and optionally display a collection in a data grid.
    /// </summary>
    public class OptionDialogViewModel : DialogViewModelBase<DialogResult>
    {
        /// <summary>
        /// A command for the positive button.
        /// </summary>
        public ICommand PositiveActionCommand { get; private set; }

        /// <summary>
        /// A command for the negative button.
        /// </summary>
        public ICommand NegativeActionCommand { get; private set; }

        /// <summary>
        /// The content of the positive button.
        /// </summary>
        public string PositiveButtonContent { get; set; }

        /// <summary>
        /// The content of the negative button.
        /// </summary>
        public string NegativeButtonContent { get; set; }

        /// <summary>
        /// The collection of items to display in a data grid.
        /// </summary>
        public DataTable ItemCollection { get; set; } = null;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">A message that will be displayed in the dialog.</param>
        /// <param name="positiveButtonContent">The content of the positive button.</param>
        /// <param name="negativeButtonContent">The content of the negative button. If not specified, the negative button won't be displayed.</param>
        public OptionDialogViewModel(string windowTitle, string message, string positiveButtonContent, string negativeButtonContent) : base(windowTitle, message)
        {
            PositiveActionCommand = new DialogRelayCommand<IDialogWindow>(PositiveAction);
            NegativeActionCommand = new DialogRelayCommand<IDialogWindow>(NegativeAction);

            PositiveButtonContent = positiveButtonContent;
            NegativeButtonContent = negativeButtonContent;
        }

        /// <summary>
        /// The factory for creating the dialog with a data table.
        /// </summary>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">A message that will be displayed in the dialog.</param>
        /// <param name="positiveButtonContent">The content of the positive button.</param>
        /// <param name="negativeButtonContent">The content of the negative button. If not specified, the negative button won't be displayed.</param>
        /// <param name="dataTable">The collection to display.</param>
        public static OptionDialogViewModel CreateFromDataTable(DataTable dataTable, string windowTitle, string message, string positiveButtonContent, string negativeButtonContent = null)
        {
            var dialog = new OptionDialogViewModel(windowTitle, message, positiveButtonContent, negativeButtonContent)
            {
                ItemCollection = dataTable
            };
            dialog.RaisePropertyChanged(nameof(dialog.ItemCollection));
            return dialog;
        }

        /// <summary>
        /// The factory for creating the dialog with a collection.
        /// </summary>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">A message that will be displayed in the dialog.</param>
        /// <param name="positiveButtonContent">The content of the positive button.</param>
        /// <param name="negativeButtonContent">The content of the negative button. If not specified, the negative button won't be displayed.</param>
        /// <param name="collection">The collection to display.</param>
        public static OptionDialogViewModel CreateFromCollection<T>(IEnumerable<T> collection, string windowTitle, string message, string positiveButtonContent, string negativeButtonContent = null)
        {
            var dialog = new OptionDialogViewModel(windowTitle, message, positiveButtonContent, negativeButtonContent)
            {
                ItemCollection = collection.ToDataTable()
            };
            dialog.RaisePropertyChanged(nameof(dialog.ItemCollection));
            return dialog;
        }

        private void PositiveAction(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResult.Yes);
        }

        private void NegativeAction(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResult.No);
        }
    }
}
