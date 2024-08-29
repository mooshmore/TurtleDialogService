using TurtleDialogService.Service.Core.Models.DialogInput.DialogInputTypes.NestedComboBox;
using TurtleDialogService.Service.Core.Models.DialogInput.DialogInputTypes;
using TurtleDialogService.Service.Core.Models.DialogInput;
using TurtleDialogService.Service.Core.Models;
using TurtleDialogService.Service;
using TurtleToastService.Service;

namespace TurtleDialogService.DemoApp
{
    internal static class CustomDialogDemo
    {
        internal static void CreateCustomInputsDialog()
        {
            // A required input can be created by passing it to the button constructor below

            // Todo: Why is it required to both set allowEmpty to false and also put in the textbox to a button as a required input
            // The required mechanic needs to be reworked
            var textbox = new TextBoxInput("Required textbox input", allowEmpty: false);
            var combobox = CreateComboboxExample();
            var checkbox = new InputControlBase("Checkbox input", DialogInputType.CheckBox, defaultValue: false);
            //NestedComboBoxInput nestedCombobox = CreateNextedComboboxExample();

            var inputs = new List<IDialogInput>
            {
                textbox,
                combobox,
                checkbox,
            };

            var buttons = new List<DialogButton>()
            {
                new DialogButton("Ok", textbox, isDefault: true),
                new DialogButton("Cancel", isCancel: true)
            };

            var result = TurtleDialog.ExtendedDialog("Multiple buttons dialog", "Pick an option!", inputs, buttons);

            if (result == null)
            {
                TurtleToast.Information($"Result: (null)");
            }
            else
            {
                // Todo: InputValue shoud've been done with a indexer instead.
                var resultText = 
                    @$"Result: {result.Result}
                    Required textbox input: {result.InputValue<string>("Required textbox input")}
                    Required textbox input: {result.InputValue<string>("Option")}
                    Required textbox input: {result.InputValue<bool>("Checkbox input")}
";

                TurtleToast.Confirmation(resultText);

            }
        }

        private static ComboBoxInput CreateComboboxExample()
        {
            var options = new List<string>
            {
                "Option A",
                "Option B",
                "Option C",
                "Option D",
                "Option E"
            };

            // Label can be different from the name that the input can be referenced by in the code
            return new ComboBoxInput(name: "Option", valuesList: options,  inputTitle: "Combo box input");
        }

        private static NestedComboBoxInput CreateNextedComboboxExample()
        {
            // Todo: retrieving values out of nested comboboxes is not possible / broken
            //Required textbox input: { result.InputValue<string>("Country")}
            //Required textbox input: { result.InputValue<string>("Location")}
            //Required textbox input: { result.InputValue<string>("City")}

            var countryCityDict = new Dictionary<string, List<string>>
            {
                {"Poland", new List<string> { "Warsaw", "Szczecin", "Gdansk"} },
                {"France", new List<string> { "Paris", "Nantes", "Lyon"} },
                {"Germany", new List<string> { "Berlin", "Dortmund", "Cologne"} }
            };

            NestedComboBoxInputSettings primaryComboBoxInputSettings = new NestedComboBoxInputSettings("France", "Country");
            NestedComboBoxInputSettings secondaryComboBoxInputSettings = new NestedComboBoxInputSettings(inputTitle: "City", allowEditing: true);

            var nestedCombobox = new NestedComboBoxInput("Location", countryCityDict, primaryComboBoxInputSettings, secondaryComboBoxInputSettings);
            return nestedCombobox;
        }

        internal static void CreateMultipleButtonsDialog()
        {
            var buttons = new List<DialogButton>
            {
                new DialogButton("Default option", isDefault: true),
                new DialogButton("Secondary option"),
                new DialogButton("Third option"),
                new DialogButton("Cancel option", isCancel: true),
            };
            var result = TurtleDialog.ExtendedDialog("Multiple buttons dialog", "Pick an option!", buttons: buttons);

            TurtleToast.Information($"Result: {result.Result}");
        }
    }
}
