using CrossUtilites;
using CrossUtilitiesWPF.MiscUtilities;
using System.Data;
using TurtleDialogService.Service.Core;
using TurtleDialogService.Service.Core.Bases;
using TurtleDialogService.Service.Core.Models;
using TurtleDialogService.Service.Core.Models.DialogInput;
using TurtleDialogService.Service.Core.Models.DialogInput.DialogInputTypes;
using TurtleDialogService.Service.Dialogs.Extended;
using TurtleDialogService.Service.Dialogs.Option;

namespace TurtleDialogService.Service
{
    /// <summary>
    /// Holds methods for displaying dialog boxes.
    /// </summary>
    public static class TurtleDialog
    {
        /// <summary>
        /// A constructor that configures the service.
        /// </summary>
        static TurtleDialog()
        {
            DataTemplateManager.LoadDataTemplatesByConvention();
            // Todo: A method with 50 overloads isn't a great approach. Something like a builder or a "DialogComponents" class should be used instead.
            // Todo: Inputs here should only require interfaces, and lists should be ienumerable.
        }

        /// <summary>
        /// Displays the given dialog.
        /// </summary>
        /// <typeparam name="T">The return type of the dialog.</typeparam>
        /// <param name="dialog">The dialog to display.</param>
        /// <returns>The given return type.</returns>
        public static T DisplayDialog<T>(DialogViewModelBase<T> dialog)
        {
            IDialogService dialogService = new DialogService();
            return dialogService.OpenDialog(dialog);
        }

        #region Extended dialog

        /// <summary>
        /// Displays a extended dialog, allowing to display a single input.
        /// </summary>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">The text to display in the window as a header.</param>
        /// <param name="input">The input to display.</param>
        /// <param name="buttons">The list of buttons to display. If not provided, "Ok" and "Cancel" buttons will be set.</param>
        /// <returns>The extended dialog result, containing the inputs and button result.</returns>
        public static ExtendedDialogResult ExtendedDialog(string windowTitle, string message, IDialogInput input, List<DialogButton> buttons = null) => ExtendedDialog(windowTitle, message, input.CreateList(), buttons);

        /// <summary>
        /// Displays a extended dialog, allowing to display different set of inputs and buttons.
        /// </summary>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">The text to display in the window as a header.</param>
        /// <param name="inputs">The list of inputs to display.</param>
        /// <param name="buttons">The list of buttons to display. If not provided, "Ok" and "Cancel" buttons will be set.</param>
        /// <returns>The extended dialog result, containing the inputs and button result.</returns>
        public static ExtendedDialogResult ExtendedDialog(string windowTitle, string message, List<IDialogInput> inputs = null, List<DialogButton> buttons = null)
        {
            // Generate default buttons if none have been provided
            if (buttons == null)
                buttons = GenerateOkCancelButtons();

            return DisplayDialog(new ExtendedDialogViewModel(windowTitle, message, buttons, inputs));
        }


        #endregion

        #region Input dialog

        /// <summary>
        /// Displays a dialog with a single textBox.
        /// </summary>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">The text to display in the window as a header.</param>
        /// <param name="inputTitle">The text that will be displayed next to the input.</param>
        /// <param name="defaultValue">The default value of the input.</param>
        /// <param name="allowEmpty">Whether to allow to return with a empty value or not.</param>
        /// <returns>The input value if the user has clicked the "Ok" button; Otherwise null.</returns>
        public static string Input(string windowTitle, string message, string inputTitle, string defaultValue = "", bool allowEmpty = true)
        {
            TextBoxInput textBoxInput = new TextBoxInput(inputTitle, defaultValue, inputTitle, allowEmpty);
            var result = ExtendedDialog(windowTitle, message, textBoxInput, GenerateOkCancelButtons(textBoxInput));

            // Window closed / Cancel 
            if (result == null || result.Result == "Cancel")
                return null;
            // Ok
            else
                return (string)result.Inputs[0].Value;
        }

        #endregion

        #region Option dialog

