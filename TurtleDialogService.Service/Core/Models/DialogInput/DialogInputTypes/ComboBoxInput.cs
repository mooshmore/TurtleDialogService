namespace TurtleDialogService.Service.Core.Models.DialogInput.DialogInputTypes
{
    /// <summary>
    /// A input made out of a single comboBox.
    /// </summary>
    public class ComboBoxInput : InputControlBase, IDialogInput
    {
        /// <summary>
        /// A constructor for comboBox input.
        /// </summary>
        /// <param name="name">The name that the input will be referenced by.</param>
        /// <param name="valuesList">The values that will be displayed as options of the control.</param>
        /// <param name="defaultValue">The default value of the control.</param>
        /// <param name="inputTitle">The text that will be displayed in the UI next to the input. If not specified the <paramref name="name"/> will be displayed.</param>
        /// <param name="allowEmpty">Sets whether the control can be empty. If true, adds a empty element to the list.</param>
        /// <param name="autoSetValue">Sets whether the control should set its value to the first element if the <paramref name="allowEmpty"/> is false and <paramref name="defaultValue"/> is null.</param>
        /// <param name="allowEditing">Sets whether the comboBox value can be edited.</param>
        public ComboBoxInput(string name,
                           List<string> valuesList,
                           string defaultValue = null,
                           string inputTitle = null,
                           bool allowEmpty = false,
                           bool autoSetValue = true,
                           bool allowEditing = false) : base(name, DialogInputType.ComboBox, defaultValue, inputTitle)
        {
            // Todo: only required/most important positions should be in the constructor.
            // All the other stuff like auto set value should be configurable through properties, that applies to all input constructors.
            // Todo: input title / label should be the primary required position, not the input title.
            ValuesList = valuesList;
            AllowEditing = allowEditing;

            // Add an empty element to the list if allow empty is true,
            // if not select the first element.
            if (allowEmpty)
                ValuesList.Add(string.Empty);
            else if (defaultValue == null && autoSetValue)
                Value = ValuesList[0];
        }

        /// <inheritdoc />
        public override bool VerifyCorectness()
        {
            if (!AllowEmpty && string.IsNullOrWhiteSpace((string)Value))
            {
                FocusControl();
                SignalizeToolTip("This input can't be empty.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Whether to require the input to have a value or not.
        /// </summary>
        public bool AllowEmpty { get; set; }

        /// <summary>
        /// Sets whether the input can be edited.
        /// </summary>
        public bool AllowEditing { get; set; }

        /// <summary>
        /// Holds a list of values that are displayed in the input.
        /// </summary>
        public List<string> ValuesList { get; set; }
    }
}
