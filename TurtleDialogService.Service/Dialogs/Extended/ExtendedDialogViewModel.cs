using System.Collections.Generic;
using TurtleDialogService.Service.Core.Bases;
using TurtleDialogService.Service.Core.Models;
using TurtleDialogService.Service.Core.Models.DialogInput;

namespace TurtleDialogService.Service.Dialogs.Extended
{
    /// <summary>
    /// A viewModel for a extended dialog view, which allows to display defined inputs and buttons.
    /// </summary>
    public class ExtendedDialogViewModel : DialogViewModelBase<ExtendedDialogResult>
    {
        /// <summary>
        /// A list of buttons that will be displayed.
        /// </summary>
        public List<DialogButton> Buttons { get; set; } = new List<DialogButton>();

        /// <summary>
        /// A list of inputs that will be displayed.
        /// </summary>
        public List<IDialogInput> Inputs { get; set; }

        /// <summary>
        /// The result of the dialog, containing the result and input values.
        /// </summary>
        public ExtendedDialogResult Result { get; set; } = new ExtendedDialogResult();

        /// <summary>
        /// Constructs a extended dialog, allowing to display different set of inputs and buttons.
        /// </summary>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="message">The text to display in the window as a header.</param>
        /// <param name="inputs">The list of inputs to display.</param>
        /// <param name="buttons">The list of buttons to display.</param>
        public ExtendedDialogViewModel(string windowTitle, string message, List<DialogButton> buttons, List<IDialogInput> inputs = null) : base(windowTitle, message)
        {
            Buttons = buttons;
            foreach (var button in Buttons)
            {
                button.Initialize(Result, this);
            }

            if (inputs != null)
            {
                Inputs = inputs;
                Result.Inputs = inputs;
            }
        }
    }
}