        /// <summary>
        /// Displays a option dialog with Yes / No buttons.
        /// </summary>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">The text to display in the window as a header.</param>
        /// <returns>True for the "Yes", false for the the "No" button.</returns>
        public static bool YesNo(string windowTitle, string message) => OptionDialog(windowTitle, message, "Yes", "No") == DialogResult.Yes;

        /// <summary>
        /// Displays a option dialog with Ok / Cancel buttons.
        /// </summary>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">The text to display in the window as a header.</param>
        /// <returns>
        /// <see cref="DialogResult.Yes"/> for the "Ok" button; <br/>
        /// <see cref="DialogResult.No"/> for the "Cancel" button; <br/>
        /// <see cref="DialogResult.Undefined"/> for window closing. <br/>
        /// </returns>
        public static DialogResult OkCancel(string windowTitle, string message) => OptionDialog(windowTitle, message, "Ok", "Cancel");

        /// <summary>
        /// Displays a dialog with only single button to accept the notification.
        /// </summary>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">The text to display in the window as a header.</param>
        /// <param name="buttonContent">The text that will be displayed in a button.</param>
        public static void Notification(string windowTitle, string message, string buttonContent = "Ok") => OptionDialog(windowTitle, message, buttonContent, null);

        /// <summary>
        /// Displays a dialog with two buttons with definable text.
        /// </summary>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">The text to display in the window as a header.</param>
        /// <param name="positiveButtonContent">The text of the "Yes" button.</param>
        /// <param name="negativeButtonContent">The text of the "No" button.</param>
        /// <returns>
        /// <see cref="DialogResult.Yes"/> for the <paramref name="positiveButtonContent"/> button; <br/>
        /// <see cref="DialogResult.No"/> for the <paramref name="negativeButtonContent"/> button; <br/>
        /// <see cref="DialogResult.Undefined"/> for window closing. <br/>
        /// </returns>
        public static DialogResult OptionDialog(string windowTitle, string message, string positiveButtonContent, string negativeButtonContent)
        {
            return DisplayDialog(new OptionDialogViewModel(windowTitle, message, positiveButtonContent, negativeButtonContent));
        }

        #endregion

        #region Option dialog with collection / IEnumerable

        /// <summary>
        /// Displays a dialog with a data grid which displays the <paramref name="collection"/> and a confirmation button.
        /// </summary>
        /// <param name="collection">The collection to display.</param>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="positiveButtonContent">The text of the "Yes" button.</param>
        /// <returns>
        /// <see cref="DialogResult.Yes"/> for the <paramref name="positiveButtonContent"/> button; <br/>
        /// <see cref="DialogResult.Undefined"/> for window closing. <br/>
        /// </returns>
        public static DialogResult Collection<T>(IEnumerable<T> collection, string windowTitle, string positiveButtonContent = "Ok")
            => Collection(collection, windowTitle, null, positiveButtonContent, null);

        /// <summary>
        /// Displays a dialog with a data grid which displays the <paramref name="message"/>, <paramref name="collection"/> and a confirmation button.
        /// </summary>
        /// <param name="collection">The collection to display.</param>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">The text to display in the window as a header.</param>
        /// <param name="positiveButtonContent">The text of the "Yes" button.</param>
        /// <returns>
        /// <see cref="DialogResult.Yes"/> for the <paramref name="positiveButtonContent"/> button; <br/>
        /// <see cref="DialogResult.Undefined"/> for window closing. <br/>
        /// </returns>
        public static DialogResult Collection<T>(IEnumerable<T> collection, string windowTitle, string message, string positiveButtonContent = "Ok")
            => Collection(collection, windowTitle, message, positiveButtonContent, null);

