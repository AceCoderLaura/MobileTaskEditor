using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileTaskEditor.Annotations;

namespace MobileTaskEditor
{
    public class TaskInfo : INotifyPropertyChanged
    {
        private readonly MainModel _model;
        private string _description;

        public TaskInfo(MainModel model)
        {
            _model = model;
        }

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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            _model.CurrentTaskSaved = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}