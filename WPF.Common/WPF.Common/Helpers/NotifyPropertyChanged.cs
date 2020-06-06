using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF.Common.Helpers
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            var tmp = PropertyChanged;
            tmp?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
