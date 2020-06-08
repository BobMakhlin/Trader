using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Common.Helpers.MyRelayCommand;

namespace Trader.WPF.ViewModels.DialogWindows
{
    class MessageBoxYesNoViewModel : BindableBase, IDialogAware
    {
        #region Fields
        string m_windowMessage;
        string m_title;
        #endregion

        #region Constructors
        public MessageBoxYesNoViewModel()
        {
            InitCommands();
        }
        #endregion

        #region IDialogAware
        public event Action<IDialogResult> RequestClose;

        public string Title
        {
            get => m_title;
            set => SetProperty(ref m_title, value);
        }

        public bool CanCloseDialog() => true;
        public void OnDialogClosed() { }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            WindowMessage = parameters.GetValue<string>("WindowMessage");
            Title = parameters.GetValue<string>("WindowTitle");
        }

        protected virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            var temp = RequestClose;
            temp?.Invoke(dialogResult);
        }
        #endregion

        #region Properties
        public RelayCommand ChooseYesCommand { get; set; }
        public RelayCommand ChooseNoCommand { get; set; }

        public string WindowMessage
        {
            get => m_windowMessage;
            set => SetProperty(ref m_windowMessage, value);
        }
        #endregion

        #region Methods
        void InitCommands()
        {
            ChooseYesCommand = new RelayCommand(ChooseYes);
            ChooseNoCommand = new RelayCommand(ChooseNo);
        }

        void ChooseYes()
        {
            RaiseRequestClose(new DialogResult(ButtonResult.Yes));
        }
        void ChooseNo()
        {
            RaiseRequestClose(new DialogResult(ButtonResult.No));
        }
        #endregion
    }
}
