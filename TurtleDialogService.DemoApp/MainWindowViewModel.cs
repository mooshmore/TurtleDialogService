using CrossUtilitesWPF.Bases;
using TurtleDialogService.Service;
using TurtleToastService.Service;

namespace TurtleDialogService.DemoApp
{
    public class MainWindowViewModel
    {
        public static RelayCommand FuncRelayCommandCreator<T>(Func<T> method)
        {
            return new RelayCommand(() =>
            {
                T result = method();
                TurtleToast.Information($"Result: {result}");
            });
        }

        public RelayCommand NotificationDialogCommand { get; } = new RelayCommand(() => TurtleDialog.Notification("Notification dialog", "Pick an option!"));

        public RelayCommand YesNoDialogCommand { get; } = FuncRelayCommandCreator(() => TurtleDialog.YesNo("Yes / no dialog", "Pick an option!"));

        public RelayCommand OkCancelDialogCommand { get; } = FuncRelayCommandCreator(() => TurtleDialog.OkCancel("Yes / no dialog", "Pick an option!"));

        public RelayCommand InputDialogCommand { get; } = FuncRelayCommandCreator(() => TurtleDialog.OkCancel("Yes / no dialog", "Pick an option!"));

        public RelayCommand OptionDialogComand { get; } = FuncRelayCommandCreator(() => TurtleDialog.OkCancel("Yes / no dialog", "Pick an option!"));


        public RelayCommand CustomInputsDialogCommand { get; } = new RelayCommand(CustomDialogDemo.CreateCustomInputsDialog);
        public RelayCommand MultipleButtonsDialogCommand { get; } = new RelayCommand(CustomDialogDemo.CreateMultipleButtonsDialog);
    }
}
