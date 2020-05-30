using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.Common.Services
{
    public class DialogService : IDialogService
    {
        #region Private Definitions
        OpenFileDialog openFileDialog = new OpenFileDialog();
        #endregion

        public string File { get; set; }

        public bool OpenFileDialog()
        {
            if (openFileDialog.ShowDialog() == true)
            {
                File = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public DialogResult MessageBoxYesNo(string msg, string caption = "")
        {
            var result = MessageBox.Show(msg, caption, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                return DialogResult.Yes;
            }
            return DialogResult.No;
        }
        public DialogResult MessageBoxOk(string msg, string caption = "")
        {
            MessageBox.Show(msg, caption, MessageBoxButton.OK);
            return DialogResult.OK;
        }
    }
}
