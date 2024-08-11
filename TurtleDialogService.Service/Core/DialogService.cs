using System.Windows;
using TurtleDialogService.Service.Core.Bases;

namespace TurtleDialogService.Service.Core
{
    /// <summary>
    /// A service for displaying viewModels as dialogs.
    /// </summary>
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Opens the given ViewModel as a dialog.
        /// The View of the VieWModel is automatically associated with the given ViewModel by <see cref="DataTemplateManager.LoadDataTemplatesByConvention"/>.
        /// </summary>
        /// <typeparam name="T">The return type of the given dialog.</typeparam>
        /// <param name="viewModel">The viewModel to display.</param>
        /// <returns>The result of the given viewModel.</returns>
        public T OpenDialog<T>(DialogViewModelBase<T> viewModel)
        {
            IDialogWindow window = new DialogWindow
            {
                DataContext = viewModel,
                ResizeMode = ResizeMode.NoResize
            };
            ((Window)window).WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ((Window)window).Owner = Application.Current.MainWindow;
            window.ShowDialog();
            return viewModel.DialogResult;
        }
    }
}
