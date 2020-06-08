using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.WPF.Helpers
{
    static class DialogServiceExtensions
    {
        public static void MessageBoxOk(this IDialogService dialogService, string windowTitle, string message)
        {
            var parameters = new DialogParameters();
            parameters.Add("WindowMessage", message);
            parameters.Add("WindowTitle", windowTitle);

            dialogService.ShowDialog
            (
                "MessageBoxOk",
                parameters,
                null
            );
        }
        public static void MessageBoxYesNo(this IDialogService dialogService, string windowTitle, string message, Action<IDialogResult> callback)
        {
            var parameters = new DialogParameters();
            parameters.Add("WindowMessage", message);
            parameters.Add("WindowTitle", windowTitle);

            dialogService.ShowDialog
            (
                "MessageBoxYesNo",
                parameters,
                callback
            );
        }
    }
}
