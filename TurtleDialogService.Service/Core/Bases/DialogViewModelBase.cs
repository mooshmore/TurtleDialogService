using CrossUtilitesWPF.Bases;

namespace TurtleDialogService.Service.Core.Bases
{
    /// <summary>
    /// A base viewModel for the dialogs.
    /// </summary>
    /// <typeparam name="T">The return type of the dialog.</typeparam>
    public abstract class DialogViewModelBase<T> : ViewModelBase
    {
        /// <summary>
        /// The title of the window.
        /// </summary>
        public string WindowTitle { get; set; }

        /// <summary>
        /// The message that will be displayed in the window.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The result of the dialog.
        /// </summary>
        public T DialogResult { get; set; }

        /// <summary>
        /// A constructor where nothing gets set.
        /// </summary>
        public DialogViewModelBase() : this(string.Empty, string.Empty) { }

        /// <summary>
        /// A constructor for setting the window title.
        /// </summary>
        /// <param name="windowTitle">The title of the window.</param>
        public DialogViewModelBase(string windowTitle) : this(windowTitle, string.Empty) { }

        /// <summary>
        /// A default contructor.
        /// </summary>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">The message that will be displayed in the window.</param>
        public DialogViewModelBase(string windowTitle, string message)
        {
            WindowTitle = windowTitle;
            Message = message;
        }

        /// <summary>
        /// Sets the result and closes the dialog.
        /// </summary>
        /// <param name="dialog">The dialog to close.</param>
        /// <param name="result">The result to set.</param>
        public void CloseDialogWithResult(IDialogWindow dialog, T result)
        {
            DialogResult = result;

            if (dialog != null)
                dialog.DialogResult = true;
        }
    }
}