        /// <summary>
        /// Displays a dialog with a data grid which displays the <paramref name="message"/>, <paramref name="collection"/> and two buttons.
        /// </summary>
        /// <param name="collection">The collection to display.</param>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">The text to display in the window as a header.</param>
        /// <param name="positiveButtonContent">The text of the "Yes" button.</param>
        /// <param name="negativeButtonContent">The text of the "No" button.</param>
        /// <returns>
        /// <see cref="DialogResult.Yes"/> for the <paramref name="positiveButtonContent"/> button; <br/>
        /// <see cref="DialogResult.No"/> for the <paramref name="negativeButtonContent"/> button; <br/>
        /// <see cref="DialogResult.Undefined"/> for window closing. <br/>
        /// </returns>
        public static DialogResult Collection<T>(IEnumerable<T> collection, string windowTitle, string message, string positiveButtonContent = "Ok", string negativeButtonContent = "Cancel")
        {
            return DisplayDialog(OptionDialogViewModel.CreateFromCollection(collection, windowTitle, message, positiveButtonContent, negativeButtonContent));
        }

        #endregion

        #region Option dialog with collection / DataTable

        /// <summary>
        /// Displays a dialog with a data grid which displays the <paramref name="dataTable"/> and a confirmation button.
        /// </summary>
        /// <param name="dataTable">The data table to display.</param>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="positiveButtonContent">The text of the "Yes" button.</param>
        /// <returns>
        /// <see cref="DialogResult.Yes"/> for the <paramref name="positiveButtonContent"/> button; <br/>
        /// <see cref="DialogResult.Undefined"/> for window closing. <br/>
        /// </returns>
        public static DialogResult Collection(DataTable dataTable, string windowTitle, string positiveButtonContent = "Ok")
            => Collection(dataTable, windowTitle, null, positiveButtonContent, null);

        /// <summary>
        /// Displays a dialog with a data grid which displays the <paramref name="message"/>, <paramref name="dataTable"/> and a confirmation button.
        /// </summary>
        /// <param name="dataTable">The data table to display.</param>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">The text to display in the window as a header.</param>
        /// <param name="positiveButtonContent">The text of the "Yes" button.</param>
        /// <returns>
        /// <see cref="DialogResult.Yes"/> for the <paramref name="positiveButtonContent"/> button; <br/>
        /// <see cref="DialogResult.Undefined"/> for window closing. <br/>
        /// </returns>
        public static DialogResult Collection(DataTable dataTable, string windowTitle, string message, string positiveButtonContent = "Ok")
            => Collection(dataTable, windowTitle, message, positiveButtonContent, null);

        /// <summary>
        /// Displays a dialog with a data grid which displays the <paramref name="message"/>, <paramref name="dataTable"/> and two buttons.
        /// </summary>
        /// <param name="dataTable">The data table to display.</param>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">The text to display in the window as a header.</param>
        /// <param name="positiveButtonContent">The text of the "Yes" button.</param>
        /// <param name="negativeButtonContent">The text of the "No" button.</param>
        /// <returns>
        /// <see cref="DialogResult.Yes"/> for the <paramref name="positiveButtonContent"/> button; <br/>
        /// <see cref="DialogResult.No"/> for the <paramref name="negativeButtonContent"/> button; <br/>
        /// <see cref="DialogResult.Undefined"/> for window closing. <br/>
        /// </returns>
        public static DialogResult Collection(DataTable dataTable, string windowTitle, string message, string positiveButtonContent = "Ok", string negativeButtonContent = "Cancel")
        {
            return DisplayDialog(OptionDialogViewModel.CreateFromDataTable(dataTable, windowTitle, message, positiveButtonContent, negativeButtonContent));
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Generates "Ok" and "Cancel" buttons.
        /// </summary>
        /// <param name="dialogInput">The dialog input that will be assigned to the "Ok" button.</param>
        private static List<DialogButton> GenerateOkCancelButtons(IDialogInput dialogInput = null) => GenerateOkCancelButtons(dialogInput.CreateList());

        /// <summary>
        /// Generates "Ok" and "Cancel" buttons.
        /// </summary>
        /// <param name="dialogInputs">The dialog inputs that will be assigned to the "Ok" button.</param>
        private static List<DialogButton> GenerateOkCancelButtons(List<IDialogInput> dialogInputs)
        {
            return new List<DialogButton>()
                {
                    new DialogButton("Ok", dialogInputs: dialogInputs, isDefault: true),
                    new DialogButton("Cancel", isCancel: true)
                };
        }

        #endregion
    }
}
