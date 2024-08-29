using CrossUtilites;
using System.Windows.Input;
using TurtleDialogService.Service.Core.Bases;
using TurtleDialogService.Service.Core.Models.DialogInput;
using TurtleDialogService.Service.Dialogs.Extended;

namespace TurtleDialogService.Service.Core.Models
{
    /// <summary>
    /// A dialog button used by the <see cref="ExtendedDialogViewModel"/>.
    /// </summary>
    public class DialogButton : ExtendedDialogResult
    {
        /// <summary>
        /// Creates a new dialog button used by the <see cref="ExtendedDialogViewModel"/>.
        /// </summary>
        /// <param name="content">The text that will be displayed inside the button.</param>
        /// <param name="name">The name that the button will be referenced by.</param>
        /// <param name="dialogInputs">A list of inputs that the button will verify the corectness of before proceeding.</param>
        /// <param name="isDefault">Assigns the Enter key event to this button if set to true.</param>
        /// <param name="isCancel">Assigns the ESC key event to this button is set to true.</param>
        public DialogButton(string content, string name = null, List<IDialogInput> dialogInputs = null, bool isDefault = false, bool isCancel = false)
        {
            // Todo: dialog inputs should be called required inputs to better reflect what the parameter actually does, like "requiredInputs"
            // Todo: DialogButton needs a interface.
            // Todo: cleanup constructors, this is a mess.
            // Todo: Dialog button should automatically "search" for required inputs, instead of requiring to provide them.
            // Todo: constructor overloads have "dialogInputs" parameter name, where they actually only take a single input.
            Content = content;
            if (name == null)
                name = content;

            Name = name;

            _inputs = dialogInputs;

            IsDefault = isDefault;
            IsCancel = isCancel;
        }

        /// <summary>
        /// Creates a new dialog button used by the <see cref="ExtendedDialogViewModel"/>.
        /// </summary>
        /// <param name="content">The text that will be displayed inside the button.</param>
        /// <param name="dialogInputs">A list of inputs that the button will verify the corectness of before proceeding.</param>
        /// <param name="isDefault">Assigns the Enter key event to this button if set to true.</param>
        /// <param name="isCancel">Assigns the ESC key event to this button is set to true.</param>
        public DialogButton(string content, IDialogInput dialogInputs, bool isDefault = false, bool isCancel = false) : this(content, null, dialogInputs.CreateList(), isDefault, isCancel) { }

        /// <summary>
        /// Creates a new dialog button used by the <see cref="ExtendedDialogViewModel"/>.
        /// </summary>
        /// <param name="content">The text that will be displayed inside the button.</param>
        /// <param name="name">The name that the button will be referenced by.</param>
        /// <param name="dialogInput">The input that the button will verify the corectness of before proceeding.</param>
        /// <param name="isDefault">Assigns the Enter key event to this button if set to true.</param>
        /// <param name="isCancel">Assigns the ESC key event to this button is set to true.</param>
        public DialogButton(string content, string name, IDialogInput dialogInput, bool isDefault = false, bool isCancel = false) : this(content, name, dialogInput.CreateList(), isDefault, isCancel) { }

        /// <summary>
        /// Sets the buttons dialog result, viewModel and the button press command.
        /// </summary>
        public void Initialize(ExtendedDialogResult dialogResult, DialogViewModelBase<ExtendedDialogResult> dialogViewModel)
        {
            DialogResult = dialogResult;
            DialogViewModel = dialogViewModel;
            ButtonPressedCommand = new DialogRelayCommand<IDialogWindow>(ButtonPressed);
        }

        /// <summary>
        /// The text that is displayed inside the button.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The name that the button is referenced by.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A command for pressing the button.
        /// </summary>
        public ICommand ButtonPressedCommand { get; set; }

        /// <summary>
        /// Holds the 
        /// </summary>
        public ExtendedDialogResult DialogResult { get; set; }

        /// <summary>
        /// A dialog view model that this button is assigned to.
        /// </summary>
        public DialogViewModelBase<ExtendedDialogResult> DialogViewModel { get; set; }

        /// <summary>
        /// The list of inputs that the button will verify the corectness of before proceeding.
        /// </summary>
        private readonly List<IDialogInput> _inputs;

        /// <summary>
        /// Assigns the Enter key event to this button if set to true.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Assigns the ESC key event to this button is set to true.
        /// </summary>
        public bool IsCancel { get; set; }

        /// <summary>
        /// Verifies the corectness of assigned inputs.
        /// If the method finds a input that returns false, it stops and returns false without verifying thre rest of the inputs.
        /// </summary>
        /// <returns>True if all inputs are correct or there are no inputs assigned; False if not.</returns>
        private bool VerifyInputs() => _inputs?.All(item => item.VerifyCorectness()) ?? true;

        /// <summary>
        /// Sets the dialog result with the button name and closes the window.
        /// </summary>
        private void ButtonPressed(IDialogWindow window)
        {
            if (VerifyInputs())
            {
                DialogResult.Result = Name;
                DialogViewModel.CloseDialogWithResult(window, DialogResult);
            }
        }
    }
}
