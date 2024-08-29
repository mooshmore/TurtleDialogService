using TurtleDialogService.Service.Core.Models.DialogInput;

namespace TurtleDialogService.Service.Core.Models.DialogInput.DialogInputTypes
{
    /// <summary>
    /// A input made out of a single comboBox.
    /// </summary>
    public class TextBoxInput : InputControlBase, IDialogInput
    {
        /// <summary>
        /// A constructor for TextBoxInput.
        /// </summary>
        /// <param name="name">The name that the input will be referenced by.</param>
        /// <param name="defaultValue">The default value of the control.</param>
        /// <param name="inputTitle">The text that will be displayed in the UI next to the input. If not specified the <paramref name="name"/> will be displayed.</param>
        /// <param name="allowEmpty">Whether to require the input to have a value or not.</param>
        public TextBoxInput(string name, string defaultValue = null, string inputTitle = null, bool allowEmpty = true)
            : base(name, DialogInputType.TextBox, defaultValue, inputTitle)
        {
            AllowEmpty = allowEmpty;
        }

        /// <inheritdoc />
        public override bool VerifyCorectness()
        {
            if (!AllowEmpty && string.IsNullOrWhiteSpace((string)Value))
            {
                FocusControl();
                // Todo: this pops up directly inside the input, instead of to its right.
                SignalizeToolTip("This input can't be empty.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Whether to require the input to have a value or not.
        /// </summary>
        public bool AllowEmpty { get; set; }
    }
}
