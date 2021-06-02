using System;
using System.Windows.Input;

namespace ClassLibraryResources
{
    public class RelayCommand<T> : ICommand
    {
        private static bool CanExecute(T parameter)
            => true;

        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? CanExecute;
        }

        public bool CanExecute(object parameter)
            => _canExecute(TranslateParameter(parameter));

        public void Execute(object parameter)
            => _execute(TranslateParameter(parameter));

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        private static T TranslateParameter(object parameter)
        {
            T value = default;
            if (parameter != null && typeof(T).IsEnum)
            {
                value = (T) Enum.Parse(typeof(T), (string) parameter);
            }
            else
            {
                value = (T) parameter;
            }

            return value;
        }
    }

    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action execute, Func<bool> canExecute = null)
            : base(_ => execute(), canExecute == null ? null : new Func<object, bool>(_ => canExecute()))
        {
            
        }
    }
}