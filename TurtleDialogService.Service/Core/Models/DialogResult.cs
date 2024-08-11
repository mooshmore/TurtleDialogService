using TurtleDialogService.Service.Core;

namespace TurtleDialogService.Service.Core.Models
{
    /// <summary>
    /// The available dialog results of the <see cref="DialogService"/>.
    /// </summary>
    public enum DialogResult
    {
        /// <summary>
        /// Returned in cases of a window closing.
        /// </summary>
        Undefined,
        /// <summary>
        /// Returned for Yes / Ok buttons.
        /// </summary>
        Yes,
        /// <summary>
        /// Returned for No / Cancel buttons.
        /// </summary>
        No
    }
}
