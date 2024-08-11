using TurtleDialogService.Service.Dialogs.Extended;

namespace TurtleDialogService.Service.Core.Models.DialogInput
{
    /// <summary>
    /// A interface for dialog inputs used by <see cref="ExtendedDialogViewModel"/>.
    /// </summary>
    public interface IDialogInput
    {
        /// <summary>
        /// The value of the input.
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// The type of the input.
        /// </summary>
        DialogInputType InputType { get; set; }

        /// <summary>
        /// The name that the input is referenced by.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The text that is displayed in the UI next to the input.
        /// </summary>
        string InputTitle { get; set; }

        /// <summary>
        /// Holds the original value that was assigned when constructing the instance.
        /// </summary>
        object OriginalValue { get; set; }

        /// <summary>
        /// Compares the value to the original value and checks if it has changed.
        /// </summary>
        bool ValueChanged { get; }

        /// <summary>
        /// Verifies if the control is filled properly, for example if it has value when it is set that it must have one.
        /// </summary>
        /// <returns>True if the input is filled properly; False if not.</returns>
        bool VerifyCorectness();
    }
}
