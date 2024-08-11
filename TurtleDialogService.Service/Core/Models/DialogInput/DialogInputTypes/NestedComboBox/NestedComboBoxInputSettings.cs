using CrossUtilitesWPF.Bases;

namespace TurtleDialogService.Service.Core.Models.DialogInput.DialogInputTypes.NestedComboBox
{
    /// <summary>
    /// A nested comboBox input settings used in <see cref="NestedComboBoxInput"/> input.
    /// </summary>
    public class NestedComboBoxInputSettings : ViewModelBase
    {
        /// <summary>
        /// Creates a new comboBox input settings used in <see cref="NestedComboBoxInput"/> input.
        /// </summary>
        /// <param name="defaultValue">The default value of the control.</param>
        /// <param name="inputTitle">The text that will be displayed in the UI next to the input.</param>
        /// <param name="allowEmpty">Sets whether the control can be empty. If true, adds a empty element to the list.</param>
        /// <param name="allowEditing">Sets whether the comboBox value can be edited.</param>
        public NestedComboBoxInputSettings(string defaultValue = "", string inputTitle = "", bool allowEmpty = false, bool allowEditing = false)
        {
            InputTitle = inputTitle;
            Value = defaultValue;
            OriginalValue = defaultValue;
            AllowEmpty = allowEmpty;
            AllowEditing = allowEditing;
        }

        /// <summary>
        /// A event that is triggered when the value of this comboBox changes.
        /// </summary>
        public Action ValueChangedEvent { get; set; }

        /// <summary>
        /// Sets whether the input can be edited.
        /// </summary>
        public bool AllowEditing { get; set; }

        /// <summary>
        /// The text that is displayed in the UI next to the input.
        /// </summary>
        public string InputTitle { get; set; }

        /// <summary>
        /// Sets whether the control can be empty.
        /// </summary>
        public bool AllowEmpty { get; set; }

        private string _value;

        /// <summary>
        /// The value of the input.
        /// </summary>
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                RaisePropertyChanged();
                ValueChangedEvent?.Invoke();
            }
        }

        private List<string> _valuesList;

        /// <summary>
        /// Holds a list of values that are displayed in the input.
        /// </summary>
        public List<string> ValuesList
        {
            get => _valuesList;
            set
            {
                _valuesList = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Holds the original value that was assigned when constructing the instance.
        /// </summary>
        private readonly string OriginalValue;

        /// <summary>
        /// Compares the value to the original value and checks if it has changed.
        /// </summary>
        public bool ValueChanged => OriginalValue.ToString() != Value.ToString();

    }

}
