using CrossUtilitesWPF.Bases;
using System.Windows.Input;

namespace TurtleDialogService.Service.Core.Models.DialogInput.Modules
{
    /// <summary>
    /// A tooltip functionality for dialog inputs.
    /// Allows to control the display state of the tooltip.
    /// </summary>
    public class InputTooltip : ViewModelBase
    {
        /// <summary>
        /// Constructor for the InputTooltip.
        /// </summary>
        public InputTooltip()
        {
            ElementActionCommand = new RelayCommand(HideTooltip);
        }

        /// <summary>
        /// Sets the tooltip text to the given text and sets the visibility to true.
        /// </summary>
        /// <param name="text">The text to be displayed in the tooltip.</param>
        public void SignalizeToolTip(string text)
        {
            ToolTipText = text;
            ToolTipVisibility = true;
        }

        private string _toolTipText;

        /// <summary>
        /// The text of the tooltip.
        /// </summary>
        public string ToolTipText
        {
            get { return _toolTipText; }
            set
            {
                _toolTipText = value;
                RaisePropertyChanged();
            }
        }

        private bool _toolTipVisibility = false;

        /// <summary>
        /// The visibility of the tooltip.
        /// </summary>
        public bool ToolTipVisibility
        {
            get { return _toolTipVisibility; }
            set
            {
                _toolTipVisibility = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// A command that is triggered whenever the element that is assigned 
        /// to the tooltip triggers some action, like a click or entering a value.
        /// </summary>
        public ICommand ElementActionCommand { get; set; }

        private void HideTooltip(object parameter) => ToolTipVisibility = false;
    }
}
