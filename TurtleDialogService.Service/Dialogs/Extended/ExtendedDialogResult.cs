using System.Collections.Generic;
using System.Linq;
using TurtleDialogService.Service.Core.Models;
using TurtleDialogService.Service.Core.Models.DialogInput;

namespace TurtleDialogService.Service.Dialogs.Extended
{
    /// <summary>
    /// A return class for the <see cref="ExtendedDialogViewModel"/>, holding the result and inputs.
    /// </summary>
    public class ExtendedDialogResult
    {
        /// <summary>
        /// The <see cref="DialogButton.Name"/> of the button that was pressed.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// The list of inputs that were in the in the dialog.
        /// </summary>
        public List<IDialogInput> Inputs { get; set; }

        /// <summary>
        /// Returns the input from the <see cref="Inputs"/> list with the given <see cref="IDialogInput.Name"/>.
        /// </summary>
        /// <param name="inputName">The name of the input to return, <see cref="IDialogInput.Name"/></param>
        /// <returns>The input that matched *the given name.</returns>
        public IDialogInput Input(string inputName) => Inputs.First(input => input.Name == inputName);

        /// <summary>
        /// Returns the input value from the <see cref="Inputs"/> list with the given <see cref="IDialogInput.Name"/>.
        /// </summary>
        /// <typeparam name="T">The type of the return value.</typeparam>
        /// <param name="inputName">The name of the input to retrieve the value from, <see cref="IDialogInput.Name"/></param>
        /// <returns>The value of the given input.</returns>
        public T InputValue<T>(string inputName) => (T)Input(inputName).Value;
    }
}
