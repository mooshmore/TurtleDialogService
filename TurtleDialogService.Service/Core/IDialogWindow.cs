namespace TurtleDialogService.Service.Core
{
    /// <summary>
    /// A window used to display dialogs used by <see cref="DialogService"/>.
    /// </summary>
    public interface IDialogWindow
    {
        /// <summary>
        /// The result of the dialog.
        /// </summary>
        bool? DialogResult { get; set; }

        /// <summary>
        /// The windows data context.
        /// </summary>
        object DataContext { get; set; }

        /// <summary>
        /// Displays the given window as a dialog.
        /// </summary>
        /// <returns>The result of the dialog.</returns>
        bool? ShowDialog();
    }
}
