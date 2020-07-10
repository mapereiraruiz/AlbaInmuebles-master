using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class RelayGenericCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Predicate<T> canExecute;

        public RelayGenericCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter == null)
            {
                return true;
            }
            else
            {
                return canExecute == null || canExecute((T)parameter);
            }
        }

        public void Execute(object parameter)
        {
            execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
