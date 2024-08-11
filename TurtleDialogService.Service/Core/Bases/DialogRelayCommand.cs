using System.Windows.Input;

namespace TurtleDialogService.Service.Core.Bases
{
    /// <summary>
    /// A generic dialog relay command implementation for the <see cref="DialogService"/>
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public class DialogRelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute = null;
        private readonly Func<T, bool> _canExecute = null;

        /// <summary>
        /// A event that is raised when the <see cref="_canExecute"/> changes.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// The default constructor.
        /// </summary>
        public DialogRelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (_ => true);
        }

        /// <summary>
        /// Checks if the given object can execute.
        /// </summary>
        public bool CanExecute(object parameter) => _canExecute((T)parameter);

        /// <summary>
        /// Executes with the given object.
        /// </summary>
        public void Execute(object parameter) => _execute((T)parameter);
    }
}