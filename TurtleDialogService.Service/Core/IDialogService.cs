using TurtleDialogService.Service.Core.Bases;

namespace TurtleDialogService.Service.Core
{
    /// <summary>
    /// A interface of a service for displaying viewModels as dialogs.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Opens the given viewModel as a dialog.
        /// </summary>
        /// <typeparam name="T">The return type of the given dialog.</typeparam>
        /// <param name="viewModel">The viewModel to display.</param>
        /// <returns>The result of the given viewModel.</returns>
        T OpenDialog<T>(DialogViewModelBase<T> viewModel);
    }
}
