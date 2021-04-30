using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileTaskEditor.Annotations;
using MobileTaskEditor.Commands;
using MobileTaskEditor.Model;

namespace MobileTaskEditor.ViewModel
{
    public sealed class MainModel : INotifyPropertyChanged
    {
        private TaskInfoViewModel _currentTask;
        private RelayCommand _newTaskCommand;

        public MainModel()
        {
            _newTaskCommand = new RelayCommand(p =>
            {
                CurrentTask = new TaskInfoViewModel { TaskInfo = new TaskInfo() };
            }, p => true);
        }

        
        
        public TaskInfoViewModel CurrentTask
        {
            get => _currentTask;
            set
            {
                if (Equals(value, _currentTask)) return;
                _currentTask = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasTask));
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
            NewTaskCommand.OnCanExecuteChanged();
        }
    }
}