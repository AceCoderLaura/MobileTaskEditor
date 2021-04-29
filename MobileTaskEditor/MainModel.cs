using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileTaskEditor.Annotations;

namespace MobileTaskEditor
{
    public sealed class MainModel : INotifyPropertyChanged
    {
        private TaskInfo _currentTask;
        private SaveTaskCommand _saveTaskCommand;
        private RelayCommand _newTaskCommand;
        private bool _currentTaskSaved;

        public bool CurrentTaskSaved
        {
            get => _currentTaskSaved;
            set
            {
                if (value == _currentTaskSaved) return;
                _currentTaskSaved = value;
                OnPropertyChanged();
                SaveTaskCommand.OnCanExecuteChanged();
            }
        }

        public MainModel()
        {
            SaveTaskCommand = new SaveTaskCommand(this);
            NewTaskCommand = new RelayCommand(p =>
            {
                CurrentTask = new TaskInfo(this);
                CurrentTaskSaved = false;
            }, p => true);
        }

        public TaskInfo CurrentTask
        {
            get => _currentTask;
            set
            {
                if (Equals(value, _currentTask)) return;
                _currentTask = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasTask));
                SaveTaskCommand.OnCanExecuteChanged();
            }
        }

        public SaveTaskCommand SaveTaskCommand
        {
            get => _saveTaskCommand;
            set
            {
                if (Equals(value, _saveTaskCommand)) return;
                _saveTaskCommand = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NewTaskCommand
        {
            get => _newTaskCommand;
            set
            {
                if (Equals(value, _newTaskCommand)) return;
                _newTaskCommand = value;
                OnPropertyChanged();
            }
        }

        public bool HasTask => CurrentTask != null;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}