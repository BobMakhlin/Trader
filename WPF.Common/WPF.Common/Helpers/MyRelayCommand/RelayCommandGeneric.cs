using System;
using System.Windows.Input;

namespace WPF.Common.Helpers.MyRelayCommand
{
    public class RelayCommand<T> : ICommand
    {
        #region Private Definitions
        Action<T> m_action;
        Func<T, bool> m_canExecute;
        #endregion

        public RelayCommand(Action<T> action, Func<T, bool> canExecute = null)
        {
            m_action = action;
            m_canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            m_action.Invoke((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return m_canExecute == null || m_canExecute.Invoke((T)parameter);
        }
    }
}
