using TurtleDialogService.Service.Dialogs.Extended;

namespace TurtleDialogService.Service.Core.Models.DialogInput
{
    /// <summary>
    /// Stores the available types of inputs used by the <see cref="ExtendedDialogViewModel"/>.
    /// </summary>
    public enum DialogInputType
    {
        /// <summary>
        /// The TextBox control.
        /// </summary>
        TextBox,
        /// <summary>
        /// The ComboBox control.
        /// </summary>
        ComboBox,
        /// <summary>
        /// The nestedComboBox control. 
        /// </summary>
        NestedComboBox,
        /// <summary>
        /// The CheckBox control.
        /// </summary>
        CheckBox,
    }
}
