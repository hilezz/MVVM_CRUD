using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MVVM.Commands
{
    public class RelayCommand<T> : ICommand
    {


        private readonly Action<T> executeAction;
        private readonly Predicate<T> canExecute;


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

      
            
        public RelayCommand(Action<T> executeAction, Predicate<T> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        //public Relaycommand(Action<object> execute) : this(execute, null) { }


        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }
            
            
             return canExecute((T)parameter);
            
        }

        public void Execute(object parameter)
        {
            executeAction((T)parameter);
        }
    }

    public class RelayCommand : ICommand
    {

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public RelayCommand(Action<object> execute) : this(execute, null)
        {
            _execute = execute;

        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }   

      

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedEvent+=value;  
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedEvent -= value;
            }
        }


        private event EventHandler CanExecuteChangedEvent;


    }


}
