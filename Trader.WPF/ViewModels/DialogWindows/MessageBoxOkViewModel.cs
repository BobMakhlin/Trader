using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using WPF.Common.Helpers.MyRelayCommand;

namespace Trader.WPF.ViewModels
{
    class MessageBoxOkViewModel : BindableBase, IDialogAware
    {
        #region Fields
        string m_title;
        string m_windowMessage;
        #endregion

        #region Constructors
        public MessageBoxOkViewModel()
        {
            InitCommands();
        }
        #endregion

        #region Properties
        public string WindowMessage
        {
            get => m_windowMessage;
            set => SetProperty(ref m_windowMessage, value);
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
            Title = parameters.GetValue<string>("WindowTitle");
            WindowMessage = parameters.GetValue<string>("WindowMessage");
        }

        protected virtual void RaiseRequestClose(IDialogResult result)
        {
            var temp = RequestClose;
            temp?.Invoke(result);
        }
        #endregion

        #region Properties
        public RelayCommand CloseThisWindowCommand { get; set; }
        #endregion

        #region Methods
        void InitCommands()
        {
            CloseThisWindowCommand = new RelayCommand(CloseThisWindow);
        }
        void CloseThisWindow()
        {
            RaiseRequestClose(new DialogResult(ButtonResult.OK));
        }
        #endregion
    }
}
