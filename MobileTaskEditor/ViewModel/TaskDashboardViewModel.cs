using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MobileTaskEditor.Annotations;
using MobileTaskEditor.Commands;
using MobileTaskEditor.Model;

namespace MobileTaskEditor.ViewModel
{
    public sealed class TaskDashboardViewModel : INotifyPropertyChanged
    {
        private readonly OpenTaskCommand _openTaskCommand;
        private TaskInfo _selectedTask;
        private ObservableCollection<TaskInfo> _tasks = new ObservableCollection<TaskInfo>
        {
            new TaskInfo
            {
                Description = "This is the first task and it is a dummy"
            },
            new TaskInfo
            {
                Description = "This is the second task and it is a dummy"
            },
            new TaskInfo
            {
                Description = "This is the third task and it is a dummy"
            },
            new TaskInfo
            {
                Description = "This is the fourth task and it is a dummy"
            },
            new TaskInfo
            {
                Description = "This is the fifth task and it is a dummy"
            },
            new TaskInfo
            {
                Description = "This is the sixth task and it is a dummy"
            },
        };

        public TaskDashboardViewModel()
        {
            _openTaskCommand = new OpenTaskCommand();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TaskInfo> Tasks
        {
            get => _tasks;
            set
            {
                if (Equals(value, _tasks)) return;
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenTaskCommand => _openTaskCommand;

        public TaskInfo SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (Equals(value, _selectedTask)) return;
                _selectedTask = value;
                OnPropertyChanged();
                _openTaskCommand.OnCanExecuteChanged();
            }
        }


        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}