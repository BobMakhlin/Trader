using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF.Common.Helpers.MyRelayCommand
{
    public class RelayCommand : ICommand
    {
        #region Private Definitions
        Action m_execute;
        Func<bool> m_canExecute;
        #endregion

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            m_execute = execute;
            m_canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            m_execute.Invoke();
        }
        public bool CanExecute(object parameter)
        {
            return m_canExecute == null || m_canExecute.Invoke();
        }
    }
}
