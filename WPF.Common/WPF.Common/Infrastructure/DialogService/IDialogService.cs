namespace WPF.Common.Services
{
    public interface IDialogService
    {
        string File { get; set; }
        bool OpenFileDialog();

        DialogResult MessageBoxYesNo(string msg, string caption = "");
        DialogResult MessageBoxOk(string msg, string caption = "");
    }
}
