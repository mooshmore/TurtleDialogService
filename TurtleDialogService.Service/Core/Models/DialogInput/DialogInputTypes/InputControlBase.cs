using TurtleDialogService.Service.Core;
using TurtleDialogService.Service.Core.Models.DialogInput;
using TurtleDialogService.Service.Core.Models.DialogInput.Modules;

namespace TurtleDialogService.Service.Core.Models.DialogInput.DialogInputTypes
{
    /// <summary>
    /// A base class for input controls in <see cref="DialogService"/>.
    /// </summary>
    public class InputControlBase : InputTooltip, IDialogInput
    {
        /// <summary>
        /// A basic constructor for the base of input controls.
        /// </summary>
        /// <param name="name">The name that the input will be referenced by.</param>
        /// <param name="inputType">The type of the input.</param>
        /// <param name="defaultValue">The default value of the control.</param>
        /// <param name="inputTitle">The text that will be displayed in the UI next to the input. If not specified the <paramref name="name"/> will be displayed.</param>
        public InputControlBase(string name, DialogInputType inputType, object defaultValue = null, string inputTitle = null)
        {
            // Todo: this should be abstract, and input types like checkbox should have its own class. 
            // Todo: inputTitle should be called "label" instead.
            // If the input title hasn't been specified assign the given name as the inputs title
            if (inputTitle == null)
                inputTitle = name;

            Name = name;
            InputTitle = inputTitle;
            InputType = inputType;

            Value = defaultValue;
            OriginalValue = defaultValue;
        }

        /// <summary>
        /// Determines whether the control should be focused. Used in triggers.
        /// </summary>
        public bool ShouldFocus { get; set; }

        /// <summary>
        /// Focuses the control.
        /// </summary>
        public void FocusControl()
        {
            // This awfulness is required for the FocusBehavior to work properly.
            ShouldFocus = false;
            RaisePropertyChanged(nameof(ShouldFocus));
            ShouldFocus = true;
            RaisePropertyChanged(nameof(ShouldFocus));
        }

        /// <inheritdoc />
        public virtual bool VerifyCorectness() => true;

        /// <inheritdoc />
        public object Value { get; set; }

        /// <inheritdoc />
        public DialogInputType InputType { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string InputTitle { get; set; }

        /// <inheritdoc />
        public object OriginalValue { get; set; }

        /// <inheritdoc />
        public bool ValueChanged => OriginalValue?.ToString() == Value?.ToString();
    }
}