using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileTaskEditor.Annotations;

namespace MobileTaskEditor.Model
{
    public sealed class TaskInfo : INotifyPropertyChanged, IChangeTracking
    {
        private string _description;
        private bool _isChanged;

        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(propertyName != nameof(IsChanged)) IsChanged = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AcceptChanges() => IsChanged = false;

        public bool IsChanged
        {
            get => _isChanged;
            private set
            {
                if (value == _isChanged) return;
                _isChanged = value;
                OnPropertyChanged();
            }
        }
    }
}