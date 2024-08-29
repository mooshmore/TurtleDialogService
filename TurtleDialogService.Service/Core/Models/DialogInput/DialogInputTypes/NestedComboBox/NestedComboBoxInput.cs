using System;
using System.Collections.Generic;
using System.Linq;
using TurtleDialogService.Service.Core.Models.DialogInput;

namespace TurtleDialogService.Service.Core.Models.DialogInput.DialogInputTypes.NestedComboBox
{
    /// <summary>
    /// A input that consists of two comboBoxes, where the second "nested" combobox
    /// has its values changed based on the primary comboBox value.
    /// </summary>
    public class NestedComboBoxInput : IDialogInput
    {
        /// <summary>
        /// A constructor for nested comboBoxes, where the values of the second, "nested" combobox 
        /// are dependant on the value of the first comboBox.
        /// </summary>
        /// <param name="name">The name that the input will be referenced by.</param>
        /// <param name="nestedList">
        /// A dictionary of items for both of the comboBoxes, where the dictionary keys are the items of the first comboBox,
        /// and their values are items for the second, "nested" comboBox.</param>
        /// <param name="primaryComboBoxSettings">The set of settings for the primary comboBox.</param>
        /// <param name="secondaryComboBoxSettings">The set of settings for the secondary, "nested" comboBox.</param>
        /// <param name="inputTitle">The text that will be displayed in the UI next to the input. If not specified the <paramref name="name"/> will be displayed.</param>
        public NestedComboBoxInput(string name,
                           Dictionary<string, List<string>> nestedList,
                           NestedComboBoxInputSettings primaryComboBoxSettings,
                           NestedComboBoxInputSettings secondaryComboBoxSettings,
                           string inputTitle = null)
        {
            // Todo: rename input title to labels
            // Todo: Can't this be done to accept any number of lists
            // Todo: The main input title should be more centered and be bold / have bigger font

            // If the input title hasn't been specified assign the given name as the inputs title
            if (inputTitle == null)
                inputTitle = name;

            Name = name;
            InputTitle = inputTitle;

            NestedList = nestedList;

            PrimaryComboBox = primaryComboBoxSettings;
            SecondaryComboBox = secondaryComboBoxSettings;

            // Add an empty item if allow empty is true
            if (PrimaryComboBox.AllowEmpty)
                nestedList.Add(string.Empty, new List<string>() { string.Empty });

            // Add an empty item in each nested list if allow empty is true
            if (SecondaryComboBox.AllowEmpty)
            {
                foreach (var item in nestedList)
                {
                    item.Value.Insert(0, string.Empty);
                }
            }

            PrimaryComboBox.ValuesList = nestedList.Keys.ToList();
            // Update the secondary comboBox items on primary comboBox value change
            PrimaryComboBox.ValueChangedEvent += UpdateSecondaryComboBoxItems;

            UpdateSecondaryComboBoxItems();
        }

        /// <summary>
        /// Updates the items of the secondary comboBox based on the primary comboBox value.
        /// </summary>
        private void UpdateSecondaryComboBoxItems()
        {
            SecondaryComboBox.ValuesList = NestedList[PrimaryComboBox.Value];

            // If empty value is not allowed select the first item
            if (!SecondaryComboBox.AllowEmpty && SecondaryComboBox.ValuesList.Count != 0)
                SecondaryComboBox.Value = SecondaryComboBox.ValuesList[0];
        }

        /// <inheritdoc />
        public bool VerifyCorectness() => true;

        private Dictionary<string, List<string>> NestedList { get; set; }

        /// <summary>
        /// The primary comboBox.
        /// </summary>
        public NestedComboBoxInputSettings PrimaryComboBox { get; set; }

        /// <summary>
        /// The secondary, "nested" comboBox.
        /// </summary>
        public NestedComboBoxInputSettings SecondaryComboBox { get; set; }

        /// <inheritdoc />
        public DialogInputType InputType { get; set; } = DialogInputType.NestedComboBox;

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        [Obsolete("Acces the value by refering to to PrimaryComboBox or SecondaryComboBox value instead.")]
        public object Value
        {
            get => throw new NotSupportedException("Get the value by refering to PrimaryComboBox or SecondaryComboBox value.");
            set => throw new NotSupportedException("Set the value by refering to PrimaryComboBox or SecondaryComboBox value.");
        }

        /// <inheritdoc />
        public string InputTitle { get; set; }

        /// <inheritdoc />
        [Obsolete("Acces the value by refering to to PrimaryComboBox or SecondaryComboBox value instead.")]
        public object OriginalValue
        {
            get => throw new NotSupportedException("Get the value by refering to PrimaryComboBox or SecondaryComboBox value.");
            set => throw new NotSupportedException("Set the value by refering to PrimaryComboBox or SecondaryComboBox value.");
        }

        /// <inheritdoc />
        [Obsolete("Acces the value by refering to to PrimaryComboBox or SecondaryComboBox value instead.")]
        public bool ValueChanged => throw new NotSupportedException("Check the value by refering to PrimaryComboBox or SecondaryComboBox value.");

    }
}
